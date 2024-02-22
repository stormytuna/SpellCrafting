using System;
using System.Collections.Generic;

namespace SpellCrafting.Helpers;

public static class LinqHelpers
{
    public static void Do<T>(this IEnumerable<T> enumerable, Action<T> action) {
        foreach (T entry in enumerable) {
            action?.Invoke(entry);
        }
    }
}
