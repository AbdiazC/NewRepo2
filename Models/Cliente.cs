using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Cliente
    {
        public int CliId { get; set; }
        public string CliNom { get; set; }
        public string CliClave { get; set; }
        public int CliCel { get; set; }
        public int CliSaldo { get; set; }
        public string CliCorreo { get; set; }

        public string ConfirmarCliClave { get; set; }

    }
}