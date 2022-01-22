using Microsoft.AspNetCore.Mvc;
using PaymentsSystem.Data;
using System;
using System.Linq;
using PaymentsSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PaymentsSystem.Controllers
{
    public class PaymentController : Controller
    {
        ApplicationDbContext _dBContext;
        public PaymentController(ApplicationDbContext dBContext)
        {
            _dBContext = dBContext;
        }
        [HttpGet]



        [Route("payment/{Type?}/{StudentName?}")]
        public IActionResult Index(string Type, string StudentName)
        {




            ViewBag.Type = Type;
            ViewBag.StudentName = StudentName;

            if (Type == null || Type == "" || Type == "all")
            {
                if (StudentName == null || StudentName == "")
                {
                    ViewBag.payments = _dBContext.payments.Include(x => x.User).Where(x => x.IsPaid == true).ToList();
                    ViewBag.paymentsPrescriptions = _dBContext.payments.Include(x => x.User).Where(x => x.IsPaid == false).ToList();
                }
                else
                {
                    ViewBag.payments = _dBContext.payments.Include(x => x.User).Where(x => x.IsPaid == true).Where(x => x.User.Email.Contains(StudentName)).ToList();
                    ViewBag.paymentsPrescriptions = _dBContext.payments.Include(x => x.User).Where(x => x.IsPaid == false).Where(x => x.User.Email.Contains(StudentName)).ToList();
                }
            }
            else
            {
                if (StudentName == null || StudentName == "")
                {
                    ViewBag.payments = _dBContext.payments.Include(x => x.User).Where(x => x.IsPaid == true).Where(x => x.Type == Type).ToList();
                    ViewBag.paymentsPrescriptions = _dBContext.payments.Include(x => x.User).Where(x => x.IsPaid == false).Where(x => x.Type == Type).ToList();
                }
                else
                {
                    ViewBag.payments = _dBContext.payments.Include(x => x.User).Where(x => x.IsPaid == true).Where(x => x.User.Email.Contains(StudentName)).Where(x => x.Type == Type).ToList();
                    ViewBag.paymentsPrescriptions = _dBContext.payments.Include(x => x.User).Where(x => x.IsPaid == false).Where(x => x.User.Email.Contains(StudentName)).Where(x => x.Type == Type).ToList();
                }
            }


            return View();
        }
        [HttpPost]
        public IActionResult Index(string payAction)
        {

            _dBContext.payments.Include(x => x.User).FirstOrDefault(x => x.Id == Guid.Parse(payAction)).IsPaid = true;
            _dBContext.SaveChanges();
            return RedirectToAction("all", "payment");
        }

        [Route("add/payment")]
        public IActionResult Addpayment()
        {
            List<string> emails = new List<string>();

            foreach (var item in _dBContext.Users)
            {
                emails.Add(item.Email);
            }
            ViewBag.Users = emails;
            ViewBag.Types = new string[]{ "tuition", "fond", "accommodation" }.ToList();

            return View();
        }
        [HttpPost]
        [Route("add/payment")]
        public IActionResult Addpayment(float Amount, string Description, string Type, string User, DateTime date)
        {
            Console.WriteLine("test");
            return View();
        }

    }
}
