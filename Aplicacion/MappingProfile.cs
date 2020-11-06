using AutoMapper;
using Dominio;
using Aplicacion.Cursos;
using System.Linq;

namespace Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*Obtiene la lista de instructores*/
            CreateMap<Curso, CursoDto>()
            .ForMember(x => x.Instructores, y => y.MapFrom(z => z.InstructoresLink
             .Select(a => a.Instructor).ToList()
             ))

            /*Obtiene la lista de comentarios*/
            .ForMember(x => x.Comentarios, y => y
              .MapFrom(z => z.ComentarioLista))

            /*Obtiene la lista de promociones*/
            .ForMember(x => x.Precio, y => y.MapFrom(z => z.PrecioPromocion));

            CreateMap<CursoInstructor, CursosInstructorDto>();
            CreateMap<Instructor, InstructorDto>();
            CreateMap<Comentario, ComentarioDto>();
            CreateMap<Precio, PrecioDto>();

        }
    }
}