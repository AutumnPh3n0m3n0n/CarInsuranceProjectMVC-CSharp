using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarInsuranceProject.Models
{
    public class GetRateCarInsurance
    {
        //personal info
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public System.DateTime DayofBirth { get; set; }
        //car info
        public int Year { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
        public bool Luxury { get; set; }
        //citation info
        public bool Dui { get; set; }
        public int Tickets { get; set; }
        //coverage or liability
        public bool Liable { get; set; }
    }
}