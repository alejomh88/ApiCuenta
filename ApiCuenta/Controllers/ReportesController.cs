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
    public class ReportesController : ControllerBase
    {
        private readonly ApplicationDbContext _bd;

        public ReportesController(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        [HttpGet("{FechaInicial:datetime}/{FechaFinal:datetime}/{Cuenta:int}", Name = "GetReporteMovimientos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<List<Reporte>> GetReporteMovimientos(DateTime FechaInicial, DateTime FechaFinal, string Cuenta)
        {
            var result = await _bd.Reporte.FromSqlRaw("EXEC GET_REPORTE_MOVIMIENTOS {0}, {1}, {2};", FechaInicial, FechaFinal, Cuenta).ToListAsync();
            return result;
        }

    }
}
