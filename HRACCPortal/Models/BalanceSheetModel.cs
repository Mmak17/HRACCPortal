using HRACCPortal.Edmx;
using HRACCPortal.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRACCPortal.Models
{
    public class BalanceSheetModel : BalanceSheetObjectModel
    {
        private HRACCDBEntities hRACCDBEntities;

        public BalanceSheetModel()
        {
            hRACCDBEntities = new HRACCDBEntities();
        }

        public IEnumerable<SelectListItem> ddlCustomerList
        {
            get
            {
                var customer = hRACCDBEntities.Customers.AsEnumerable().ToList();
                IEnumerable<SelectListItem> items = from value in customer
                                                    select new SelectListItem
                                                    {
                                                        Text = value.CustomerName,
                                                        Value = value.CustomerIdPK.ToString(),
                                                    };
                return items;
            }
        }
        public string AddEditBalanceSheet(BalanceSheetModel model)
        {
            string statusDetail = string.Empty;
            if (model.BalanceSheetId == 0)
            {
                BalanceSheet bs = new BalanceSheet()
                {
                    CustomerIdFK = model.CustomerIdFK,
                    Year = model.Year,
                    Month = model.Month,
                    InvoiceNumber = model.InvoiceNumber,
                    InvoiceAmount = model.InvoiceAmount,
                    PaymentReceived = model.PaymentReceived,
                    Balance = model.Balance,
                    DateAdded = IndianTimeNow,
                    DateUpdated = IndianTimeNow,
                    AddedBy = "ADMIN",
                    UpdatedBy = "ADMIN"
                };

                hRACCDBEntities.BalanceSheets.AddObject(bs);
                statusDetail = hRACCDBEntities.SaveChanges() == 1 ? "Success" : "Failed";
            }
            else
            {
                var updateBalanceSheet = hRACCDBEntities.BalanceSheets.Where(x => x.BalanceSheetId == model.BalanceSheetId).FirstOrDefault();
                updateBalanceSheet.CustomerIdFK = model.CustomerIdFK;
                updateBalanceSheet.Year = model.Month;
                updateBalanceSheet.Month = model.Month;
                updateBalanceSheet.InvoiceNumber = model.InvoiceNumber;
                updateBalanceSheet.InvoiceAmount = model.InvoiceAmount;
                updateBalanceSheet.PaymentReceived = model.PaymentReceived;
                updateBalanceSheet.Balance = model.Balance;
                updateBalanceSheet.DateUpdated = IndianTimeNow;
                updateBalanceSheet.UpdatedBy = "ADMIN";
                statusDetail = hRACCDBEntities.SaveChanges() == 1 ? "Success" : "Failed";
            }
            return statusDetail;
        }
        //Edit Position
        public BalanceSheetModel GetBalanceSheetDetailsById(int? balancesheetId)
        {
            var balancesheetData = hRACCDBEntities.BalanceSheets.Single(x => x.BalanceSheetId == balancesheetId);
            var customerData = hRACCDBEntities.Customers.Where(x => x.CustomerIdPK == balancesheetData.CustomerIdFK).FirstOrDefault();
           BalanceSheetModel balancesheetModel = new BalanceSheetModel()
            {
                CustomerIdFK = balancesheetData.CustomerIdFK,
                CustomerName = customerData.CustomerName,
                BalanceSheetId = balancesheetData.BalanceSheetId,
                Year = balancesheetData.Year,
                Month = balancesheetData.Month,
                InvoiceNumber = balancesheetData.InvoiceNumber,
                InvoiceAmount = balancesheetData.InvoiceAmount,
                PaymentReceived = balancesheetData.PaymentReceived,
                Balance = balancesheetData.Balance,
                DateUpdated = balancesheetData.DateUpdated,
                UpdatedBy = "ADMIN"
            };
            return balancesheetModel;
        }
        //view position
        public void GetBalanceSheetList()
        {
            BalanceSheetList = (from bsLts in hRACCDBEntities.BalanceSheets
                            join custLts in hRACCDBEntities.Customers
                            on bsLts.CustomerIdFK equals custLts.CustomerIdPK
                            select new
                            {
                                bsLts.BalanceSheetId,
                                bsLts.CustomerIdFK,
                                bsLts.Year,
                                bsLts.Month,
                                bsLts.InvoiceNumber,
                                bsLts.InvoiceAmount,
                                bsLts.PaymentReceived,
                                bsLts.Balance,
                                bsLts.DateAdded,
                                bsLts.DateUpdated,
                                bsLts.AddedBy,
                                bsLts.UpdatedBy,
                                custLts.CustomerName

                            }).AsEnumerable().Select(i => new BalanceSheetObjectModel
                            {
                                BalanceSheetId = i.BalanceSheetId,
                                CustomerName = i.CustomerName,
                                Year = i.Year,
                                Month = i.Month,
                                InvoiceNumber = i.InvoiceNumber,
                                InvoiceAmount = i.InvoiceAmount,
                                PaymentReceived = i.PaymentReceived,
                                Balance = i.Balance,
                                DateAdded = i.DateAdded,
                                DateUpdated = Convert.ToDateTime(i.DateUpdated).ToString("MMM,dd, yyyy"),
                                AddedBy = i.AddedBy,
                                UpdatedBy = i.UpdatedBy

                            }).ToList();
        }

    }
}