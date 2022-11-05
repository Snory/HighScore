using HighScore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HighScore.Data.Repositories
{
    public class ExpressionBuilder<T> : IExpressionBuilder<T>
    {
        public Expression<Func<T, bool>> CreateExpression(Expression<Func<T, bool>> expresion)
        {
            return expresion;
        }

        public Expression<Func<T, bool>> And(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            return Expression.Lambda<Func<T, bool>>((Expression)Expression.AndAlso(expression1.Body, expression2.Body), (IEnumerable<ParameterExpression>)expression1.Parameters);
        }
    }
}
