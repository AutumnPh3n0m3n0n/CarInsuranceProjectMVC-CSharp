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
        public ActionResult SignUp(string firstName, string lastName, string emailAddress, string bYear, string bMonth, string bDay, string getYear, string getBrand, string getModel, string getPlate, string Luxury, string Dui, string numTickets, string coverage)
        {
            //use exception handling. if empty or bad data is inputted go to the error page
            try
            {
                int byear = Convert.ToInt32(bYear);
                int bmonth = Convert.ToInt32(bMonth);
                int bday = Convert.ToInt32(bDay);
                int caryear = Convert.ToInt32(getYear);
                int numtickets = Convert.ToInt32(numTickets);
                bool lux;
                bool liability;
                bool dui;
                if (Luxury == "Y" || Luxury == "y" || Luxury == "Yes" || Luxury == "yes")
                    lux = true;
                else
                    lux = false;
                if (coverage == "Y" || coverage == "y" || coverage == "Yes" || coverage == "yes")
                    liability = true;
                else
                    liability = false;
                if (Dui == "Y" || Dui == "y" || Dui == "Yes" || Dui == "yes")
                    dui = true;
                else
                    dui = false;

                //give error exception if incorrect value is inputted
                if (bmonth < 1 || bmonth > 12 || bday < 1 || bday > 31 || byear < 1900 || byear > 2005 || caryear < 1920 || caryear > 2020 || numtickets < 0)
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
                else
                {
                    DateTime birthAssign = new DateTime(byear, bmonth, bday);
                    int InsuranceCost = 50;
                    DateTime toDate = DateTime.Now;
                    int age = (int)(toDate.Year - birthAssign.Year);

                    if (birthAssign > toDate.AddYears(-age))
                        age--;

                    //add $25 to the insurance if user age is 100 or above
                    if (age >= 100)
                    {
                        Console.WriteLine("Since the user is over 100, $25 has been added to the monthly insurance.");
                        InsuranceCost += 25;
                    }

                    //add $25 to the insurance if user age is less than 25
                    if (age < 25)
                    {
                    //have an if statement to prevent a duplicate message incase user is under 18
                        if (age >= 18)
                            Console.WriteLine("Since the user is under 25, $25 has been added to the monthly insurance.");
                        InsuranceCost += 25;
                    }

                    //add another $75 or $100 total to the insurance if user age is under 18
                    if (age < 18)
                    {
                        Console.WriteLine("Since the user is under 18, $100 has been added to the monthly insurance.");
                        InsuranceCost += 75;
                    }

                    if (caryear < 2000)
                    {
                        Console.WriteLine("Since the car was made before 2000, $25 has been added to the monthly insurance.");
                        InsuranceCost += 25;
                    }

                    if (caryear > 2015)
                    {
                        Console.WriteLine("Since the car was made 2015, $25 has been added to the monthly insurance.");
                        InsuranceCost += 25;
                    }

                    int speedingCost = numtickets * 10;
                    if (numtickets > 0)
                    {
                        Console.WriteLine("You have {0} speeding tickets, so ${1} has been added to your monthly insurance", numtickets, speedingCost);
                    }
                    InsuranceCost += speedingCost;

                    //increment $25 to the cost if car is a porsche
                    if (getBrand == "Porsche" || getBrand == "Porshe" || getBrand == "Porche" || getBrand == "porsche" || getBrand == "Porsh")
                    {
                        Console.WriteLine("Since you drive a porsche, $25 has been added to the monthly total.");
                        InsuranceCost += 25;
                        //increment another $25 to the cost if car is a Porsche 911 Carrera
                        if (getModel == "911 Carrera" || getModel == "911 carrera" || getModel == "911Carrera" || getModel == "911carrera") ;
                        {
                            Console.WriteLine("Since you drive a 911 Carrera, $25 has been added to the monthly total");
                            InsuranceCost += 25;
                        }
                    }

                    if (liability)
                    {
                        Console.WriteLine("Since you have opted for liability insurance, a 50% charge has been added to your monthly total.");
                        InsuranceCost = Convert.ToInt32(InsuranceCost * 1.5);
                    }

                    if (dui)
                    {
                        Console.WriteLine("Since you have a DUI charge, a 25% charge has been added to your monthly total");
                        InsuranceCost = Convert.ToInt32(InsuranceCost * 1.25);
                    }
                    ViewBag.total = InsuranceCost;

                    using (db_CarInsuranceEntities db = new db_CarInsuranceEntities())
                    {
                        var signup = new InsuranceCustomer();
                        signup.firstName = firstName;
                        signup.lastName = lastName;
                        signup.emailAddress = emailAddress;
                        signup.Year = caryear;
                        signup.Brand = getBrand;
                        signup.Model = getModel;
                        signup.Plate = getPlate;
                        signup.luxury = lux;
                        signup.dayOfBirth = birthAssign;
                        signup.Dui = dui;
                        signup.tickets = numtickets;
                        signup.liable = liability;



                        db.InsuranceCustomers.Add(signup);
                        db.SaveChanges();

                    }

                    return View("~/Views/Success/Index.cshtml");
                }
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

    }
}