using ApiCuenta.Modelos;
using ApiCuenta.Modelos.Dto;
using ApiCuenta.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiCuenta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaRepositorio _cuRepo;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public CuentasController(ICuentaRepositorio cuRepo, IMapper mapper, HttpClient httpClient)
        {
            _cuRepo = cuRepo;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCuentas()
        {
            var listaCuentas = _cuRepo.GetCuentas();

            var listaCuentasDto = new List<CuentaDto>();

            foreach (var lista in listaCuentas)
            {
                listaCuentasDto.Add(_mapper.Map<CuentaDto>(lista));
            }
            return Ok(listaCuentasDto);
        }

        [HttpGet("{Numero:int}", Name = "GetCuenta")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCuenta(string Numero)
        {
            var itemCuenta = _cuRepo.GetCuenta(Numero);

            if (itemCuenta == null)
            {
                return NotFound();
            }

            var itemCuentaDto = _mapper.Map<CuentaDto>(itemCuenta);
            return Ok(itemCuenta);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CuentaDto))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CrearCuenta([FromBody] CuentaDto crearCuentaDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (crearCuentaDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_cuRepo.ExisteCuenta(crearCuentaDto.Numero))
            {
                ModelState.AddModelError("", "La Cuenta ya existe");
                return StatusCode(404, ModelState);
            }

            string identificacion = crearCuentaDto.Identificacion;
            var response = await _httpClient.GetAsync($"https://localhost:7112/api/Clientes/{identificacion}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            else
            {
                var Cuenta = _mapper.Map<Cuenta>(crearCuentaDto);
                if (!_cuRepo.CrearCuenta(Cuenta))
                {
                    ModelState.AddModelError("", $"Algo salió mal guardando el registro {Cuenta.Numero}");
                    return StatusCode(500, ModelState);
                }
                return CreatedAtRoute("GetCuenta", new { Numero = Cuenta.Numero }, Cuenta);
            }
        }

        [HttpPatch("{Numero:int}", Name = "ActualizarPatchCuenta")]
        [ProducesResponseType(201, Type = typeof(CuentaDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ActualizarPatchCuenta(string Numero, [FromBody] CuentaDto CuentaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (CuentaDto == null || Numero != CuentaDto.Numero)
            {
                return BadRequest(ModelState);
            }

            string identificacion = CuentaDto.Identificacion;
            var response = await _httpClient.GetAsync($"https://localhost:7112/api/Clientes/{identificacion}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            else
            {
                var Cuenta = _mapper.Map<Cuenta>(CuentaDto);

                if (!_cuRepo.ActualizarCuenta(Cuenta))
                {
                    ModelState.AddModelError("", $"Algo salió mal actualizando el registro{Cuenta.Numero}");
                    return StatusCode(500, ModelState);
                }
                return NoContent();
            }
        }

        [HttpDelete("{Numero:int}", Name = "BorrarCuenta")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult BorrarCuenta(string Numero)
        {
            if (!_cuRepo.ExisteCuenta(Numero))
            {
                return NotFound();
            }

            var Cuenta = _cuRepo.GetCuenta(Numero);

            if (!_cuRepo.BorrarCuenta(Cuenta))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {Cuenta.Numero}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
