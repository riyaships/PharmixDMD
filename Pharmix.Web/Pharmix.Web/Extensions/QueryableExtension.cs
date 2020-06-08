using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Pharmix.Web.Extensions
{
    public static class QueryableExtensionHelper
    {

        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        private static MethodInfo startsWithMethod =
        typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod =
        typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, SortField);
            var exp = Expression.Lambda(prop, param);
            string method = Ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }

        /// <summary>
        /// Multiple field sorting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="sortModels"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IEnumerable<SortModel> sortModels)
        {
            var expression = source.Expression;
            int count = 0;
            foreach (var item in sortModels)
            {
                var parameter = Expression.Parameter(typeof(T), "x");
                var selector = Expression.PropertyOrField(parameter, item.Name);
                var method = string.Equals(item.Sort, "desc", StringComparison.OrdinalIgnoreCase) ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call(typeof(Queryable), method,
                    new Type[] { source.ElementType, selector.Type },
                    expression, Expression.Quote(Expression.Lambda(selector, parameter)));
                count++;
            }
            return count > 0 ? source.Provider.CreateQuery<T>(expression) : source;
        }


        public static Expression<Func<T,
   bool>> GetExpression<T>(IList<Filter> filters)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]);
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]);
                    else
                        exp = Expression.Or(exp, GetExpression<T>(param, filters[0], filters[1]));

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        exp = Expression.Or(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, Filter filter)
        {
            MemberExpression member = Expression.Property(param, filter.PropertyName);
            ConstantExpression constant = Expression.Constant(filter.Value.ToString().ToLower());

            switch (filter.Operation)
            {
                case Op.Equals:
                    return Expression.Equal(member, constant);

                case Op.GreaterThan:
                    return Expression.GreaterThan(member, constant);

                case Op.GreaterThanOrEqual:
                    return Expression.GreaterThanOrEqual(member, constant);

                case Op.LessThan:
                    return Expression.LessThan(member, constant);

                case Op.LessThanOrEqual:
                    return Expression.LessThanOrEqual(member, constant);

                case Op.Contains:
                    return Expression.Call(member, containsMethod, constant);

                case Op.StartsWith:
                    return Expression.Call(member, startsWithMethod, constant);

                case Op.EndsWith:
                    return Expression.Call(member, endsWithMethod, constant);
            }

            return null;
        }

        private static BinaryExpression GetExpression<T>
        (ParameterExpression param, Filter filter1, Filter filter2)
        {
            Expression bin1 = GetExpression<T>(param, filter1);
            Expression bin2 = GetExpression<T>(param, filter2);

            return Expression.Or(bin1, bin2);
        }

        public static List<T> ProcessCollection<T>(IQueryable<T> records, IFormCollection requestFormData, int pageSize, out int totalRecords)
        {
            var skip = Convert.ToInt32(requestFormData["start"].ToString());
            pageSize = Convert.ToInt32(requestFormData["length"].ToString());
            totalRecords = 0;

            Microsoft.Extensions.Primitives.StringValues tempOrder = new[] { "" };

            if (requestFormData.TryGetValue("order[0][column]", out tempOrder))
            {
                var columnIndex = requestFormData["order[0][column]"].ToString();
                var sortDirection = requestFormData["order[0][dir]"].ToString();
                tempOrder = new[] { "" };
                if (requestFormData.TryGetValue($"columns[{columnIndex}][data]", out tempOrder))
                {
                    var columnName = requestFormData[$"columns[{columnIndex}][data]"].ToString();
                    if (pageSize > 0)
                    {
                        var prop = getProperty<T>(columnName);
                        //if (sortDirection.Equals("asc"))
                        //{
                        //    return lstElements.OrderBy(prop.GetValue).Skip(skip).Take(pageSize).ToList();
                        //}
                        //else
                        //    return lstElements.OrderByDescending(prop.GetValue).Skip(skip).Take(pageSize).ToList();


                        var filterList = GetFilters(requestFormData);

                        IEnumerable<T> filteredRecords = null;
                        if (filterList != null && filterList.Count() > 0)
                        {
                            var deleg = GetExpression<T>(filterList).Compile();
                            //lstElements = lstElements.Where(deleg).ToList();
                            filteredRecords = records.Where(deleg).ToList();
                        }
                        else
                            filteredRecords = records.ToList();

                        if (filteredRecords != null)
                        {
                            filteredRecords = filteredRecords.AsQueryable().OrderByField(columnName, sortDirection.Equals("asc")).ToList();

                            filteredRecords = filteredRecords.Skip(skip).Take(pageSize).ToList();

                            totalRecords = filteredRecords.Count();
                            return filteredRecords.ToList();
                        }
                        //else
                        //{
                        //    var listType = typeof(List<>);

                        //    var constructedListType = listType.MakeGenericType(records);

                        //    var testList=Activator.CreateInstance(listType);
                        //    var test= (T)Activator.CreateInstance(typeof(T), new List<T>());
                        //    return (T)Activator.CreateInstance(typeof(T), new List<T>());
                        //}
                    }
                    else
                        return records.ToList();
                }
            }
            return null;
        }

        public static List<Filter> GetFilters(IFormCollection requestFormData)
        {
            List<Filter> filterList = null;

            bool isExists = true;
            int currColIndex = 0;
            string searchString = string.Empty;

            if (requestFormData.ContainsKey("search[value]"))
                searchString = requestFormData["search[value]"].ToString().ToLower();

            if (!string.IsNullOrEmpty(searchString))
                while (isExists)
                {
                    if (requestFormData.ContainsKey($"columns[{currColIndex}][searchable]"))
                    {
                        var isSearchable = Convert.ToBoolean(requestFormData[$"columns[{currColIndex}][searchable]"]);
                        if (isSearchable)
                        {
                            var colName = requestFormData[$"columns[{currColIndex}][data]"];
                            if (filterList == null)
                                filterList = new List<Filter>();
                            filterList.Add(new Filter() { PropertyName = colName, Operation = Op.Contains, Value = searchString });
                        }
                    }
                    else isExists = false;
                    currColIndex++;
                }
            return filterList;
        }

        private static PropertyInfo getProperty<T>(string name)
        {
            var properties = typeof(T).GetProperties();
            PropertyInfo prop = null;
            foreach (var item in properties)
            {
                if (item.Name.ToLower().Equals(name.ToLower()))
                {
                    prop = item;
                    break;
                }
            }
            return prop;
        }

        #region Not using

        //public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending)
        //    {
        //    var param = Expression.Parameter(typeof(T), "p");
        //    var prop = Expression.Property(param, SortField);
        //    var exp = Expression.Lambda(prop, param);
        //    string method = Ascending ? "OrderBy" : "OrderByDescending";
        //    Type[] types = new Type[] { q.ElementType, exp.Body.Type };
        //    var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);

        //    var type = typeof(T);
        //    var property = type.GetProperty(SortField);
        //    var parameter = Expression.Parameter(type, "p");
        //    var propertyAccess = Expression.Property(parameter, property);
        //    var orderByExp = Expression.Lambda(propertyAccess, parameter);
        //    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new[] { type, property.PropertyType }, q.Expression, Expression.Quote(orderByExp));
        //    //return (IEnumerable<T>)Expression.Lambda(resultExp).Compile().DynamicInvoke();

        //    return q.Provider.CreateQuery<T>(mce);
        //}

        //private static IOrderedQueryable<T> OrderingHelper<T>(IQueryable<T> source, string propertyName, bool descending, bool anotherLevel)
        //{
        //    ParameterExpression param = Expression.Parameter(typeof(T), string.Empty); // I don't care about some naming
        //    MemberExpression property = Expression.PropertyOrField(param, propertyName);
        //    LambdaExpression sort = Expression.Lambda(property, param);
        //    MethodCallExpression call = Expression.Call(
        //        typeof(Queryable),
        //        (!anotherLevel ? "OrderBy" : "ThenBy") + (descending ? "Descending" : string.Empty),
        //        new[] { typeof(T), property.Type },
        //        source.Expression,
        //        Expression.Quote(sort));
        //    return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        //}
        //public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        //{
        //    return OrderingHelper(source, propertyName, false, false);
        //}
        //public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        //{
        //    return OrderingHelper(source, propertyName, true, false);
        //}
        //public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string propertyName)
        //{
        //    return OrderingHelper(source, propertyName, false, true);
        //}
        //public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string propertyName)
        //{
        //    return OrderingHelper(source, propertyName, true, true);
        //}

        #endregion

    }


    public class Filter
    {
        public string PropertyName { get; set; }
        public Op Operation { get; set; }
        public object Value { get; set; }
    }

    public enum Op
    {
        Equals,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith
    }


    public class TmpUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
    public class SortModel
    {
        public string Name { get; set; }
        public string Sort { get; set; }
    }
}
