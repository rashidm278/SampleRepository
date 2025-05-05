using SearchProject.Domain.Entities;

namespace SearchProject.Domain.Interfaces
{
    /// <summary>
    /// Movie repository interface
    /// </summary>
    public interface IMovieRepository : IRepository<Movies>
    {
        /// <summary>
        /// contract to do search 
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="genre"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<MovieSearchResult> SearchAsync(string userName, string searchQuery, string genre, string sortBy, int page, int pageSize);
    }
}
