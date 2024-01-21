using ApiCuenta.Data;
using ApiCuenta.Modelos;
using ApiCuenta.Repositorio.IRepositorio;

namespace ApiCuenta.Repositorio
{
    public class MovimientoRepositorio : IMovimientoRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public MovimientoRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarMovimiento(Movimiento movimiento)
        {
            movimiento.Fecha = DateTime.Now;
            _bd.Movimimento.Update(movimiento);
            return Guardar();
        }

        public bool BorrarMovimiento(Movimiento movimiento)
        {
            _bd.Movimimento.Remove(movimiento);
            return Guardar();
        }

        public bool CrearMovimiento(Movimiento movimiento)
        {
            movimiento.Fecha = DateTime.Now;
            movimiento.Saldo = movimiento.Saldo;
            _bd.Movimimento.Add(movimiento);
            return Guardar();
        }

        public bool ExisteMovimiento(int id)
        {
            bool valor = _bd.Movimimento.Any(c => c.IdMovimiento == id);
            return valor;
        }

        public bool ExisteMovimiento(DateTime fecha)
        {
            bool valor = _bd.Movimimento.Any(c => c.Fecha == fecha);
            return valor;
        }

        public Movimiento GetMovimiento(int id)
        {
            return _bd.Movimimento.FirstOrDefault(c => c.IdMovimiento == id);
        }

        public ICollection<Movimiento> GetMovimientos()
        {
            return _bd.Movimimento.OrderBy(c => c.Numero).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

    }
}
