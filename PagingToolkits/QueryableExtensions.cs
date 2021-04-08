using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
	public static class QueryableExtensions
	{
		private static Type _QueryableType = typeof(Queryable);

		public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, LambdaExpression orderExpression)
		{
			var methodCallExp = Expression.Call(
				_QueryableType
					.GetGenericMethod(
						nameof(Queryable.OrderBy),
						new[] { typeof(IAsyncEnumerable<>), typeof(Func<,>) })
					!.MakeGenericMethod(new[] { typeof(T), orderExpression.ReturnType }),
				Expression.Constant(query),
				Expression.Constant(orderExpression.Compile()));

			return Expression.Lambda<Func<IOrderedQueryable<T>>>(
				methodCallExp).Compile()();
		}

		public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> query, LambdaExpression orderExpression)
		{
			var methodCallExp = Expression.Call(
			   _QueryableType
				   .GetGenericMethod(
					   nameof(Queryable.OrderByDescending),
					   new[] { typeof(IAsyncEnumerable<>), typeof(Func<,>) })
				   !.MakeGenericMethod(new[] { typeof(T), orderExpression.ReturnType }),
			   Expression.Constant(query),
			   Expression.Constant(orderExpression.Compile()));

			return Expression.Lambda<Func<IOrderedQueryable<T>>>(
				methodCallExp).Compile()();
		}
	}
}
