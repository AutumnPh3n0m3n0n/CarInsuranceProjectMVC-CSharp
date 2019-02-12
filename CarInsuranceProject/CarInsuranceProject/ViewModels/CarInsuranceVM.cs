using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarInsuranceProject.ViewModels
{
    public class CarInsuranceVMs
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime DayofBirth { get; set; }
        public int Year { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }

    }
}