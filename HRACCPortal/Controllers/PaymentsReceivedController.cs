using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRACCPortal.Edmx;
using HRACCPortal.Models;

namespace HRACCPortal.Controllers
{
    [Authorize]
    public class PaymentsReceivedController : Controller
    {
        // GET: Customer
        public HRACCDBEntities entities;
        clsCrud cls;
        public PaymentsReceivedController()
        {
            entities = new HRACCDBEntities();
            cls = new clsCrud();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPaymentsReceived()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPaymentsReceived(PaymentsReceivedModel PaymentsReceived)
        {


            string message = "";
            try
            {
                message = cls.AddPaymentsReceived(PaymentsReceived);
            }
            catch (Exception e)
            {
                message = e.Message;
            }


            return Json(new { message = message, JsonRequestBehavior.AllowGet });
        }


        public ActionResult ViewPaymentsReceived()
        {
            cls.GetPaymentsReceived();
            return View(cls);
        }
        public ActionResult EditPaymentsReceived(int id)
        {
            PaymentsReceivedModel cl = cls.GetPaymentsReceivedById(id);
            return Json(new { cl = cl, JsonRequestBehavior.AllowGet });
        }

    }
}