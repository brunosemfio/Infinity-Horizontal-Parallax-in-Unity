using System.Collections.Generic;

public static class ListExtensions
{
    public static T GetAndRemove<T>(this IList<T> list, int index)
    {
        if (index >= list.Count) return default;

        var item = list[index];
        list.RemoveAt(index);

        return item;
    }
}