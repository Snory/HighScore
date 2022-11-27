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

        public static Expression<Func<T,dynamic>> CreateExpression<T>(Expression<Func<T, dynamic>> expresion)
        {
            return expresion;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            //apply params from expression1 to expression2
            var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());

            var andAlso = Expression.AndAlso(expression1.Body, invokedExpression);

            return Expression.Lambda<Func<T, bool>>(andAlso, expression1.Parameters);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());

            var or = Expression.Or(expression1.Body, expression2.Body);

            return Expression.Lambda<Func<T, bool>>(or, expression1.Parameters);
        }
    }
}
