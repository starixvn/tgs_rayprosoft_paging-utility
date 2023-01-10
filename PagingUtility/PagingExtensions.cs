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

			paging.OrderBy = type.MakeMemberAccess(paging.MapPropertyName(propertyName), out var memberPropertyName);
			paging.PropertyName = memberPropertyName;
			paging.IsDesc = isDesc;
		}
	}

	public static Paging<T> MapProperty<T>(this Paging<T> paging, string inputPropertyName, [NotNull] Expression<Func<T, object>> propertySelector)
	{
		Paging<T>.AddPropertyMap(inputPropertyName, GetMemberName(propertySelector.Body), paging.PropertyNameMap);

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
		=> new();

	private static string GetMemberName(Expression expression)
		=> expression is MemberExpression memberExpression
			? memberExpression.Member.Name
			: expression is MethodCallExpression methodCallExpression
				? methodCallExpression.Method.Name
				: expression is UnaryExpression unaryExpression
					? GetMemberName(unaryExpression)
					: throw new ArgumentException("Can not read property name.");

	private static string GetMemberName(UnaryExpression unaryExpression)
		=> unaryExpression.Operand is MethodCallExpression methodExpression
			? methodExpression.Method.Name
			: ((MemberExpression)unaryExpression.Operand).Member.Name;
}
