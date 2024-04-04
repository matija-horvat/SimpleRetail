using System.Linq.Expressions;

namespace SimpleRetail.Data.Utils;

public static class IQueryableExtensions
{
    public static IQueryable<T> Paginate<T, TKey>(this IQueryable<T> source, int page = 1, int recordsToTake = 10, Expression<Func<T, TKey>> orderBy = null)
    {
        if(page == 0) page = 1;
        if (recordsToTake == 0) recordsToTake = 10;
        if (recordsToTake > 100) recordsToTake = 100;

        if (orderBy != null)
            source = source.OrderBy(orderBy);

        return source.Skip((page - 1) * recordsToTake).Take(recordsToTake);
    }
}
