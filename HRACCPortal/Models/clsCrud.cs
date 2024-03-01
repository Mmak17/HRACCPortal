﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRACCPortal.Edmx;
using HRACCPortal.ObjectModel;

namespace HRACCPortal.Models
{
    public class clsCrud
    {
        public HRACCDBEntities entities;
        public CustomerModel customerModel;
        public ConsultantModel consultantModel;
        public InvoiceModel invoiceModel;
        public InvoiceObjectModel invoiceObjectModel;
        public ConsultantPositionDetailsModel consultantPositionDetailsModel;
        public clsCrud()
        {
            customerModel = new CustomerModel();
            consultantModel = new ConsultantModel();
            invoiceModel = new InvoiceModel();
            invoiceObjectModel = new InvoiceObjectModel();
            consultantPositionDetailsModel = new ConsultantPositionDetailsModel();
            entities = new HRACCDBEntities();
        }
        public List<CustomerModel> CustomerList { get; set; }
        public List<ConsultantModel> ConsultantList { get; set; }
        public List<ConsultantPositionDetailsModel> ConsultantPositionDetailsList { get; set; }

        public IEnumerable<SelectListItem> ddlPositionsList
        {
            get
            {
                var customer = entities.Positions.AsEnumerable().ToList();
                IEnumerable<SelectListItem> items = from value in customer
                                                    select new SelectListItem
                                                    {
                                                        Text = value.PositionTitle,
                                                        Value = value.PositionIdPK.ToString(),
                                                    };
                return items;
            }
        }
        public IEnumerable<SelectListItem> ddlPositionsRateList
        {
            get
            {
                var customer = entities.PositionRates.AsEnumerable().ToList();
                IEnumerable<SelectListItem> items = from value in customer
                                                    select new SelectListItem
                                                    {
                                                        Text = value.Rate.ToString(),
                                                        Value = value.PositionRateIdPK.ToString(),
                                                    };
                return items;
            }
        }
        public IEnumerable<SelectListItem> ddlConsultantsList
        {
            get
            {
                var customer = entities.Consultants.AsEnumerable().ToList();
                IEnumerable<SelectListItem> items = from value in customer
                                                    select new SelectListItem
                                                    {
                                                        Text = value.FirstName,
                                                        Value = value.ConsultantIdPK.ToString(),
                                                    };
                return items;
            }
        }

