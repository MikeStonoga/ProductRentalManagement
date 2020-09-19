using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRM.Domain.BaseCore.ValueObjects
{
    public class BirthDate : ValueObject
    {
        public DateTime Date { get; private set; }
        public int Age => DateTime.Now.Date.Year - Date.Date.Year;
        public int Day => Date.Day;
        public int Month => Date.Month;
        public int Year => Date.Year;
        public int Hour => Date.Hour;
        public int Minute => Date.Minute;
        public int Second => Date.Second;
        public DayOfWeek DayOfWeek => Date.DayOfWeek;
        public int DayOfYear => Date.DayOfYear;
        
        private BirthDate(){}
        
        public BirthDate(DateTime date)
        {
            if (date.Date <= DateTime.Now.Date.AddYears(-123) || date > DateTime.Now) throw new ValidationException("Invalid BirthDate");
            Date = date;
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Age;
            yield return Date;
            yield return Day;
            yield return Month;
            yield return Year;
            yield return Hour;
            yield return Minute;
            yield return Second;
            yield return DayOfWeek;
            yield return DayOfYear;
        }

        public bool HasAgeGreaterThan(int age) => Age > age;
        public bool HasAgeEqualOrGreaterThan(int age) => Age >= age;
        public bool HasAgeLesserThan(int age) => Age < age;
        public bool HasAgeEqualOrLesserThan(int age) => Age <= age;
    }
}