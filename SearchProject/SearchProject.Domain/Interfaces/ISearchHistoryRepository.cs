using SearchProject.Domain.Entities;

namespace SearchProject.Domain.Interfaces
{
    /// <summary>
    /// search history repository interface
    /// </summary>
    public interface ISearchHistoryRepository : IRepository<SearchHistory>
    {
        Task<List<SearchHistory>> GetByUsernameAsync(string username);
    }

}
