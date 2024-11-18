using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TeacherController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}