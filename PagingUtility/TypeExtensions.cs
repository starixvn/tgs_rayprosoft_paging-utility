using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace Tgs.Paging;

internal static class TypeExtensions
{
	public static LambdaExpression MakeMemberAccess(this Type type, string propertyName, out string mappedPropertyName)
	{
		var propertyInfo = type.GetProperty(propertyName)!;

		var parameter = Expression.Parameter(type, "o");
		var propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);

		mappedPropertyName = propertyInfo.Name;
		return Expression.Lambda(propertyAccess, parameter);
	}

	public static MethodInfo? GetGenericMethod(this Type type, string methodName, Type[] parameterTypes)
		=> type.GetMethods()
			.Where(mi => mi.Name == methodName)
			.Where(mi => parameterTypes.SequenceEqual(mi.GetParameters().Select(p => p.ParameterType), TypeEqualityComparer.Default))
			.FirstOrDefault();

	private class TypeEqualityComparer : IEqualityComparer<Type>
	{
		public static TypeEqualityComparer Default => new();

		public bool Equals(Type? x, Type? y)
		{
			return x?.Assembly == y?.Assembly
				&& x?.Namespace == y?.Namespace
				&& x?.Name == y?.Name;
		}

		public int GetHashCode([DisallowNull] Type obj)
		{
			throw new NotImplementedException();
		}
	}
}
