using System.Collections.Generic;
using System.Linq;
using Pharmix.Data.Entities.ViewModels;

namespace Pharmix.Web.Services.Mappers
{
    public static class QueryListHelper
    {
        public static IEnumerable<TResult> SortResults<TResult>(IEnumerable<TResult> results, SearchRequest request) where TResult : class
        {
            if (string.IsNullOrEmpty(request.SortBy)) return results;

            return request.SortOrder == "desc" ? results.OrderByDescending(p => GetPropertyValue(p, request.SortBy))
                : results.OrderBy(p => GetPropertyValue(p, request.SortBy));
        }

        public static object GetPropertyValue(object obj, string prop)
        {
            var propAry = prop.Split('.');

            foreach (var item in propAry)
            {
                if (obj == null) continue;
                var propertyInfo = obj.GetType().GetProperty(item);
                obj = propertyInfo != null ? propertyInfo.GetValue(obj) : null;
            }

            return obj;
        }

        /// <summary>
        /// Added for UserViewModel
        /// ApplicationUser is not inherited from BaseEntity
        /// </summary>
        public static IEnumerable<TResult> SortViewModelResults<TResult>(IEnumerable<TResult> results, SearchRequest request) 
        {
            if (string.IsNullOrEmpty(request.SortBy)) return results;

            return request.SortOrder == "desc" ? results.OrderByDescending(p => GetPropertyValue(p, request.SortBy))
                : results.OrderBy(p => GetPropertyValue(p, request.SortBy));
        }
    }
}
