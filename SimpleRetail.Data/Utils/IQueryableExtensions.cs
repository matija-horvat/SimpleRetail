namespace SimpleRetail.Data.Utils;

public static class IQueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int page = 1, int recordsToTake = 10)
    {
        if(page == 0) page = 1;
        if (recordsToTake == 0) recordsToTake = 10;
        if (recordsToTake > 100) recordsToTake = 100;
        return source.Skip((page - 1) * recordsToTake).Take(recordsToTake);
    }
}
