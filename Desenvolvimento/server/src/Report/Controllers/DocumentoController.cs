using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;

namespace DemoRotativa.Controllers
{
    public class DocumentoController : Controller
    {
        public IActionResult Oficio()
        {
            return new ViewAsPdf("Oficio");
        }
    }
}