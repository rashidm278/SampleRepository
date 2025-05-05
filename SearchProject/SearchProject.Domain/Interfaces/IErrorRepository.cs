using SearchProject.Domain.Entities;

namespace SearchProject.Domain.Interfaces
{
    public interface IErrorReportRepository : IRepository<ErrorReport>
    {
        Task<List<ErrorReport>> GetAllAsync();
    }
}
