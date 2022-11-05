using System.Linq.Expressions;

namespace HighScore.Data.Repositories
{
    public interface IExpressionBuilder<T>
    {
        Expression<Func<T, bool>> CreateExpression(Expression<Func<T, bool>> expresion);
        public Expression<Func<T, bool>> And(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2);
    }
}