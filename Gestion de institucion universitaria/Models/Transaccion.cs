using System;

namespace Gestion_de_institucion_universitaria.Models
{
    /// <summary>
    /// Modelo de transacción para el archivo secuencial (logs)
    /// </summary>
    [Serializable]
    public class Transaccion
    {
        public DateTime FechaHora { get; set; }
        public string TipoTransaccion { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }

        public Transaccion() 
        { 
            FechaHora = DateTime.Now;
        }

        public Transaccion(string tipoTransaccion, string matricula, string descripcion, decimal monto)
        {
            FechaHora = DateTime.Now;
            TipoTransaccion = tipoTransaccion;
            Matricula = matricula;
            Descripcion = descripcion;
            Monto = monto;
        }

        public override string ToString()
        {
            return $"{FechaHora:yyyy-MM-dd HH:mm:ss} | {TipoTransaccion} | {Matricula} | {Descripcion} | ${Monto:F2}";
        }
    }
}
