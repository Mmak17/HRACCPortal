using HRACCPortal.Edmx;
using HRACCPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRACCPortal.Controllers
{
    public class SubContractorController : Controller
    {
        // GET: SubContractor
        public HRACCDBEntities entities;
        clsCrud cls;
        public SubContractorController()
        {
            entities = new HRACCDBEntities();
            cls = new clsCrud();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddSubContractor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSubContractor(SubContractorModel subContractor)
        {


            string message = "";
            try
            {
                message = cls.AddSubContractor(subContractor);
            }
            catch (Exception e)
            {
                message = e.Message;
            }


            return Json(new { message = message, JsonRequestBehavior.AllowGet });
        }


        public ActionResult ViewSubContractors()
        {
            cls.GetSubControllers();
            return View(cls);
        }
        public ActionResult EditSubContractor(int id)
        {
            CustomerModel cl = cls.GetSubContractorById(id);
            return Json(new { cl = cl, JsonRequestBehavior.AllowGet });
        }
    }
}
}