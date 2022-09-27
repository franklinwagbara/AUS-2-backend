using System;
using System.Collections.Generic;
using System.Text;

namespace AUS2.Core.Utilities
{
    public static class Roles
    {
        public const string Staff = "Staff|Staff";
        public const string SuperAdmin = "SuperAdmin|Super Admin";
        public const string Company = "Company|Company";
        public const string ICT = "ICT|ICT";
        public const string Reviewer = "Reviewer|Reviewer";
        public const string Supervisor = "Supervisor|Supervisor";
        public const string Manager = "Manager|Manager";
        public const string HeadUMR = "HeadUMR|Head UMR";
        public const string Support = "Support|Support";
    }

    public static class StateList
    {
        public const string Abia = "Abia|ABI";
        public const string Adamawa = "Adamawa|ADA";
        public const string AkwaIbom = "Akwa Ibom|AKW";
        public const string Anambra = "Anambra|ANA";
        public const string Bauchi = "Bauchi|BAU";
        public const string Bayelsa = "Bayelsa|BAY";
        public const string Benue = "Benue|BEN";
        public const string Borno = "Borno|BOR";
        public const string Crossriver = "Cross river|CRO";
        public const string Delta = "Delta|DEL";
        public const string Ebonyi = "Ebonyi|EBO";
        public const string Edo = "Edo|EDO";
        public const string Ekiti = "Ekiti|EKI";
        public const string Enugu = "Enugu|ENU";
        public const string FCT = "FCT|FCT";
        public const string Gombe = "Gombe|GOM";
        public const string Imo = "Imo|IMO";
        public const string Jigawa = "Jigawa|JIG";
        public const string Kaduna = "Kaduna|KAD";
        public const string Kano = "Kano|KAN";
        public const string Katsina = "Katsina|KAT";
        public const string Kebbi = "Kebbi|KEB";
        public const string Kogi = "Kogi|KOG";
        public const string Kwara = "Kwara|KWA";
        public const string Lagos = "Lagos|LAG";
        public const string Nasarawa = "Nasarawa|NAS";
        public const string Niger = "Niger|NIG";
        public const string Ogun = "Ogun|OGU";
        public const string Ondo = "Ondo|OND";
        public const string Osun = "Osun|OSU";
        public const string Oyo = "Oyo|OYO";
        public const string Plateau = "Plateau|PLA";
        public const string Rivers = "Rivers|RIV";
        public const string Sokoto = "Sokoto|SOK";
        public const string Taraba = "Taraba|TAR";
        public const string Yobe = "Yobe|YOB";
        public const string Zamfara = "Zamfara|ZAM";
    }

    public class StateObj
    {
        public int SerialNumber { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public string SenDistrict { get; set; }
        public string SenDistrictCode { get; set; }
        public string Shape_Length { get; set; }
        public string Shape_Area { get; set; }
    }
}
