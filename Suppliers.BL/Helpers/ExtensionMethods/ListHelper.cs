using Common.Generic.Helpers;

namespace Suppliers.BL.Helpers.ExtensionMethods;

public static class ListHelper
{
    public static DataCollection<T> ToDataCollection<T>(this List<T> source, int page, int take)
    {
        var count = source.Count;
        return new DataCollection<T>()
        {
            Page = page,
            Total = count,
            Pages = (int)Math.Ceiling((double)count / take),
            Items = source.Skip((page - 1) * take).Take(take),
        };
    }

    public static DataCollection<T> ToDataCollection<T>(this IEnumerable<T> source, int page, int take)
    {
        var count = source.Count();
        return new DataCollection<T>()
        {
            Page = page,
            Total = count,
            Pages = (int)Math.Ceiling((double)count / take),
            Items = source.Skip((page - 1) * take).Take(take),
        };
    }
}
