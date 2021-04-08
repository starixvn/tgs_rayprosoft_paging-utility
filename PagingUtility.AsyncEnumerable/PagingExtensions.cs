using System.Collections.Generic;
using System.Linq;

namespace TGS.Paging
{
	public static class PagingExtensions
	{
		public static IAsyncEnumerable<T> ApplyOrderBy<T>(this IAsyncEnumerable<T> query, Paging<T> paging)
		{
			if (paging.OrderBy != null)
				return paging.IsDesc
					? query.OrderByDescending(paging.OrderBy)
					: query.OrderBy(paging.OrderBy);
			else
				return query;
		}

		public static IAsyncEnumerable<T> ApplyPaging<T>(this IAsyncEnumerable<T> query, Paging<T> paging)
		{
			var page = paging.Page <= 0 ? 0 : paging.Page - 1;

			var pageSize = paging.RowCount <= 0 ? 10 : paging.RowCount;

			return query.Skip(page * pageSize).Take(pageSize);
		}
	}
}
