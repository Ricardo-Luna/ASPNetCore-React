using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
	//http:localhost:5000/api/Cursos
	[Route("api/[controller]")]
	[ApiController]
	public class CursosController : MiControllerBase
	{
		private readonly IMediator _mediator;
		public CursosController(IMediator mediator)
		{
			_mediator = mediator;
		}
		//********************GET***************************************
		[HttpGet]
		
		public async Task<ActionResult<List<CursoDto>>> Get()
		{
			return await Mediator.Send(new Consulta.ListaCursos());
		}

		//http:localhost:5000/api/Cursos/1
		// ********************GET***************************************
		[HttpGet("{id}")]
		public async Task<ActionResult<CursoDto>> Detalle(Guid id)
		{
			return await Mediator.Send(new ConsultaId.CursoUnico { Id = id });
		}
		// //********************POST**************************************
		[HttpPost]
		public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
		{
			return await Mediator.Send(data);
		}
		// //********************PUT***************************************
		[HttpPut("{id}")]
		public async Task<ActionResult<Unit>> Editar(Guid id, Editar.Ejecuta data)
		{
			data.CursoId = id;
			return await Mediator.Send(data);
		}
		// //********************DELETE************************************
		[HttpDelete("{id}")]
		public async Task<ActionResult<Unit>> Eliminar(Guid id)
		{
			return await Mediator.Send(new Eliminar.Ejecutar { Id = id });
		}
	}
}