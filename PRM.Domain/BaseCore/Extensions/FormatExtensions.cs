using System;

namespace PRM.Domain.BaseCore.Extensions
{
    public static class FormatExtensions
    {
        public static string FormatDate(this DateTime dateToFormat)
        {
            return dateToFormat.ToShortDateString() + " " + dateToFormat.ToLongTimeString();
        }
    }
}