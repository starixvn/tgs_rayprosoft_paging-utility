using NUnit.Framework;
using Tgs.Paging;

namespace PagingUtilityTests;

[TestFixture]
public class AsyncEnumerableTests
{
	[Test]
	public void Can_Get_OrderBy_Method()
	{
		var sut = typeof(AsyncEnumerable);

		var actual = sut.GetGenericMethod(
			nameof(Queryable.OrderBy),
			new[] { typeof(IAsyncEnumerable<>), typeof(Func<,>) });

		Assert.IsNotNull(actual);
	}

	[Test]
	public void Can_Get_OrderByDescending_Method()
	{
		var sut = typeof(AsyncEnumerable);

		var actual = sut.GetGenericMethod(
			nameof(Queryable.OrderByDescending),
			new[] { typeof(IAsyncEnumerable<>), typeof(Func<,>) });

		Assert.IsNotNull(actual);
	}

	[Test]
	public async Task Can_OrderBy_Data()
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
		}.ToAsyncEnumerable();

		var paging = new Paging<StubData>();

		paging.SortByPropertyName("Field1", false);

		var expected = await collection.OrderBy(data => data.Field1).ToArrayAsync();

		var actual = await collection.ApplyOrderBy(paging).ToArrayAsync();

		Assert.IsTrue(expected.SequenceEqual(actual));
	}

	[Test]
	public async Task Can_OrderByDescending_Data()
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
		}.ToAsyncEnumerable();

		var paging = new Paging<StubData>();

		paging.SortByPropertyName("Field1", true);

		var expected = await collection.OrderByDescending(data => data.Field1).ToArrayAsync();

		var actual = await collection.ApplyOrderBy(paging).ToArrayAsync();

		Assert.IsTrue(expected.SequenceEqual(actual));
	}

	[Test]
	public async Task Can_Paging_Data()
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
		}.ToAsyncEnumerable();

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

		var actual = await collection.ApplyPaging(paging).ToArrayAsync();

		Assert.IsTrue(expected.SequenceEqual(actual));
	}

	[Test]
	public void Create_Paging_Object_From_AsyncEnumerable()
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
		}.ToAsyncEnumerable();

		var actual = collection.CreatePaging();

		Assert.IsNotNull(actual);
		Assert.IsTrue(actual is Paging<StubData>);
	}
}