        #region Custoemr
        public string AddCustomer(CustomerModel customer)
        {
            if (customer.CustomerIdPK > 0)
            {
                var customerModel = entities.Customers.Where(x => x.CustomerIdPK == customer.CustomerIdPK).FirstOrDefault();
                customerModel.AddedBy = "Admin";
                customerModel.CustomerContactAddress1 = customer.CustomerContactAddress1;
                customerModel.CustomerContactAddress2 = customer.CustomerContactAddress2;
                customerModel.CustomerContactCity = customer.CustomerContactCity;
                customerModel.CustomerContactEmail = customer.CustomerContactEmail;
                customerModel.CustomerContactPhone = customer.CustomerContactPhone;
                customerModel.CustomerContactState = customer.CustomerContactState;
                customerModel.CustomerContactZip = customer.CustomerContactZip;
                customerModel.CustomerName = customer.CustomerName;
                if (!string.IsNullOrEmpty(customer.CustomerName))
                {
                    customerModel.CustomerTerm = customer.CustomerTerm;
                }
                else
                {
                    customerModel.CustomerTerm = "30";
                }
                customerModel.DateUpdated = DateTime.Now.ToString("MM/dd/yyyy").Replace("-", "/");
                customerModel.UpdatedBy = "ADMIN";
                customerModel.CustomerIdPK = customer.CustomerIdPK;
                customerModel.isActive = customer.isActive;
                int i = entities.SaveChanges();
                if (i > 0)
                {
                    return "updated";
                }
                else
                {
                    return "fail";
                }
            }
            else
            {
                var cons = entities.Consultants.Where(x => x.Email == customer.CustomerContactEmail).ToList();
                if (cons.Count > 0)
                {
                    return "Email already exist";
                }

                Customer scust = new Customer
                {
                    AddedBy = customer.AddedBy,
                    CustomerContactAddress1 = customer.CustomerContactAddress1,
                    CustomerContactAddress2 = customer.CustomerContactAddress2,
                    CustomerContactCity = customer.CustomerContactCity,
                    CustomerContactEmail = customer.CustomerContactEmail,
                    CustomerContactPhone = customer.CustomerContactPhone,
                    CustomerContactState = customer.CustomerContactState,
                    CustomerContactZip = customer.CustomerContactZip,
                    CustomerName = customer.CustomerName,
                    CustomerTerm = customer.CustomerTerm,
                    DateAdded = DateTime.Now.ToString("MM/dd/yyyy").Replace("-", "/"),
                    DateUpdated = DateTime.Now.ToString("MM/dd/yyyy").Replace("-", "/"),
                    UpdatedBy = "Admin",
                    isActive = customer.isActive
                };

                entities.Customers.AddObject(scust);
                int i = entities.SaveChanges();
                if (i > 0)
                {
                    return "success";
                }
                else
                {
                    return "fail";
                }
            }
        }
        public void GetCustomers()
        {
            CustomerList = (from g in entities.Customers
                            select g
                               ).AsEnumerable().Select(customer => new CustomerModel
                               {
                                   AddedBy = customer.AddedBy,
                                   CustomerContactAddress1 = customer.CustomerContactAddress1,
                                   CustomerContactAddress2 = customer.CustomerContactAddress2,
                                   CustomerContactCity = customer.CustomerContactCity,
                                   CustomerContactEmail = customer.CustomerContactEmail,
                                   CustomerContactPhone = customer.CustomerContactPhone,
                                   CustomerContactState = customer.CustomerContactState,
                                   CustomerContactZip = customer.CustomerContactZip,
                                   CustomerName = customer.CustomerName,
                                   CustomerTerm = customer.CustomerTerm,
                                   DateAdded = customer.DateAdded,
                                  // DateUpdated = Convert.ToDateTime(customer.DateUpdated).ToString("MMM,dd, yyyy"),
                                   DateUpdated= DateTime.Now.ToString("MMM,dd,yyyy"),
                                   // DateUpdated = customer.DateUpdated,
                                   UpdatedBy = customer.UpdatedBy,
                                   CustomerIdPK = customer.CustomerIdPK,
                                   isActive = customer.isActive
                               }).ToList();
        }
        public CustomerModel GetCustomerById(int id)
        {
            var customer = entities.Customers.Where(x => x.CustomerIdPK == id).FirstOrDefault();
            customerModel.AddedBy = customer.AddedBy;
            customerModel.CustomerContactAddress1 = customer.CustomerContactAddress1;
            customerModel.CustomerContactAddress2 = customer.CustomerContactAddress2;
            customerModel.CustomerContactCity = customer.CustomerContactCity;
            customerModel.CustomerContactEmail = customer.CustomerContactEmail;
            customerModel.CustomerContactPhone = customer.CustomerContactPhone;
            customerModel.CustomerContactState = customer.CustomerContactState;
            customerModel.CustomerContactZip = customer.CustomerContactZip;
            customerModel.CustomerName = customer.CustomerName;
            customerModel.CustomerTerm = customer.CustomerTerm;
            customerModel.DateAdded = customer.DateAdded;
            customerModel.DateUpdated = customer.DateUpdated;
            customerModel.UpdatedBy = customer.UpdatedBy;
            customerModel.CustomerIdPK = customer.CustomerIdPK;
            customerModel.isActive = customer.isActive;
            return customerModel;
        }

        #endregion

