using EventsAPI.Service.Dto;
using EventsAPI.Service.Entity;
using EventsAPI.Service.Interface;
using EventsAPI.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EventsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CityEventController : ControllerBase
    {
        public ICityEventService _cityEventService { get; set; }
        public IGenerateTokenService _tokenService { get; set; }

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService = cityEventService;
        }

        [HttpGet("title")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CityEventDto>>> GetEventTitle(string title)
        {
            List<CityEventDto> cityEvents = await _cityEventService.SearchEventByTitle(title);
            if (cityEvents == null) return BadRequest();
            return Ok(cityEvents);
        }

        [HttpGet("eventPrice")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CityEventDto>>> GetEventPrice(decimal minPrice, decimal maxPrice, DateTime date)
        {
            List<CityEventDto> cityEvents = await _cityEventService.SearchEventByPriceRangeAndDate(minPrice, maxPrice, date);
            return Ok(cityEvents);
        }

        [HttpGet("localEvent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CityEventDto>>> GetEventLocalAndData(string local, DateTime date)
        {
            List<CityEventDto> eventos = await _cityEventService.SearchEventByLocalAndDate(local, date);
            return Ok(eventos);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CityEventDto>> AdicionarEvento(CityEventDto cityEvent)
        {
            if (!await _cityEventService.AddEvent(cityEvent))
            {
                return BadRequest();
            }

            return Ok(cityEvent);
        }

        [HttpPut("update")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditarEvento(int index, CityEventDto cityEvent)
        {
            if (!await _cityEventService.EditEvent(cityEvent, index))
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ExcluirEvento([FromQuery] int id)
        {
            if (!await _cityEventService.RemoveEvent(id))
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}