using Aplicacion.ManejadorError;
//using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecutar : IRequest
        {
            public Guid Id { get; set; }
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.Id);
                foreach (var instructor in instructoresDB)
                {
                    _context.CursoInstructor.Remove(instructor);
                }
                var curso = await _context.Curso.FindAsync(request.Id);
                if (curso == null)
                {
                    //  throw new Exception("No se puede eliminar el curso");
                    throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "No se encontró el curso" });
                }
                _context.Remove(curso);
                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;

                }
                throw new Exception("No se pudieron guardar los cambios");
            }
        }
    }
}