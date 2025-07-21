using Microsoft.AspNetCore.Mvc;

namespace manufacturin_solution_apis.Controllers
{
    public class SeguridadController : Controller
    {
        public IActionResult Salir()
        {
            if (TempData["mySesión"] != null)
            {
                globales.objSesión.Finalizar(TempData["mySesión"].ToString());
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
