﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PRM.Infrastructure.ApplicationDelivery.WebApiHost.BaseCore.Extensions
{
    public static class WebControllerExtensions
    {
        public static Guid FormatAsGuid(this string id)
        {
            var hasId = !string.IsNullOrEmpty(id);
            var guid = hasId ? Guid.Parse(id) : Guid.Empty;
            return guid;
        }
        public static Guid? FormatAsNullableGuid(this string id)
        {
            var hasId = !string.IsNullOrEmpty(id);
            var guid = hasId ? Guid.Parse(id) : (Guid?) null;
            return guid;
        }
        public static List<Guid> FormatAsGuids(this IEnumerable<string> ids)
        {
            return ids.Select(id => id.FormatAsGuid()).ToList();
        }
        
        public static List<Guid?> FormatAsNullableGuids(this IEnumerable<string> ids)
        {
            var nullableGuids = new List<Guid?>();
            if (ids == null) return nullableGuids;
            
            foreach (var id in ids)
            {
                nullableGuids.Add(id.FormatAsNullableGuid());
            }

            return nullableGuids;
        }

        public static DateTime FormatAsDateTime(this string date)
        {
            if (date == null) throw new ArgumentNullException(nameof(date));
            var datetime = Convert.ToDateTime(date);
            return datetime;
        }
        
        public static DateTime FormatAsOptionalDateTime(this string date)
        {
            var datetime = Convert.ToDateTime(date);
            return datetime;
        }
    }
}