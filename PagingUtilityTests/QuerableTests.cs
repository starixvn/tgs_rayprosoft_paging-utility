using System.Linq.Expressions;
using NUnit.Framework;
using Tgs.Paging;

namespace PagingUtilityTests;

[TestFixture]
public class QuerableTests
{
	[Test]
	public void Can_Get_OrderBy_Method()
	{
		var sut = typeof(Queryable);

		var actual = sut.GetGenericMethod(
			nameof(Queryable.OrderBy),
			new[] { typeof(IQueryable<>), typeof(Expression<>).MakeGenericType(typeof(Func<,>)) });

		Assert.IsNotNull(actual);
	}

	[Test]
	public void Can_Get_OrderByDescending_Method()
	{
		var sut = typeof(Queryable);

		var actual = sut.GetGenericMethod(
			nameof(Queryable.OrderByDescending),
			new[] { typeof(IQueryable<>), typeof(Expression<>).MakeGenericType(typeof(Func<,>)) });

		Assert.IsNotNull(actual);
	}

	[Test]
	public void Can_OrderBy_Data()
	{
		var collection = new[]
		{
			new StubData
			{
				Field1 = 3
			},
			new StubData
			{
				Field1 = 2
			},
			new StubData
			{
				Field1 = 1
			}
		}.AsQueryable();

		var paging = new Paging<StubData>();

		paging.SortByPropertyName("Field1", false);

		var expected = collection.OrderBy(data => data.Field1).ToArray();

		var actual = collection.ApplyOrderBy(paging).ToArray();

		Assert.IsTrue(expected.SequenceEqual(actual));
	}

	[Test]
	public void Can_OrderByDescending_Data()
	{
		var collection = new[]
		{
			new StubData
			{
				Field1 = 1
			},
			new StubData
			{
				Field1 = 2
			},
			new StubData
			{
				Field1 = 3
			}
		}.AsQueryable();

		var paging = new Paging<StubData>();

		paging.SortByPropertyName("Field1", true);

		var expected = collection.OrderByDescending(data => data.Field1).ToArray();

		var actual = collection.ApplyOrderBy(paging).ToArray();

		Assert.IsTrue(expected.SequenceEqual(actual));
	}

	[Test]
	public void Can_Paging_Data()
	{
		var collection = new[]
		{
			new StubData
			{
				Field1 = 1
			},
			new StubData
			{
				Field1 = 2
			},
			new StubData
			{
				Field1 = 3
			}
		}.AsQueryable();

		var paging = new Paging<StubData>
		{
			Page = 2,
			RowCount = 1
		};

		var expected = new[] {
			new StubData
			{
				Field1 = 2
			}
		};

		var actual = collection.ApplyPaging(paging).ToArray();

		Assert.IsTrue(expected.SequenceEqual(actual));
	}
}