        #region Consultant
        public string AddConsultant(ConsultantModel obj)
        {

            if (obj.ConsultantIdPK > 0)
            {
                var obj1 = entities.Consultants.Where(x => x.ConsultantIdPK == obj.ConsultantIdPK).FirstOrDefault();
                obj1.Active = obj.Active;
                obj1.Address1 = obj.Address1;
                obj1.Address2 = obj.Address2;
                obj1.City = obj.City;
                obj1.ConsultantNameAbbrv = obj.ConsultantNameAbbrv;
                obj1.Email = obj.Email;
                obj1.FirstName = obj.FirstName;
                obj1.InactiveDate = obj.InactiveDate;
                obj1.InactiveReason = obj.InactiveReason;
                obj1.LastName = obj.LastName;
                obj1.MiddleName = obj.MiddleName;
                obj1.Phone = obj.Phone;
                obj1.StartDate = obj.StartDate;
                obj1.DateUpdated = DateTime.Now.ToString();
                obj1.UpdatedBy = "Admin";
                obj1.State = obj.State;
                obj1.Title = obj.Title;
                obj1.Zip = obj.Zip;
                int i = entities.SaveChanges();
                if (i > 0)
                {
                    return "updated";
                }
                else
                {
                    return "fail";
                }

            }
            else
            {
                var cons = entities.Consultants.Where(x => x.Email == obj.Email).ToList();
                if (cons.Count > 0)
                {
                    return "Email already exist";
                }
                Consultant eobj = new Consultant
                {
                    AddedBy = "Admin",
                    Active = obj.Active,
                    Address1 = obj.Address1,
                    Address2 = obj.Address2,
                    City = obj.City,
                    ConsultantNameAbbrv = obj.ConsultantNameAbbrv,
                    Email = obj.Email,
                    FirstName = obj.FirstName,
                    InactiveDate = obj.InactiveDate,
                    InactiveReason = obj.InactiveReason,
                    LastName = obj.LastName,
                    MiddleName = obj.MiddleName,
                    Phone = obj.Phone,
                    StartDate = obj.StartDate,
                    DateAdded = DateTime.Now.ToString("MM/dd/yyyy").Replace("-", "/"),
                    DateUpdated = DateTime.Now.ToString("MM/dd/yyyy").Replace("-", "/"),
                    UpdatedBy = "Admin",
                    State = obj.State,
                    Title = obj.Title,
                    Zip = obj.Zip
                };
                entities.Consultants.AddObject(eobj);

                int i = entities.SaveChanges();
                if (i > 0)
                {
                    return "success";
                }
                else
                {
                    return "fail";
                }
            }
        }
        public void GetConsultants()
        {
            ConsultantList = (from g in entities.Consultants
                              select g
                               ).AsEnumerable().Select(obj => new ConsultantModel
                               {
                                   AddedBy = obj.AddedBy,
                                   Active = obj.Active,
                                   Address1 = obj.Address1,
                                   Address2 = obj.Address2,
                                   City = obj.City,
                                   ConsultantNameAbbrv = obj.ConsultantNameAbbrv,
                                   Email = obj.Email,
                                   FirstName = obj.FirstName,
                                   InactiveDate = obj.InactiveDate,
                                   InactiveReason = obj.InactiveReason,
                                   LastName = obj.LastName,
                                   MiddleName = obj.MiddleName,
                                   Phone = obj.Phone,
                                   StartDate = obj.StartDate,
                                   DateAdded = obj.DateAdded, //string.IsNullOrEmpty(obj.DateAdded) == true ? DateTime.Now.ToString("MM/dd/yyyy") : DateTime.Parse(obj.DateAdded).ToString("MM/dd/yyyy"),
                                                              //  DateUpdated = obj.DateUpdated,
                                   //DateUpdated = Convert.ToDateTime(obj.DateUpdated).ToString("MMM,dd, yyyy"),
                                   DateUpdated= DateTime.Now.ToString("MMM,dd,yyyy"),
                                   UpdatedBy = obj.UpdatedBy,
                                   State = obj.State,
                                   Title = obj.Title,
                                   Zip = obj.Zip,
                                   ConsultantIdPK = obj.ConsultantIdPK
                               }).ToList();
        }
        public ConsultantModel GetConsultantById(int id)
        {
            var obj = entities.Consultants.Where(x => x.ConsultantIdPK == id).FirstOrDefault();
            consultantModel.AddedBy = obj.AddedBy;
            consultantModel.Active = obj.Active;
            consultantModel.Address1 = obj.Address1;
            consultantModel.Address2 = obj.Address2;
            consultantModel.City = obj.City;
            consultantModel.ConsultantNameAbbrv = obj.ConsultantNameAbbrv;
            consultantModel.Email = obj.Email;
            consultantModel.FirstName = obj.FirstName;
            consultantModel.InactiveDate = string.IsNullOrEmpty(obj.InactiveDate) == true ? DateTime.Now.ToString("dd/MM/yyyy") : obj.InactiveDate;
            consultantModel.InactiveReason = obj.InactiveReason;
            consultantModel.LastName = obj.LastName;
            consultantModel.MiddleName = obj.MiddleName;
            consultantModel.Phone = obj.Phone;
            consultantModel.StartDate = string.IsNullOrEmpty(obj.StartDate) == true ? DateTime.Now.ToString("dd/MM/yyyy") : obj.StartDate;
            consultantModel.DateAdded = string.IsNullOrEmpty(obj.DateAdded) == true ? DateTime.Now.ToString("dd/MM/yyyy") : obj.DateAdded;
            //   consultantModel.DateUpdated = obj.DateUpdated;
            // consultantModel.UpdatedBy = obj.UpdatedBy;
            consultantModel.State = obj.State;
            consultantModel.Title = obj.Title;
            consultantModel.Zip = obj.Zip;
            consultantModel.ConsultantIdPK = obj.ConsultantIdPK;
            return consultantModel;
        }
        #endregion

