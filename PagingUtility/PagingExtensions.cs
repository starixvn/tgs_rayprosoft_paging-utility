namespace Tgs.Paging;

public static class PagingExtensions
{
	public static void SortByPropertyName<T>(this Paging<T> paging, string propertyName, bool isDesc)
	{
		if (!string.IsNullOrWhiteSpace(propertyName))
		{
			var type = typeof(T);

			paging.OrderBy = type.MakeMemberAccess(propertyName);
			paging.IsDesc = isDesc;
		}
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
}
