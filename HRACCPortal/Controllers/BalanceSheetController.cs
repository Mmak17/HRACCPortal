﻿using HRACCPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRACCPortal.Controllers
{
    public class BalanceSheetController : Controller
    {
        // GET: BalanceSheet
        public ActionResult Index()
        {
            return View();
        }

        private readonly BalanceSheetModel bsmodel;

        public BalanceSheetController()
        {
            bsmodel = new BalanceSheetModel();
        }
        // GET: Position
        [HttpGet]
        public ActionResult ViewBalanceSheet()
        {
            bsmodel.GetBalanceSheetList();
            return View(bsmodel);
        }

        //POST: Add Position
        [HttpPost]
        public ActionResult AddBalanceSheet(BalanceSheetModel bsmodel)
        {
            try
            {
                string status = bsmodel.AddEditBalanceSheet(bsmodel);
                return Json(new { message = status, JsonRequestBehavior.AllowGet });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message, JsonRequestBehavior.AllowGet });
            }

        }
        //POST: Get Position for Edit View
        public ActionResult EditBalanceSheet(int? id)
        {
            try
            {
                BalanceSheetModel balancesheetData = bsmodel.GetBalanceSheetDetailsById(id);

                return Json(new { balancesheetData = balancesheetData, JsonRequestBehavior.AllowGet });
            }
            catch (Exception e)
            {
                return View(e.Message);
            }

        }

    }
}