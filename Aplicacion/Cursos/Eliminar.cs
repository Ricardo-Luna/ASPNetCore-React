using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
//using Aplicacion.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecutar : IRequest
        {
            public int Id { get; set; }
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
                var curso = await _context.Curso.FindAsync(request.Id);
                if (curso == null)
                {
                     throw new Exception("No se puede eliminar el curso");
                   // throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "No se encontró el curso" });
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