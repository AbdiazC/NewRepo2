using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;

namespace WebApplication1.Controllers
{
    public class AccesoController : Controller
    {
        static string cadena = "Data Source=(Local);Initial Catalog=DB_Acceso;Integrated Security=true";
        // GET: Acceso
        public ActionResult Login()
        {

            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }


        /// /

        [HttpPost]
        public ActionResult Login(Cliente oCliente)
        {

            oCliente.CliClave = ConvertirSha256(oCliente.CliClave);
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("CliCorreo", oCliente.CliCorreo);
                cmd.Parameters.AddWithValue("CliClave", oCliente.CliClave);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                oCliente.CliId = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            if (oCliente.CliId != 0)
            {
                Session["Cliente"] = oCliente;
                return RedirectToAction("Index", "Home2");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }
        }


        [HttpPost]
        public ActionResult Registrar(Cliente oCliente)
        {
            bool registrado;
            string mensaje;
            if (oCliente.CliClave == oCliente.ConfirmarCliClave)
            {
                oCliente.CliClave = ConvertirSha256(oCliente.CliClave);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("CliCorreo", oCliente.CliCorreo);
                cmd.Parameters.AddWithValue("CliClave", oCliente.CliClave);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();  
                cmd.ExecuteNonQuery();
                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }
            ViewData["Mensaje"] = mensaje;
            if (registrado)
            {
                return RedirectToAction("Login","Acceso");
            }
            else
            {
                return View();
            } 
        }

        //Encriptador de Contraseña
        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }


    }
}