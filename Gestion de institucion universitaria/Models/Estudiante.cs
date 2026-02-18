using System;

namespace Gestion_de_institucion_universitaria.Models
{
    /// <summary>
    /// Modelo de estudiante para el sistema universitario
    /// </summary>
    [Serializable]
    public class Estudiante
    {
        public string Matricula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public bool EstaInscrito { get; set; }
        public DateTime FechaInscripcion { get; set; }

        public Estudiante() { }

        public Estudiante(string matricula, string nombre, string apellido, string carrera, bool estaInscrito)
        {
            Matricula = matricula;
            Nombre = nombre;
            Apellido = apellido;
            Carrera = carrera;
            EstaInscrito = estaInscrito;
            FechaInscripcion = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Matricula} - {Apellido}, {Nombre} - {Carrera} - {(EstaInscrito ? "Activo" : "Inactivo")}";
        }
    }
}
