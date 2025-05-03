using SearchProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchProject.Interfaces.Interfaces
{
    public interface IErrorReportRepository : IRepository<ErrorReport>
    {
        Task<List<ErrorReport>> GetAllAsync();
    }
}
