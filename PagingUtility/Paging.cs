using System.Linq.Expressions;

namespace Tgs.Paging;

public class Paging<TRow>
{
	public int Page { get; set; }

	public int RowCount { get; set; }

	public bool IsDesc { get; set; }

	internal LambdaExpression? OrderBy { get; set; } = null;

	internal Dictionary<string, string> PropertyNameMap { get; } = new(StringComparer.OrdinalIgnoreCase);

	internal static void AddPropertyMap(
		string inputPropertyName,
		string mappedName,
		Dictionary<string, string> m_PropertyNameMap)
		=> m_PropertyNameMap.Add(inputPropertyName, mappedName);

	internal string MapPropertyName(string inputPropertyName)
		=> PropertyNameMap.TryGetValue(inputPropertyName, out var mappedName)
			? mappedName
			: inputPropertyName;
}
