using Tgs.Paging;

namespace PagingUtilityTests;

public class PagingTypeTests
{
	[Test]
	public void 可以轉換分頁物件型別()
	{
		var sut = new Paging<StubData>();

		var collection = new[]
		{
			new StubData2
			{
				Field2 = 3
			},
			new StubData2
			{
				Field2 = 2
			},
			new StubData2
			{
				Field2 = 1
			}
		}.AsQueryable();

		sut.SortByPropertyName(nameof(StubData.Field1), false);

		sut.Page = 1;
		sut.RowCount = 1;

		var paging = sut.ConvertTo<StubData2>(p => p.MapProperty(
			nameof(StubData.Field1),
			o => o.Field2));

		var expected = new StubData2
		{
			Field2 = 1
		};

		var actual = collection.ApplyOrderBy(paging).ApplyPaging(paging).First();

		Assert.AreEqual(expected, actual);
	}
}
