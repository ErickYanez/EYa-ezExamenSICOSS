using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            ML.Result result = BL.Usuario.Login(userName);
            if (result.Correct == true)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (usuario.Password == password)
                {                    
                    return RedirectToAction("Historial", "Calculadora", new {IdUsuario = usuario.IdUsuario});
                }
                else
                {
                    ViewBag.Message = "Contraseña incorrecta";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Message = "" + result.Message;
                return PartialView("Modal");
            }
        }

        [HttpGet]
        public ActionResult Form()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);            
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            //ADD
            ML.Result result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                ViewBag.Message = result.Message;
            }
            else
            {
                ViewBag.Message = "Error: " + result.Message;
            }
            return PartialView("Modal");
        }
    }
}
