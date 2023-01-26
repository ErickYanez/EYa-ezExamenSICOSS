using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class CalculadoraController : Controller
    {
        [HttpGet]
        public ActionResult Historial(int IdUsuario, string Numero, int Resultado)
        {
            ML.Calculadora calculadora = new ML.Calculadora();
            ML.Result result = BL.Calculadora.GetAll(IdUsuario);
            calculadora.Usuario = new ML.Usuario();            
            if (result.Correct)
            {
                ViewBag.Numero = Numero;
                ViewBag.Resultado = Resultado;
                calculadora.Calculadoras = result.Objects;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
            }
            return View(calculadora);
        }

        [HttpPost]
        public ActionResult SuperDigito(int IdUsuario, string Numero)
        {
            //ADD
            ML.Calculadora calculadora = new ML.Calculadora();
            calculadora.Usuario = new ML.Usuario();
            calculadora.Usuario.IdUsuario = IdUsuario;
            calculadora.Numero = Numero;
            calculadora.Resultado = BL.Calculadora.SumarDigitos(Numero);
            ML.Result result1 = BL.Calculadora.VerificarNumero(Numero);
            if (result1.Correct != true){
                ML.Result result = BL.Calculadora.Add(calculadora);
                if (result.Correct)
                {
                    return RedirectToAction("Historial", "Calculadora", new { IdUsuario = calculadora.Usuario.IdUsuario, Numero = calculadora.Numero, Resultado = calculadora.Resultado });
                }
                else
                {
                    ViewBag.Message = "Error: " + result.Message;
                    return View(result);
                }
            }
            else
            {
                ML.Result result2 = BL.Calculadora.ActualizarFecha(calculadora);
                if (result2.Correct)
                {
                    calculadora = (ML.Calculadora)result2.Object;
                    return RedirectToAction("Historial", "Calculadora", new { IdUsuario = calculadora.Usuario.IdUsuario, Numero = calculadora.Numero, Resultado = calculadora.Resultado });
                }
                else
                {
                    ViewBag.Message = "Error: " + result2.Message;
                    return View(result2);
                }
            }
        }

        public ActionResult Delete(int IdUsuario, int IdCalculadora)
        {
            ML.Calculadora calculadora = new ML.Calculadora();
            calculadora.Usuario = new ML.Usuario();
            ML.Result result = BL.Calculadora.Delete(IdUsuario, IdCalculadora);
            if (calculadora != null)
            {
                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                    ViewBag.IdUsuario = IdUsuario;
                }
                else
                {
                    ViewBag.Message = "Error: " + result.Message;
                }
            }
            else
            {
                return Redirect("/Calculadora/Historial");
            }
            return PartialView("Modal");
        }
    }
}
