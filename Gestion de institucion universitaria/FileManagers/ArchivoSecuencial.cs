using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Gestion_de_institucion_universitaria.Models;

namespace Gestion_de_institucion_universitaria.FileManagers
{
    /// <summary>
    /// Manejador de archivo SECUENCIAL
    /// Usado para logs de transacciones y procesamiento por lotes
    /// </summary>
    public class ArchivoSecuencial
    {
        private readonly string _rutaArchivo;

        public ArchivoSecuencial(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
        }

        /// <summary>
        /// Registra una transacción (escritura secuencial al final del archivo)
        /// Simula el registro de pagos, impresiones, etc.
        /// </summary>
        public void RegistrarTransaccion(Transaccion transaccion)
        {
            using (var sw = new StreamWriter(_rutaArchivo, append: true, Encoding.UTF8))
            {
                sw.WriteLine(SerializarTransaccion(transaccion));
            }
        }

        /// <summary>
        /// Lee todas las transacciones secuencialmente (procesamiento por lotes)
        /// Simula el corte de caja diario o auditoría contable
        /// </summary>
        public List<Transaccion> LeerTodasTransacciones()
        {
            var transacciones = new List<Transaccion>();

            if (!File.Exists(_rutaArchivo))
                return transacciones;

            foreach (var linea in File.ReadAllLines(_rutaArchivo, Encoding.UTF8))
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;
                
                var transaccion = DeserializarTransaccion(linea);
                if (transaccion != null)
                    transacciones.Add(transaccion);
            }

            return transacciones;
        }

        /// <summary>
        /// Genera reporte de corte de caja diario
        /// Lee secuencialmente todas las transacciones del día
        /// </summary>
        public Dictionary<string, object> GenerarCorteCaja(DateTime fecha)
        {
            var transacciones = LeerTodasTransacciones()
                .Where(t => t.FechaHora.Date == fecha.Date)
                .ToList();

            var reporte = new Dictionary<string, object>
            {
                ["Fecha"] = fecha.ToShortDateString(),
                ["TotalTransacciones"] = transacciones.Count,
                ["TotalIngresos"] = transacciones.Where(t => t.Monto > 0).Sum(t => t.Monto),
                ["TotalEgresos"] = transacciones.Where(t => t.Monto < 0).Sum(t => t.Monto),
                ["SaldoNeto"] = transacciones.Sum(t => t.Monto),
                ["PorTipo"] = transacciones.GroupBy(t => t.TipoTransaccion)
                    .ToDictionary(g => g.Key, g => new { 
                        Cantidad = g.Count(), 
                        Total = g.Sum(t => t.Monto) 
                    })
            };

            return reporte;
        }

        /// <summary>
        /// Obtiene transacciones por matrícula (requiere lectura completa del archivo)
        /// </summary>
        public List<Transaccion> ObtenerTransaccionesPorMatricula(string matricula)
        {
            return LeerTodasTransacciones()
                .Where(t => t.Matricula == matricula)
                .OrderBy(t => t.FechaHora)
                .ToList();
        }

        /// <summary>
        /// Obtiene transacciones por tipo
        /// </summary>
        public List<Transaccion> ObtenerTransaccionesPorTipo(string tipo)
        {
            return LeerTodasTransacciones()
                .Where(t => t.TipoTransaccion.Equals(tipo, StringComparison.OrdinalIgnoreCase))
                .OrderBy(t => t.FechaHora)
                .ToList();
        }

        /// <summary>
        /// Procesa el archivo completo y genera estadísticas
        /// Simula el procesamiento nocturno de auditoría
        /// </summary>
        public string GenerarEstadisticas()
        {
            var transacciones = LeerTodasTransacciones();
            var sb = new StringBuilder();

            sb.AppendLine("=== ESTADÍSTICAS DE TRANSACCIONES ===");
            sb.AppendLine($"Total de transacciones: {transacciones.Count}");
            sb.AppendLine($"Período: {transacciones.Min(t => t.FechaHora):dd/MM/yyyy} - {transacciones.Max(t => t.FechaHora):dd/MM/yyyy}");
            sb.AppendLine();
            
            sb.AppendLine("Por tipo de transacción:");
            foreach (var grupo in transacciones.GroupBy(t => t.TipoTransaccion))
            {
                sb.AppendLine($"  {grupo.Key}: {grupo.Count()} transacciones, Total: ${grupo.Sum(t => t.Monto):F2}");
            }
            
            sb.AppendLine();
            sb.AppendLine($"Total Ingresos: ${transacciones.Where(t => t.Monto > 0).Sum(t => t.Monto):F2}");
            sb.AppendLine($"Total Egresos: ${Math.Abs(transacciones.Where(t => t.Monto < 0).Sum(t => t.Monto)):F2}");
            sb.AppendLine($"Saldo Neto: ${transacciones.Sum(t => t.Monto):F2}");

            return sb.ToString();
        }

        /// <summary>
        /// Archiva transacciones antiguas (procesamiento por lotes)
        /// </summary>
        public int ArchivarTransaccionesAntiguas(DateTime fechaLimite, string rutaArchivo)
        {
            var transacciones = LeerTodasTransacciones();
            var antiguos = transacciones.Where(t => t.FechaHora < fechaLimite).ToList();
            var recientes = transacciones.Where(t => t.FechaHora >= fechaLimite).ToList();

            // Guardar antiguos en archivo de archivo
            using (var sw = new StreamWriter(rutaArchivo, false, Encoding.UTF8))
            {
                foreach (var trans in antiguos)
                {
                    sw.WriteLine(SerializarTransaccion(trans));
                }
            }

            // Reescribir archivo principal solo con recientes
            using (var sw = new StreamWriter(_rutaArchivo, false, Encoding.UTF8))
            {
                foreach (var trans in recientes)
                {
                    sw.WriteLine(SerializarTransaccion(trans));
                }
            }

            return antiguos.Count;
        }

        private string SerializarTransaccion(Transaccion trans)
        {
            return $"{trans.FechaHora:O}|{trans.TipoTransaccion}|{trans.Matricula}|{trans.Descripcion}|{trans.Monto}";
        }

        private Transaccion? DeserializarTransaccion(string datos)
        {
            var partes = datos.Split('|');
            if (partes.Length < 5) return null;

            return new Transaccion
            {
                FechaHora = DateTime.Parse(partes[0]),
                TipoTransaccion = partes[1],
                Matricula = partes[2],
                Descripcion = partes[3],
                Monto = decimal.Parse(partes[4])
            };
        }

        /// <summary>
        /// Limpia el archivo de transacciones
        /// </summary>
        public void LimpiarArchivo()
        {
            if (File.Exists(_rutaArchivo))
            {
                File.Delete(_rutaArchivo);
            }
        }
    }
}
