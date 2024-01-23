using System.ComponentModel.DataAnnotations;

namespace ApiCuenta.Modelos.Dto
{
    public class ActualizarCuentaDto
    {
        public string Tipo { get; set; }

        public float Saldo { get; set; }

        public bool Estado { get; set; }

        public string Identificacion { get; set; }
    }
}
