using System.Linq.Expressions;

namespace Tgs.Paging;

public class Paging<TRow>
{
	private readonly Dictionary<string, string> _propertyNameMap = new(StringComparer.OrdinalIgnoreCase);

	public int Page { get; set; }

	public int RowCount { get; set; }

	public bool IsDesc { get; set; }

	internal LambdaExpression? OrderBy { get; set; } = null;

	internal string MapPropertyName(string inputPropertyName)
	{
		if (_propertyNameMap.TryGetValue(inputPropertyName, out var mappedName))
			return mappedName;
		else
			return inputPropertyName;
	}

	internal void AddPropertyMap(string inputPropertyName, string mappedName)
	{
		_propertyNameMap.Add(inputPropertyName, mappedName);
	}
}
