using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.ExtensionMethods
{
    /// <summary>
    /// Helper Methods for DbSets
    /// </summary>
    public static class QueryableHelpers
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> source, int pageSize, int page)
        {
            return source.Skip(page * pageSize).Take(pageSize);
        }

        public static async Task<T?> GetByIdAsync<T>(this IQueryable<T> source, int id) where T : IEntity
        {
            return await source.SingleOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<int> CountAsync<T>(this IQueryable<T> source)
        {
            return await source.CountAsync();
        }
    }
}
