using System;
using System.Diagnostics.CodeAnalysis;

namespace SoccerFantasy.Tests
{
	public class Comparer
	{
		public static ObjectCombarer<U?> Get<U>(Func<U?,U?,bool> func)
		{
			return new ObjectCombarer<U?>(func);
		}
	}

	public class ObjectCombarer<T> : Comparer, IEqualityComparer<T>
	{
		private Func<T?, T?, bool> comparisonFunc;

		public ObjectCombarer(Func<T?, T?, bool> func)
		{
			comparisonFunc = func;
		}

        public bool Equals(T? x, T? y)
        {
			return comparisonFunc(x, y);
        }

        public int GetHashCode([DisallowNull] T obj)
        {
			return obj?.GetHashCode() ?? 0;
        }
    }
}

