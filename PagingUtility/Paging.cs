using System.Linq.Expressions;

namespace Tgs.Paging;

public class Paging<TRow>
{
	public int Page { get; set; }

	public int RowCount { get; set; }

	public bool IsDesc { get; set; }

	public LambdaExpression? OrderBy { get; set; } = null;
}
