namespace SimpleRetail.BL.Contracts;

public interface IService<TRequest, TResponse>
{
    /// <summary>
    /// Return all members of specified entity.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="recordsToTake"></param>
    /// <returns></returns>
    Task<IEnumerable<TResponse>> GetAll(int page, int recordsToTake);

    /// <summary>
    /// Return specific entity with this Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TResponse?> GetById(Guid id);

    /// <summary>
    /// Save new entity into database.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<TResponse?> CreateAsync(TRequest request);

    /// <summary>
    /// Update existing entity in database.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<TResponse?> UpdateAsync(TRequest request);

    /// <summary>
    /// Delete existing entity from database (soft method).
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, Guid ChangeUserId);
}
