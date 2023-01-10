namespace Tgs.Paging;

public static class PagingExtensions
{
	public static IAsyncEnumerable<T> ApplyOrderBy<T>(this IAsyncEnumerable<T> query, Paging<T> paging)
		=> paging.OrderBy != null
			? paging.IsDesc
				? query.OrderByDescending(paging.OrderBy)
				: query.OrderBy(paging.OrderBy)
			: query;

	public static IAsyncEnumerable<T> ApplyPaging<T>(this IAsyncEnumerable<T> query, Paging<T> paging)
	{
		var page = paging.Page <= 0 ? 0 : paging.Page - 1;

		var pageSize = paging.RowCount <= 0 ? 10 : paging.RowCount;

		return query.Skip(page * pageSize).Take(pageSize);
	}

	public static Paging<T> CreatePaging<T>(this IAsyncEnumerable<T> _)
		=> new();
}
