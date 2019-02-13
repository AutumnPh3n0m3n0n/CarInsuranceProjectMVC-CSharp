using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarInsuranceProject.ViewModels;
using CarInsuranceProject.Models;

namespace CarInsuranceProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (db_CarInsuranceEntities db = new db_CarInsuranceEntities())
            {
                var signups = db.InsuranceCustomers;
                var signupVMs = new List<CarInsuranceVMs>();
                foreach (var signup in signups)
                {
                    var insurancevm = new CarInsuranceVMs();
                    insurancevm.FirstName = signup.firstName;
                    insurancevm.LastName = signup.lastName;
                    insurancevm.EmailAddress = signup.emailAddress;
                    signupVMs.Add(insurancevm);
                }
                return View(signupVMs);
            }
        }

    }
}