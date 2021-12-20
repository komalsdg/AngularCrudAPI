using AngularCrudAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace AngularCrudAPI.Interface.Services
{
    public static class PagingService
    {
        public static async Task<Paging<T>> GetPagination<T>(IQueryable<T> query, int page, string orderBy, bool orderByDesc, int pageSize) where T : class
        {
            Paging<T> pagination = new Paging<T>
            {
                TotalItems = query.Count(),
                PageSize = pageSize,
                PageNumber = page,
                OrderBy = orderBy,
                OrderByDesc = orderByDesc
            };

            int skip = (page - 1) * pageSize;
            
            pagination.Result = await query
                .OrderByField(orderBy, orderByDesc)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return pagination;
        }

        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool descending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, SortField);
            var exp = Expression.Lambda(prop, param);
            string method = descending ? "OrderByDescending" : "OrderBy";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }
}
