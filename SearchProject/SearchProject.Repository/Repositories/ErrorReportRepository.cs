using Microsoft.EntityFrameworkCore;
using SearchProject.Domain.Entities;
using SearchProject.Domain.Interfaces;
using SearchProject.Repository.Data;

namespace SearchProject.Repository.Repositories
{
    public class ErrorReportRepository : IErrorReportRepository
    {
        private readonly AppDbContext _context;

        public ErrorReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorReport> AddAsync(ErrorReport report)
        {
            _context.ErrorReports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<List<ErrorReport>> GetAllAsync()
        {
            return await _context.ErrorReports
                .OrderByDescending(e => e.ReportedAt)
                .ToListAsync();
        }
    }
}
