namespace SearchProject.Interfaces
{
    /// <summary>
    /// base repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// contract to add entity
        /// </summary>
        /// <param name="entity"></param>
        Task<T> AddAsync(T entity);
    }
}
