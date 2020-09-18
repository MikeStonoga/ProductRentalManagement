using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PRM.Domain.BaseCore.Dtos;
using PRM.Domain.BaseCore.Extensions;

namespace PRM.Domain.BaseCore.ValueObjects
{
    public class DateRange : ValueObject
    {
        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }
        
        public int Days => EndDate.Subtract(StartDate).Days;

        private DateRange() { }
        
        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate) throw new ValidationException("End date must not be earlier then start date");
            StartDate = startDate;
            EndDate = endDate;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StartDate;
            yield return EndDate;
        }

        public bool IsMonthOnRange(int month)
        {
            return StartDate.Month <= month && month <= EndDate.Month;
        }
        
        public bool IsOnRange(DateTime date)
        {
            return StartDate <= date && date <= EndDate;
        }
        
        public bool IsOnRange(DateRange input)
        {
            return StartDate <= input.StartDate && input.EndDate <= EndDate;
        }
    }

    public static class DateRangeProvider
    {
        public static ValidationDto<DateRange> GetDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var dateRange = new DateRange(startDate, endDate);
                var response = new ValidationDto<DateRange>()
                {
                    Success = true,
                    Message = "Success",
                    Result = dateRange
                };
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var response = new ValidationDto<DateRange>()
                {
                    Success = false,
                    Message = e.Message,
                    Result = new DateRange(DateTime.MinValue, DateTime.MaxValue)
                };

                return response;
            }
        }
    }
}