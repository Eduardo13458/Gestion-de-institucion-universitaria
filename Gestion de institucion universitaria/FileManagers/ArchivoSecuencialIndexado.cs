using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Gestion_de_institucion_universitaria.Models;

namespace Gestion_de_institucion_universitaria.FileManagers
{
    /// <summary>
    /// Manejador de archivo SECUENCIAL INDEXADO (ISAM)
    /// Usado para consultas de calificaciones con acceso rápido por índice
    /// y lectura secuencial para reportes
    /// </summary>
    public class ArchivoSecuencialIndexado
    {
        private readonly string _rutaArchivoDatos;
        private readonly string _rutaArchivoIndice;

        public ArchivoSecuencialIndexado(string rutaBase)
        {
            _rutaArchivoDatos = rutaBase + ".dat";
            _rutaArchivoIndice = rutaBase + ".idx";
        }

        /// <summary>
        /// Clase para representar una entrada en el índice
        /// </summary>
        private class EntradaIndice
        {
            public string Clave { get; set; } = string.Empty; // Matrícula
            public long Posicion { get; set; } // Posición en el archivo de datos
        }

        /// <summary>
        /// Agrega una calificación al archivo
        /// </summary>
        public void AgregarCalificacion(Calificacion calificacion)
        {
            long posicion;

            // Escribir en el archivo de datos
            using (var fs = new FileStream(_rutaArchivoDatos, FileMode.Append, FileAccess.Write))
            {
                posicion = fs.Position;
                string linea = SerializarCalificacion(calificacion);
                byte[] datos = Encoding.UTF8.GetBytes(linea + Environment.NewLine);
                fs.Write(datos, 0, datos.Length);
            }

            // Actualizar el índice
            ActualizarIndice(calificacion.Matricula, posicion);
        }

        /// <summary>
        /// Actualiza el archivo de índice
        /// </summary>
        private void ActualizarIndice(string matricula, long posicion)
        {
            var indices = CargarIndices();
            
            if (!indices.Any(i => i.Clave == matricula && i.Posicion == posicion))
            {
                indices.Add(new EntradaIndice { Clave = matricula, Posicion = posicion });
                indices = indices.OrderBy(i => i.Clave).ToList();
            }

            GuardarIndices(indices);
        }

        /// <summary>
        /// Carga todos los índices en memoria
        /// </summary>
        private List<EntradaIndice> CargarIndices()
        {
            var indices = new List<EntradaIndice>();

            if (!File.Exists(_rutaArchivoIndice))
                return indices;

            foreach (var linea in File.ReadAllLines(_rutaArchivoIndice))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                
                var partes = linea.Split('|');
                if (partes.Length == 2)
                {
                    indices.Add(new EntradaIndice
                    {
                        Clave = partes[0],
                        Posicion = long.Parse(partes[1])
                    });
                }
            }

            return indices;
        }

        /// <summary>
        /// Guarda los índices en el archivo
        /// </summary>
        private void GuardarIndices(List<EntradaIndice> indices)
        {
            using (var sw = new StreamWriter(_rutaArchivoIndice, false))
            {
                foreach (var entrada in indices)
                {
                    sw.WriteLine($"{entrada.Clave}|{entrada.Posicion}");
                }
            }
        }

        /// <summary>
        /// Busca calificaciones por matrícula usando el ÍNDICE (acceso rápido)
        /// Simula cuando un alumno entra a su portal
        /// </summary>
        public List<Calificacion> BuscarPorMatricula(string matricula)
        {
            var calificaciones = new List<Calificacion>();
            var indices = CargarIndices();
            
            // Búsqueda usando el índice
            var posiciones = indices.Where(i => i.Clave == matricula).Select(i => i.Posicion).ToList();

            if (!File.Exists(_rutaArchivoDatos))
                return calificaciones;

            using (var fs = new FileStream(_rutaArchivoDatos, FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs))
            {
                foreach (var posicion in posiciones)
                {
                    fs.Seek(posicion, SeekOrigin.Begin);
                    string? linea = sr.ReadLine();
                    if (linea != null)
                    {
                        var cal = DeserializarCalificacion(linea);
                        if (cal != null)
                            calificaciones.Add(cal);
                    }
                }
            }

            return calificaciones;
        }

        /// <summary>
        /// Lectura SECUENCIAL de todas las calificaciones ordenadas
        /// Simula cuando el coordinador necesita imprimir el acta final
        /// </summary>
        public List<Calificacion> LeerTodasSecuencial()
        {
            var calificaciones = new List<Calificacion>();

            if (!File.Exists(_rutaArchivoDatos))
                return calificaciones;

            foreach (var linea in File.ReadAllLines(_rutaArchivoDatos))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                
                var cal = DeserializarCalificacion(linea);
                if (cal != null)
                    calificaciones.Add(cal);
            }

            return calificaciones.OrderBy(c => c.Matricula).ThenBy(c => c.Materia).ToList();
        }

        /// <summary>
        /// Obtiene calificaciones por materia (lectura secuencial con filtro)
        /// </summary>
        public List<Calificacion> ObtenerCalificacionesPorMateria(string materia)
        {
            return LeerTodasSecuencial()
                .Where(c => c.Materia.Equals(materia, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        /// <summary>
        /// Calcula el promedio de un estudiante
        /// </summary>
        public double CalcularPromedio(string matricula)
        {
            var calificaciones = BuscarPorMatricula(matricula);
            return calificaciones.Any() ? calificaciones.Average(c => c.Nota) : 0;
        }

        private string SerializarCalificacion(Calificacion cal)
        {
            return $"{cal.Matricula}|{cal.Materia}|{cal.Nota}|{cal.Periodo}|{cal.FechaRegistro:O}";
        }

        private Calificacion? DeserializarCalificacion(string datos)
        {
            var partes = datos.Split('|');
            if (partes.Length < 5) return null;

            return new Calificacion
            {
                Matricula = partes[0],
                Materia = partes[1],
                Nota = double.Parse(partes[2]),
                Periodo = partes[3],
                FechaRegistro = DateTime.Parse(partes[4])
            };
        }

        /// <summary>
        /// Reconstruye el índice desde cero
        /// </summary>
        public void ReconstruirIndice()
        {
            var indices = new List<EntradaIndice>();

            if (!File.Exists(_rutaArchivoDatos))
                return;

            using (var fs = new FileStream(_rutaArchivoDatos, FileMode.Open, FileAccess.Read))
            using (var sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    long posicion = fs.Position;
                    string? linea = sr.ReadLine();
                    
                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        var cal = DeserializarCalificacion(linea);
                        if (cal != null)
                        {
                            indices.Add(new EntradaIndice
                            {
                                Clave = cal.Matricula,
                                Posicion = posicion
                            });
                        }
                    }
                }
            }

            GuardarIndices(indices.OrderBy(i => i.Clave).ToList());
        }
    }
}
