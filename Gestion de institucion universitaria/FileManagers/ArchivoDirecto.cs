using System;
using System.IO;
using System.Text;
using Gestion_de_institucion_universitaria.Models;

namespace Gestion_de_institucion_universitaria.FileManagers
{
    /// <summary>
    /// Manejador de archivo DIRECTO con acceso por HASH
    /// Usado para control de acceso rápido (validación de credenciales)
    /// </summary>
    public class ArchivoDirecto
    {
        private const int TAMAÑO_REGISTRO = 256; // Tamaño fijo para cada registro
        private const int TOTAL_POSICIONES = 10000; // Tabla hash con 10,000 posiciones
        private readonly string _rutaArchivo;

        public ArchivoDirecto(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
            InicializarArchivo();
        }

        /// <summary>
        /// Inicializa el archivo con espacios vacíos si no existe
        /// </summary>
        private void InicializarArchivo()
        {
            if (!File.Exists(_rutaArchivo))
            {
                using (var fs = new FileStream(_rutaArchivo, FileMode.Create, FileAccess.Write))
                {
                    byte[] registroVacio = new byte[TAMAÑO_REGISTRO];
                    for (int i = 0; i < TOTAL_POSICIONES; i++)
                    {
                        fs.Write(registroVacio, 0, TAMAÑO_REGISTRO);
                    }
                }
            }
        }

        /// <summary>
        /// Función HASH simple: convierte la matrícula a posición en el archivo
        /// </summary>
        private int CalcularHash(string matricula)
        {
            int suma = 0;
            foreach (char c in matricula)
            {
                suma += c;
            }
            return suma % TOTAL_POSICIONES;
        }

        /// <summary>
        /// Guarda un estudiante en el archivo usando hash
        /// </summary>
        public void GuardarEstudiante(Estudiante estudiante)
        {
            int posicion = CalcularHash(estudiante.Matricula);
            long offset = posicion * TAMAÑO_REGISTRO;

            // Serializar el estudiante a un formato fijo
            string datos = SerializarEstudiante(estudiante);
            byte[] buffer = new byte[TAMAÑO_REGISTRO];
            byte[] datosBytes = Encoding.UTF8.GetBytes(datos);
            
            Array.Copy(datosBytes, buffer, Math.Min(datosBytes.Length, TAMAÑO_REGISTRO - 1));

            using (var fs = new FileStream(_rutaArchivo, FileMode.Open, FileAccess.Write))
            {
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Write(buffer, 0, TAMAÑO_REGISTRO);
            }
        }

        /// <summary>
        /// Busca un estudiante por matrícula usando acceso directo (O(1))
        /// Simula el lector de credenciales en la entrada
        /// </summary>
        public Estudiante? BuscarEstudiante(string matricula)
        {
            int posicion = CalcularHash(matricula);
            long offset = posicion * TAMAÑO_REGISTRO;

            byte[] buffer = new byte[TAMAÑO_REGISTRO];

            using (var fs = new FileStream(_rutaArchivo, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Read(buffer, 0, TAMAÑO_REGISTRO);
            }

            string datos = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
            
            if (string.IsNullOrWhiteSpace(datos))
                return null;

            return DeserializarEstudiante(datos);
        }

        /// <summary>
        /// Valida el estatus de un estudiante (ACCESO RÁPIDO para entrada del campus)
        /// </summary>
        public bool ValidarAcceso(string matricula)
        {
            var estudiante = BuscarEstudiante(matricula);
            return estudiante != null && estudiante.EstaInscrito;
        }

        private string SerializarEstudiante(Estudiante est)
        {
            return $"{est.Matricula}|{est.Nombre}|{est.Apellido}|{est.Carrera}|{est.EstaInscrito}|{est.FechaInscripcion:O}";
        }

        private Estudiante DeserializarEstudiante(string datos)
        {
            var partes = datos.Split('|');
            if (partes.Length < 6) return null!;

            return new Estudiante
            {
                Matricula = partes[0],
                Nombre = partes[1],
                Apellido = partes[2],
                Carrera = partes[3],
                EstaInscrito = bool.Parse(partes[4]),
                FechaInscripcion = DateTime.Parse(partes[5])
            };
        }

        /// <summary>
        /// Obtiene estadísticas del archivo
        /// </summary>
        public (int total, int activos, int inactivos) ObtenerEstadisticas()
        {
            int total = 0, activos = 0, inactivos = 0;

            byte[] buffer = new byte[TAMAÑO_REGISTRO];
            using (var fs = new FileStream(_rutaArchivo, FileMode.Open, FileAccess.Read))
            {
                for (int i = 0; i < TOTAL_POSICIONES; i++)
                {
                    fs.Read(buffer, 0, TAMAÑO_REGISTRO);
                    string datos = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                    
                    if (!string.IsNullOrWhiteSpace(datos))
                    {
                        total++;
                        var est = DeserializarEstudiante(datos);
                        if (est != null)
                        {
                            if (est.EstaInscrito) activos++;
                            else inactivos++;
                        }
                    }
                }
            }

            return (total, activos, inactivos);
        }
    }
}
