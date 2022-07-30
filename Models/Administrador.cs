using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Administrador
    {
        public int AdminId { get; set; }
        public string AdminNom { get; set; }
        public string AdminClave { get; set; }
        public int AdminCel { get; set; }
        public bool AdminEst { get; set; }
        public string AdminCorreo { get; set; }
    }
}