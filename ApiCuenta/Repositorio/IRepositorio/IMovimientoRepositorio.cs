using ApiCuenta.Modelos;

namespace ApiCuenta.Repositorio.IRepositorio
{
    public interface IMovimientoRepositorio
    {

        Movimiento GetMovimiento(int id);

        ICollection<Movimiento> GetMovimientos();

        bool CrearMovimiento(Movimiento movimiento);

        bool ActualizarMovimiento(Movimiento movimiento);

        bool BorrarMovimiento(Movimiento movimiento);

        bool ExisteMovimiento(DateTime fecha);

        bool ExisteMovimiento(int id);

        bool Guardar();
    }
}
