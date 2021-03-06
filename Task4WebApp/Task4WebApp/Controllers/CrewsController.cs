﻿using System.Threading.Tasks;
using DTOLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Task4WebApp.Controllers
{
	[Produces("application/json")]
    [Route("api/Crews")]
    public class CrewsController : Controller
    {
		private readonly AirportService.Services.CrewService airport;

		public CrewsController(AirportService.Services.CrewService service)
		{
			this.airport = service;

		}
		// GET: api/Crews
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			try
			{
				var crews = await this.airport.GetCrews();
				if (crews == null)
				{
					return NotFound();
				}
				return Ok(crews);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// GET: api/Crews/5
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var crew = await this.airport.GetCrewById(id);
				if (crew == null)
				{
					return NotFound();
				}
				return Ok(crew);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// POST: api/Crews
		[HttpPost("departure-id/{id}")]
		public async Task<IActionResult> Post(int departId, [FromBody]CrewDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await airport.CreateCrew(departId, value);

					return Ok(result);
				}
				return BadRequest(ModelState);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// PUT: api/Crews/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody]CrewDTO value)
		{
			try
			{
				if (ModelState.IsValid)
				{
					value.Id = id;
					var result = await airport.UpdateCrew(value);

					return Ok(result);
				}
				return BadRequest(ModelState);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var result = await airport.DeleteCrew(id);
				return Ok(result);
			}
			catch (System.Exception ex)
			{

				return BadRequest(ex.Message);
			}
		}

		// LOad: api/ApiWithActions/5
		[HttpGet("load/")]
		public async Task<IActionResult> Load(int id)
		{
			try
			{

				var result = airport.LoadCrews();
				await Task.WhenAll(airport.WriteFile(result), airport.WriteToDb(result));

				return Ok(result);
			}
			catch (System.Exception ex)
			{
			
					return BadRequest(ex.Message);
			}
		}
	}
}
