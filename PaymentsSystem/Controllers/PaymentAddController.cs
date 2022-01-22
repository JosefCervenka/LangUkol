using Microsoft.AspNetCore.Mvc;
using PaymentsSystem.Data;
using System;
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
        public IActionResult Index(float Amount, string Description, string Type, string User, DateTime Date)
        {
            //System.Console.WriteLine(Amount + Description + Type + User + Date);

            _dBContext.payments.Add(new Models.Payment { User = _dBContext.Users.FirstOrDefault(x => x.Email == User), Amount = Amount, Description = Description, Type = Type });
            _dBContext.SaveChanges();

            return RedirectToAction("all", "payment");
        }
    }
}
