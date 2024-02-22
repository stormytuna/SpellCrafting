using System.Collections.Generic;

namespace SpellCrafting.Helpers;

public static class ListHelpers
{
    public static bool AllInRange<T>(this List<T> list, params int[] indexes) {
        foreach (int index in indexes) {
            if (index < 0 || index >= list.Count) {
                return false;
            }
        }

        return true;
    }

    public static bool AnyNotInRange<T>(this List<T> list, params int[] indexes) {
        foreach (int index in indexes) {
            if (index < 0 || index >= list.Count) {
                return true;
            }
        }

        return false;
    }

    public static List<T> SwapIndexes<T>(this List<T> list, int first, int second) {
        List<T> newList = new(list);
        (newList[first], newList[second]) = (newList[second], newList[first]);
        return newList;
    }
}