        #region consultantPositionDetails

        public void GetconsultantPositionDetails()
        {
            ConsultantPositionDetailsList = (from g in entities.ConsultantPositionDetails
                                             select g
                               ).AsEnumerable().Select(obj => new ConsultantPositionDetailsModel
                               {
                                   ConsultantPositionIdPK = obj.ConsultantPositionIdPK,
                                   ConsultantIdFK = obj.ConsultantIdFK,
                                   PositionIdFK = obj.PositionIdFK,
                                   PositionRateIdFK = obj.PositionRateIdFK,
                                   PositionStartDate = obj.PositionStartDate,
                                   PositionEndDate = obj.PositionEndDate,
                                   PositionActive = obj.PositionActive,
                                   AddedBy = obj.AddedBy,
                                   DateAdded = obj.DateAdded, //string.IsNullOrEmpty(obj.DateAdded) == true ? DateTime.Now.ToString("MM/dd/yyyy") : DateTime.Parse(obj.DateAdded).ToString("MM/dd/yyyy"),
                                   DateUpdated = Convert.ToDateTime(obj.DateUpdated).ToString("MMM,dd, yyyy"),
                                   UpdatedBy = obj.UpdatedBy,

                               }).ToList();
        }

        public ConsultantPositionDetailsModel GetConsultantPositionDetailsById(int id)
        {
            var obj = entities.ConsultantPositionDetails.Where(x => x.ConsultantPositionIdPK == id).FirstOrDefault();
            consultantPositionDetailsModel.AddedBy = obj.AddedBy;
            consultantPositionDetailsModel.PositionActive = obj.PositionActive;
            consultantPositionDetailsModel.PositionStartDate = string.IsNullOrEmpty(obj.PositionStartDate) == true ? DateTime.Now.ToString("dd/MM/yyyy") : obj.PositionStartDate;
            consultantPositionDetailsModel.DateAdded = string.IsNullOrEmpty(obj.DateAdded) == true ? DateTime.Now.ToString("dd/MM/yyyy") : obj.DateAdded;

            consultantPositionDetailsModel.PositionEndDate = obj.PositionEndDate;
            consultantPositionDetailsModel.ConsultantPositionIdPK = obj.ConsultantPositionIdPK;
            consultantPositionDetailsModel.ConsultantIdFK = obj.ConsultantIdFK;
            consultantPositionDetailsModel.PositionIdFK = obj.PositionIdFK;
            consultantPositionDetailsModel.PositionRateIdFK = obj.PositionRateIdFK;
            return consultantPositionDetailsModel;
        }

