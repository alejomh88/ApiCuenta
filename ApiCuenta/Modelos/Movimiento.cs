using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCuenta.Modelos
{
    public class Movimiento
    {
        [Key]
        public int IdMovimiento { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public string TipoMovimiento { get; set; }

        public float Saldo { get; set; }

        public float Valor { get; set; }

        [ForeignKey("Cuenta")]
        public string Numero { get; set; }

        public Cuenta Cuenta { get; set; }

    }
}
