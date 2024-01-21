using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCuenta.Modelos.Dto
{
    public class CrearMovimientoDto
    {

        [Required]
        //public DateTime Fecha { get; set; }

        public string TipoMovimiento { get; set; }

        //public float Saldo { get; set; }

        public float Valor { get; set; }

        public string Numero { get; set; }
    }
}
