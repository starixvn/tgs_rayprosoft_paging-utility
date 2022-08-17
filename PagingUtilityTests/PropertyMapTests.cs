using NUnit.Framework;
using Tgs.Paging;

namespace PagingUtilityTests;

public class PropertyMapTests
{
	[Test]
	public void Add_Property_Map()
	{
		var sut = new Paging<StubData>();

		sut.MapProperty("Test", s => s.Field1);

		var actual = sut.MapPropertyName("Test");

		Assert.AreEqual(nameof(StubData.Field1), actual);
	}

	[Test]
	public void Order_Data_With_PropertyMap()
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

		paging.MapProperty("Test", p => p.Field1);

		paging.SortByPropertyName("Test", false);

		var expected = collection.OrderBy(data => data.Field1).ToArray();

		var actual = collection.ApplyOrderBy(paging).ToArray();

		Assert.IsTrue(expected.SequenceEqual(actual));
	}
}
