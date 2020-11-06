using System;
using Dominio;

namespace Aplicacion.Cursos
{
    public class CursosInstructorDto
    {
        public Curso CursoId { get; set; }
		public Guid InstructorId { get; set; }
    }
}