using Microsoft.AspNetCore.Mvc;

namespace eAgenda.WebApp.Modulos.ModuloHome.Apresentacao;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}
