using System.Linq.Expressions;

namespace Tgs.Paging;

public static class QueryableExtensions
{
	private static readonly Type _QueryableType = typeof(Queryable);

	public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, LambdaExpression orderExpression)
	{
		var methodCallExp = Expression.Call(
			_QueryableType
				.GetGenericMethod(
					nameof(Queryable.OrderBy),
					new[] { typeof(IQueryable<>), typeof(Expression<>).MakeGenericType(typeof(Func<,>)) })
				!.MakeGenericMethod(new[] { typeof(T), orderExpression.ReturnType }),
			Expression.Constant(query),
			Expression.Constant(orderExpression));

		return Expression.Lambda<Func<IOrderedQueryable<T>>>(
			methodCallExp).Compile()();
	}

	public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, LambdaExpression orderExpression)
	{
		var methodInfo = _QueryableType
			.GetGenericMethod(
				nameof(Queryable.OrderByDescending),
				new[] { typeof(IQueryable<>), typeof(Expression<>).MakeGenericType(typeof(Func<,>)) })
			!.MakeGenericMethod(new[] { typeof(T), orderExpression.ReturnType });
		var methodCallExp = Expression.Call(
		   methodInfo,
		   Expression.Constant(query),
		   Expression.Constant(orderExpression));

		return Expression.Lambda<Func<IOrderedQueryable<T>>>(
			methodCallExp).Compile()();
	}
}
