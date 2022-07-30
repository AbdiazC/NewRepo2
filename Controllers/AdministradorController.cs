using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplication1.Permisos;

namespace WebApplication1.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        [ValidarSesion]
        public ActionResult Index()
        {
            return View();
        }
    }
}