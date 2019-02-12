using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarInsuranceProject.Models;
using CarInsuranceProject.ViewModels;

namespace CarInsuranceProject.Controllers
{
    public class HomeController : Controller
    {
        //private readonly string connectionString = @"Data Source=DESKTOP-83G96SL\SQLEXPRESS;Initial Catalog=NewsLetter;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(string firstName, string lastName, string emailAddress, int bYear, int bMonth, int bDay, int getYear, string getBrand, string getModel, string getPlate, bool Luxury, bool Dui, int numTickets, bool coverage)
        {
            //if any strings are empty return an exception
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(getBrand) || string.IsNullOrEmpty(getModel) || string.IsNullOrEmpty(getPlate))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            //give error exception if incorrect value is inputted
            if (bMonth < 1 || bMonth > 12 || bDay < 1 || bDay > 31 || bYear < 1900 || bYear > 2005 || getYear < 1920 || getYear > 2020 || numTickets < 0)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            { 
                using (db_CarInsuranceEntities db = new db_CarInsuranceEntities())
                {
                    var signup = new InsuranceCustomer();
                    signup.firstName = firstName;
                    signup.lastName = lastName;
                    signup.emailAddress = emailAddress;
                    signup.Year = getYear;
                    signup.Brand = getBrand;
                    signup.Model = getModel;
                    signup.Plate = getPlate;
                    signup.luxury = Luxury;
                    signup.dayOfBirth = new DateTime(bYear, bMonth, bDay);
                    signup.Dui = Dui;
                    signup.tickets = numTickets;
                    signup.liable = coverage;

                    db.InsuranceCustomers.Add(signup);
                    db.SaveChanges();
                   
                }

                return View("Success");

            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        private ActionResult View(object signups)
        {
            throw new NotImplementedException();
        }
    }
}