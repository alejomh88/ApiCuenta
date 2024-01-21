using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCuenta.Modelos
{
    public class Reporte
    {
        [Key]
        public DateTime Fecha { get; set; }

        public string Nombre { get; set; }

        public string NumeroCuenta { get; set; }

        public string TipoCuenta { get; set; }

        public float Saldoinicial { get; set; }

        public string Estado { get; set; }

        public string TipoMovimiento { get; set; }

        public float ValorMovimiento { get; set; }

        public float SaldoDisponible { get; set; }

    }
}
