using Microsoft.AspNetCore.Mvc;
using PaymentsSystem.Data;
using System;
using System.Linq;
using PaymentsSystem.Models;
using Microsoft.EntityFrameworkCore;

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
                if(StudentName == null || StudentName == "")
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
                    ViewBag.payments = _dBContext.payments.Include(x => x.User).Where(x => x.IsPaid == true).Where(x=> x.Type == Type).ToList();
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


    }
}
