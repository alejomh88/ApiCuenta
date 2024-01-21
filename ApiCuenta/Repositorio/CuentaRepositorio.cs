using ApiCuenta.Data;
using ApiCuenta.Modelos;
using ApiCuenta.Repositorio.IRepositorio;

namespace ApiCuenta.Repositorio
{
    public class CuentaRepositorio: ICuentaRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public CuentaRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarCuenta(Cuenta cuenta)
        {
            _bd.Cuenta.Update(cuenta);
            return Guardar();
        }

        public bool BorrarCuenta(Cuenta cuenta)
        {
            _bd.Cuenta.Remove(cuenta);
            return Guardar();
        }

        public bool CrearCuenta(Cuenta cuenta)
        {
            _bd.Cuenta.Add(cuenta);
            return Guardar();
        }

        public bool ExisteCuenta(string numero)
        {
            bool valor = _bd.Cuenta.Any(c => c.Numero.Trim() == numero.Trim());
            return valor;
        }

        //public bool ExisteCuenta(int id)
        //{
        //    bool valor = _bd.Cuenta.Any(c => c.IdCuenta == id);
        //    return valor;
        //}

        public Cuenta GetCuenta(string numero)
        {
            return _bd.Cuenta.FirstOrDefault(c => c.Numero == numero);
        }

        public ICollection<Cuenta> GetCuentas()
        {
            return _bd.Cuenta.OrderBy(c => c.Numero).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;   
        }
    }
}
