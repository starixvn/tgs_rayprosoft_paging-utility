using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Tgs.Paging;

public static class PagingExtensions
{
	public static void SortByPropertyName<T>(this Paging<T> paging, string propertyName, bool isDesc)
	{
		if (!string.IsNullOrWhiteSpace(propertyName))
		{
			var type = typeof(T);

			paging.OrderBy = type.MakeMemberAccess(paging.MapPropertyName(propertyName));
			paging.IsDesc = isDesc;
		}
	}

	public static Paging<T> MapProperty<T>(this Paging<T> paging, string inputPropertyName, [NotNull] Expression<Func<T, object>> propertySelector)
	{
		paging.AddPropertyMap(inputPropertyName, GetMemberName(propertySelector.Body));

		return paging;
	}

	public static IQueryable<T> ApplyOrderBy<T>(this IQueryable<T> query, Paging<T> paging)
		=> paging.OrderBy != null
			? paging.IsDesc
				? query.OrderByDescending(paging.OrderBy)
				: query.OrderBy(paging.OrderBy)
			: query;

	public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, Paging<T> paging)
	{
		var page = paging.Page <= 0 ? 0 : paging.Page - 1;

		var pageSize = paging.RowCount <= 0 ? 10 : paging.RowCount;

		return query.Skip(page * pageSize).Take(pageSize);
	}

	public static Paging<T> CreatePaging<T>(this IQueryable<T> _)
		=> new Paging<T>();

	private static string GetMemberName(Expression expression)
	{
		if (expression is MemberExpression)
		{
			// Reference type property or field
			var memberExpression = (MemberExpression)expression;
			return memberExpression.Member.Name;
		}
		if (expression is MethodCallExpression)
		{
			// Reference type method
			var methodCallExpression = (MethodCallExpression)expression;
			return methodCallExpression.Method.Name;
		}
		if (expression is UnaryExpression)
		{
			// Property, field of method returning value type
			var unaryExpression = (UnaryExpression)expression;
			return GetMemberName(unaryExpression);
		}

		throw new ArgumentException("Can not read property name.");
	}

	private static string GetMemberName(UnaryExpression unaryExpression)
	{
		if (unaryExpression.Operand is MethodCallExpression)
		{
			var methodExpression = (MethodCallExpression)unaryExpression.Operand;
			return methodExpression.Method.Name;
		}
		return ((MemberExpression)unaryExpression.Operand).Member.Name;
	}
}
