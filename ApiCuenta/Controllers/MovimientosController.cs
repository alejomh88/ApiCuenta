using ApiCuenta.Data;
using ApiCuenta.Modelos;
using ApiCuenta.Modelos.Dto;
using ApiCuenta.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiCuenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoRepositorio _mvRepo;
        private readonly ICuentaRepositorio _cuRepo;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public MovimientosController(IMovimientoRepositorio mvRepo, ICuentaRepositorio cuRepo, IMapper mapper, HttpClient httpClient)
        {
            _mvRepo = mvRepo;
            _cuRepo = cuRepo;
            _mapper = mapper;
            _httpClient = httpClient;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMovimientos()
        {
            var listaMovimientos = _mvRepo.GetMovimientos();

            var listaMovimientosDto = new List<MovimientoDto>();

            foreach (var lista in listaMovimientos)
            {
                listaMovimientosDto.Add(_mapper.Map<MovimientoDto>(lista));
            }
            return Ok(listaMovimientosDto);
        }

        [HttpGet("{IdMovimiento:int}", Name = "GetMovimiento")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetMovimiento(int IdMovimiento)
        {
            var itemMovimiento = _mvRepo.GetMovimiento(IdMovimiento);

            if (itemMovimiento == null)
            {
                return NotFound();
            }

            var itemMovimientoDto = _mapper.Map<MovimientoDto>(itemMovimiento);
            return Ok(itemMovimiento);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(MovimientoDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearMovimiento([FromBody] CrearMovimientoDto crearMovimientoDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearMovimientoDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_mvRepo.ExisteMovimiento(crearMovimientoDto.Fecha))
            {
                ModelState.AddModelError("", "El Movimiento ya existe");
                return StatusCode(404, ModelState);
            }

            var Movimiento = _mapper.Map<Movimiento>(crearMovimientoDto);

            var Cuenta = crearMovimientoDto.Numero;
            Cuenta cuenta = _cuRepo.GetCuenta(Cuenta);
            Movimiento.Saldo = cuenta.Saldo;

            if (!_mvRepo.CrearMovimiento(Movimiento))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {Movimiento.IdMovimiento}");
                return StatusCode(500, ModelState);
            }

            cuenta.Saldo = cuenta.Saldo + Movimiento.Valor;

            if (!_cuRepo.ActualizarCuenta(cuenta))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el saldo de la cuenta {Movimiento.Numero}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetMovimiento", new { IdMovimiento = Movimiento.IdMovimiento }, Movimiento);
        }

        [HttpPatch("{IdMovimiento:int}", Name = "ActualizarPatchMovimiento")]
        [ProducesResponseType(201, Type = typeof(MovimientoDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarPatchMovimiento(int IdMovimiento, [FromBody] MovimientoDto MovimientoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (MovimientoDto == null || IdMovimiento != MovimientoDto.IdMovimiento)
            {
                return BadRequest(ModelState);
            }

            var Movimiento = _mapper.Map<Movimiento>(MovimientoDto);

            if (!_mvRepo.ActualizarMovimiento(Movimiento))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro{Movimiento.IdMovimiento}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{IdMovimiento:int}", Name = "BorrarMovimiento")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarMovimiento(int IdMovimiento)
        {
            if (!_mvRepo.ExisteMovimiento(IdMovimiento))
            {
                return NotFound();
            }

            var Movimiento = _mvRepo.GetMovimiento(IdMovimiento);

            if (!_mvRepo.BorrarMovimiento(Movimiento))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {Movimiento.IdMovimiento}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
