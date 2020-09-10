using System;

namespace Dominio
{
    public class Comentario
    {
        public int ComentarioId { get; set; }
        public string Alumno { get; set; }
        public int Puntaje { get; set; }
        public string ComentarioText { get; set; }
        public int CursoId { get; set; }

        public Curso Curso { get; set; }
    }
}