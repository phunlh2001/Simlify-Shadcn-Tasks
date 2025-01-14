﻿using System.Linq.Expressions;

namespace TaskManagement.Core.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? query.Where(predicate) : query;
        }

        public static IQueryable<T> OrderByDirection<T>(this IQueryable<T> query, Expression<Func<T, object>> orderSelector, string sortOrder = "ASC")
        {
            return sortOrder.ToUpper() == "ASC"
                ? query.OrderBy(orderSelector)
                : query.OrderByDescending(orderSelector);
        }
    }
}
