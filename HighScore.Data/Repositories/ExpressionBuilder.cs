using HighScore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HighScore.Data.Repositories
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> CreateExpression<T>(Expression<Func<T, bool>> expresion)
        {
            return expresion;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var expresParams = expression1.Parameters; // (user) => user.id == 1..., (user) = Expression.Parameter(typeof(User))

            var andAlso = Expression.AndAlso(expression1.Body, expression2.Body);

            return Expression.Lambda<Func<T, bool>>(andAlso, expresParams);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var expresParams = expression1.Parameters; // (user) => user.id == 1..., (user) = Expression.Parameter(typeof(User))

            var or = Expression.Or(expression1.Body, expression2.Body);

            return Expression.Lambda<Func<T, bool>>(or, expresParams);
        }
    }
}
