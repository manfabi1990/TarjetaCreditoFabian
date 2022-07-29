using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ManBTarjetas.Models
{
    public class TarjetaModel
    {
        [Key]
        [Required]
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
