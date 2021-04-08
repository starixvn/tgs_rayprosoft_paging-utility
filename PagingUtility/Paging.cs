using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Paging
{
	public class Paging<TRow>
	{
		public int Page { get; set; }

		public int RowCount { get; set; }

		public bool IsDesc { get; set; }

		public LambdaExpression? OrderBy { get; set; } = null;
	}
}
