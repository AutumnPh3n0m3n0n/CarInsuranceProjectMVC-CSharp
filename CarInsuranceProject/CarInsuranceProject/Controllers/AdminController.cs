using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarInsuranceProject.ViewModels;

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
                    insurancevm.DayofBirth = signup.dayOfBirth;
                    insurancevm.Year = signup.Year;
                    insurancevm.Brand = signup.Brand;
                    insurancevm.Model = signup.Model;
                    insurancevm.Plate = signup.Plate;
                    signupVMs.Add(insurancevm);
                }
                return View(signups);
            }
        }

    }
}