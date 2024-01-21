using ApiCuenta.Modelos;
using ApiCuenta.Modelos.Dto;
using AutoMapper;

namespace ApiCuenta.CuentaMappers
{
    public class CuentaMapper: Profile
    {
        public CuentaMapper()
        {
            CreateMap<Cuenta, CuentaDto>().ReverseMap();
            CreateMap<Cuenta, CrearCuentaDto>().ReverseMap();
            CreateMap<Movimiento, MovimientoDto>().ReverseMap();
            CreateMap<Movimiento, CrearMovimientoDto>().ReverseMap();
        }
    }
}
