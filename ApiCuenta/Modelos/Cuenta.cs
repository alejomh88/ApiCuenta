using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCuenta.Modelos
{
    public class Cuenta
    {
        [Key]
        public string Numero { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public float Saldo { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        public string Identificacion { get; set; }

    }
}
