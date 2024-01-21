using ApiCuenta.Modelos;

namespace ApiCuenta.Repositorio.IRepositorio
{
    public interface ICuentaRepositorio
    {

        Cuenta GetCuenta(string numero);

        ICollection<Cuenta> GetCuentas();

        bool CrearCuenta(Cuenta cuenta);

        bool ActualizarCuenta(Cuenta cuenta);

        bool BorrarCuenta(Cuenta cuenta);

        bool ExisteCuenta(string numero);

        bool Guardar();
    }
}
