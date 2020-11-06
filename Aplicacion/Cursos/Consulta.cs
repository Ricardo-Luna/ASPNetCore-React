using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCursos : IRequest<List<CursoDto>> { }

        public class Manejador : IRequestHandler<ListaCursos, List<CursoDto>>
        {
            private readonly CursosOnlineContext _context;
            private readonly IMapper _mapper;

            //Se inyecta el mapper
            public Manejador(CursosOnlineContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<CursoDto>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                //Devolver la data al cliente usando un DTO
                var cursos = await _context.Curso
                .Include(x => x.ComentarioLista)
                .Include(X => X.PrecioPromocion)
                .Include(x => x.InstructoresLink)
                .ThenInclude(x => x.Instructor)
                .ToListAsync();

                // Map recibe dos parámetos, el tipo de dato origen y al que se transformará, después el
                // objeto, finalmente la data que se transformará
                var cursosDto = _mapper.Map<List<Curso>, List<CursoDto>>(cursos);


                return cursosDto;
            }
        }
    }
}