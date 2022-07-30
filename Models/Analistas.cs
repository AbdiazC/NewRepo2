using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Analistas
    {
        public int AnalisId { get; set; }
        public string AnalisNom { get; set; }
        public string AnalisClave { get; set; }
        public int AnalisCel { get; set; }
        public bool AnalisEst { get; set; }
        public string AnalisCorreo { get; set; }
    }
}