        public string AddConsultantPositionDetails(ConsultantPositionDetailsModel obj)
        {

            if (obj.ConsultantPositionIdPK > 0)
            {
                var obj1 = entities.ConsultantPositionDetails.Where(x => x.ConsultantPositionIdPK == obj.ConsultantPositionIdPK).FirstOrDefault();
                obj1.PositionActive = obj.PositionActive;
                obj1.AddedBy = "Admin";
                obj1.ConsultantIdFK = obj.ConsultantIdFK;
                obj1.PositionIdFK = obj.PositionIdFK;
                obj1.PositionRateIdFK = obj.PositionRateIdFK;
                obj1.PositionStartDate = obj.PositionStartDate;
                obj1.PositionEndDate = obj.PositionEndDate;
                obj1.DateAdded = DateTime.Now.ToString();
                obj1.DateUpdated = DateTime.Now.ToString();
                obj1.UpdatedBy = "Admin";

                int i = entities.SaveChanges();
                if (i > 0)
                {
                    return "updated";
                }
                else
                {
                    return "fail";
                }

            }
            else
            {

                ConsultantPositionDetail eobj = new ConsultantPositionDetail
                {
                    PositionActive = obj.PositionActive,
                    AddedBy = "Admin",
                    ConsultantIdFK = obj.ConsultantIdFK,
                    PositionIdFK = obj.PositionIdFK,
                    PositionRateIdFK = obj.PositionRateIdFK,
                    PositionStartDate = obj.PositionStartDate,
                    PositionEndDate = obj.PositionEndDate,
                    DateAdded = DateTime.Now.ToString(),
                    DateUpdated = DateTime.Now.ToString(),
                    UpdatedBy = "Admin",

                };
                entities.ConsultantPositionDetails.AddObject(eobj);

                int i = entities.SaveChanges();
                if (i > 0)
                {
                    return "success";
                }
                else
                {
                    return "fail";
                }
            }
        }

        #endregion
        #region Invoice pdf

