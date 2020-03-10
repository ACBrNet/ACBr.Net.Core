using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ACBr.Net.Core.Extensions
{
    public static class QueryExtensions
    {
        /// <summary>
        /// Wheres if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="ifFunc">If function.</param>
        /// <param name="ifExpression">If expression.</param>
        /// <param name="elseExpression">The else expression.</param>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query,
            Func<bool> ifFunc,
            Expression<Func<T, bool>> ifExpression,
            Expression<Func<T, bool>> elseExpression = null)
        {
            return query.WhereIf(ifFunc(), ifExpression, elseExpression);
        }

        /// <summary>
        /// Wheres if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="ifFunc">if set to <c>true</c> [if function].</param>
        /// <param name="ifExpression">If expression.</param>
        /// <param name="elseExpression">The else expression.</param>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query,
            bool ifFunc,
            Expression<Func<T, bool>> ifExpression,
            Expression<Func<T, bool>> elseExpression = null)
        {
            if (ifFunc)
                return query.Where(ifExpression);

            return elseExpression != null ? query.Where(elseExpression) : query;
        }

        /// <summary>
        /// Wheres if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="ifFunc">If function.</param>
        /// <param name="ifExpression">If expression.</param>
        /// <param name="elseExpression">The else expression.</param>
        /// <returns>IQueryable&lt;T&gt;.</returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query,
            Func<bool> ifFunc,
            Func<T, bool> ifExpression,
            Func<T, bool> elseExpression = null)
        {
            return query.WhereIf(ifFunc(), ifExpression, elseExpression);
        }

        /// <summary>
        /// Wheres if.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="ifFunc">if set to <c>true</c> [if function].</param>
        /// <param name="ifExpression">If expression.</param>
        /// <param name="elseExpression">The else expression.</param>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> query,
            bool ifFunc,
            Func<T, bool> ifExpression,
            Func<T, bool> elseExpression = null)
        {
            if (ifFunc)
                return query.Where(ifExpression);

            return elseExpression != null ? query.Where(elseExpression) : query;
        }
    }
}