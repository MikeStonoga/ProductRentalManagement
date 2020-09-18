using System;

namespace PRM.InterfaceAdapters.Controllers.Renters.Dtos.GetBirthDaysOnPeriods
{
    public class GetBirthDaysOnPeriodInput
    {
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
    }
}