using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCuenta.Modelos.Dto
{
    public class MovimientoDto
    {
        public int IdMovimiento { get; set; }


        //public DateTime Fecha { get; set; }

        [Required]
        public string TipoMovimiento { get; set; }

        //public float Saldo { get; set; }

        public float Valor { get; set; }

        public string Numero { get; set; }
    }
}
