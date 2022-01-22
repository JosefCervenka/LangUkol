using Microsoft.AspNetCore.Mvc;
using PaymentsSystem.Data;
using System.Collections.Generic;
using System.Linq;

namespace PaymentsSystem.Controllers
{
    public class PaymentAddController : Controller
    {
        ApplicationDbContext _dBContext;
        public PaymentAddController(ApplicationDbContext dBContext)
        {
            _dBContext = dBContext;
        }
        public IActionResult Index()
        {
            List<string> emails = new List<string>();

            foreach (var item in _dBContext.Users)
            {
                emails.Add(item.Email);
            }
            ViewBag.Users = emails;
            ViewBag.Types = new string[] { "tuition", "fond", "accommodation" }.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Index(int Amount, string)
        {
            System.Console.WriteLine(Amount);
            return RedirectToAction("all", "payment");
        }
    }
}
