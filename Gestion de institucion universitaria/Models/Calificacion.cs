using System;

namespace Gestion_de_institucion_universitaria.Models
{
    /// <summary>
    /// Modelo de calificación para el archivo indexado
    /// </summary>
    [Serializable]
    public class Calificacion
    {
        public string Matricula { get; set; } = string.Empty;
        public string Materia { get; set; } = string.Empty;
        public double Nota { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }

        public Calificacion() { }

        public Calificacion(string matricula, string materia, double nota, string periodo)
        {
            Matricula = matricula;
            Materia = materia;
            Nota = nota;
            Periodo = periodo;
            FechaRegistro = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Matricula} - {Materia}: {Nota:F2} ({Periodo})";
        }
    }
}
