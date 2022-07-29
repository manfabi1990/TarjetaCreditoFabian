using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MBTarjetas.Models
{
    public class TarjetaCredito
    {

        [Key]
        public int id { get; set; }

        [Required]
        public string titular { get; set; }

        [Required]
        public string numeroTarjeta { get; set; }

        [Required]
        public string fechaExpiracion { get; set; }

        [Required]
        public string cvv { get; set; }
    }
}
