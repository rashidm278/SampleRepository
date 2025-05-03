using SearchProject.Entities;

namespace SearchProject.Interfaces
{
    /// <summary>
    /// search history repository interface
    /// </summary>
    public interface ISearchHistoryRepository : IRepository<SearchHistory>
    {
        Task<List<SearchHistory>> GetByUsernameAsync(string username);
    }

}
