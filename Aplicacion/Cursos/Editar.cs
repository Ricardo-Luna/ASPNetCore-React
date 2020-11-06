using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public Guid CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public List<Guid> ListaInstructor { get; set; }
        }
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId);
                if (curso == null)
                {
                    //throw new Exception("No se puede eliminar el curso");
                    throw new ManejadorException(HttpStatusCode.NotFound, new { mensaje = "No se encontró el curso" });
                }
                curso.Titulo = request.Titulo ?? request.Titulo;
                curso.Descripcion = request.Descripcion ?? request.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? request.FechaPublicacion;

                if (request.ListaInstructor != null) { }
                {
                    if (request.ListaInstructor.Count > 0)
                    {
                        /*Eliminar instructores actuales en la base de datos*/
                        var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.CursoId).ToList();
                        foreach (var instructorEliminar in instructoresDB)
                        {
                            _context.CursoInstructor.Remove(instructorEliminar);
                        }
                        //Fin procedimiento

                        /*Procedimiento para agregar instructores provinientes del cliente*/
                        foreach (var ids in request.ListaInstructor)
                        {
                            var nuevoInstructor = new CursoInstructor
                            {
                                CursoId = request.CursoId,
                                InstructorId = ids
                            };
                            _context.CursoInstructor.Add(nuevoInstructor);
                        }
                        //Fin del procedimiento
                    }

                }


                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se guardaron los cambios en el curso");

            }
        }
    }
}