        public InvoicePdfModel GenratePdf(int id)
        {
            InvoicePdfModel invoicePdfModel = new InvoicePdfModel();
            invoicePdfModel.invDetails = (from inv in entities.Invoices
                                          join cpd in entities.ConsultantPositionDetails on inv.ConsultantPositionIdFK equals cpd.ConsultantPositionIdPK
                                          join ct in entities.Consultants on cpd.ConsultantIdFK equals ct.ConsultantIdPK
                                          join p in entities.Positions on cpd.PositionIdFK equals p.PositionIdPK
                                          join pr in entities.PositionRates on p.PositionIdPK equals pr.PositionIdFK
                                          join c in entities.Customers on p.CustomerIdFK equals c.CustomerIdPK
                                          where inv.InvoiceIdPK == id
                                          select new { inv, cpd, p, c, ct, pr }).AsEnumerable().Select(x => new InvoicePdfModel
                                          {
                                              CustomerName = x.c.CustomerName,
                                              CustomerContactAddress1 = x.c.CustomerContactAddress1,
                                              CustomerContactAddress2 = x.c.CustomerContactAddress2,
                                              CustomerContactCity = x.c.CustomerContactCity,
                                              CustomerContactState = x.c.CustomerContactState,
                                              CustomerContactZip = x.c.CustomerContactZip,
                                              CustomerContactEmail = x.c.CustomerContactEmail,
                                              CustomerContactPhone = x.c.CustomerContactPhone,

                                              FirstName = x.ct.FirstName,
                                              LastName = x.ct.LastName,
                                              MiddleName = x.ct.MiddleName,

                                              PositionTitle = x.p.PositionTitle,
                                              PurchaseOrderNo = x.p.PurchaseOrderNo,
                                              PositionNumber = x.p.PositionNumber,
                                              PositionFamily = x.p.PositionFamily,
                                              PositionScopeVariant = x.p.PositionScopeVariant,

                                              RegularHours = x.inv.RegularHours,
                                              Rate = x.pr.Rate,

                                              TotalAmount = Convert.ToDecimal(x.inv.RegularHours) * Convert.ToDecimal(x.pr.Rate),
                                              InvoiceNo = x.inv.Month + x.inv.Year.Substring(Math.Max(x.inv.Year.Length - 2, 0)) + x.p.PurchaseOrderNo + x.p.PurchaseOrderNo.Substring(Math.Max(x.p.PurchaseOrderNo.Length - 4, 0)),
                                              InvoiceIdPK = x.inv.InvoiceIdPK,

                                              ConsultatntName = x.ct.FirstName + x.ct.LastName,
                                              Month = x.inv.Month,
                                              Year = x.inv.Year,
                                              MonthStartDate = FirstDayOfMonthFromDateTime(x.inv.Year, x.inv.Month),
                                              MonthEndDate = LastDayOfMonthFromDateTime(x.inv.Year, x.inv.Month),
                                              DateAdded = x.inv.DateAdded,
                                           //   InvoiceDate = Convert.ToDateTime(x.inv.InvoiceDate).ToString("MMM,dd, yyyy"),
                                              InvoiceDate = DateTime.Now.ToString("MMM,dd,yyyy"),
                                              DueDate = string.IsNullOrEmpty(x.inv.DueDate) ? "" : DateTime.Now.ToString("MMM,dd,yyyy"),
                                            //  DueDate = string.IsNullOrEmpty(x.inv.DueDate) ? "" : Convert.ToDateTime(x.inv.DueDate).ToString("MMM,dd, yyyy"),
                                              Term = x.c.CustomerTerm,

                                          }).ToList();


            var comapany = entities.Companies.Where(x => x.CompanyIdPK == 1).FirstOrDefault();
            invoicePdfModel.CompanyName = comapany.CompanyName;
            invoicePdfModel.CompanyPhone = comapany.CompanyPhone;
            invoicePdfModel.CompanyEmail = comapany.CompanyEmail;
            invoicePdfModel.CompanyAddress1 = comapany.CompanyAddress1;
            invoicePdfModel.CompanyAddress2 = comapany.CompanyAddress2;
            invoicePdfModel.CompanyCity = comapany.CompanyCity;
            invoicePdfModel.CompanyState = comapany.CompanyState;
            invoicePdfModel.CompanyZip = comapany.CompanyZip;
            invoicePdfModel.CompanyFax = comapany.CompanyFax;
            return invoicePdfModel;
        }
        public string FirstDayOfMonthFromDateTime(string syear, string smonth)
        {
            int year = string.IsNullOrEmpty(syear) ? 0 : Convert.ToInt32(syear);
            int month = string.IsNullOrEmpty(smonth) ? 0 : Convert.ToInt32(smonth);
            DateTime firstDayOfTheMonth = new DateTime(year, month, 1);
            var startDate = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
            //  var endDate = startDate.AddMonths(1).AddDays(-1);
            return Convert.ToDateTime(firstDayOfTheMonth).ToString("MMM,dd, yyyy");
        }
        public string LastDayOfMonthFromDateTime(string syear, string smonth)
        {
            int year = string.IsNullOrEmpty(syear) ? 0 : Convert.ToInt32(syear);
            int month = string.IsNullOrEmpty(smonth) ? 0 : Convert.ToInt32(smonth);
            DateTime firstDayOfTheMonth = new DateTime(year, month, 1);
            var startDate = firstDayOfTheMonth.AddMonths(1).AddDays(-1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            return Convert.ToDateTime(startDate).ToString("MMM,dd, yyyy");

        }
        #endregion
    }
}