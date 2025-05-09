﻿using Microsoft.EntityFrameworkCore;
using SearchProject.Domain.Entities;
using SearchProject.Domain.Interfaces;
using SearchProject.Repository.Data;

namespace SearchProject.Repository.Repositories
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        private readonly AppDbContext _context;

        public SearchHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SearchHistory> AddAsync(SearchHistory searchHistory)
        {
            _context.SearchHistories.Add(searchHistory);
            await _context.SaveChangesAsync();
            return searchHistory;
        }

        public async Task<List<SearchHistory>> GetByUsernameAsync(string username)
        {
            return await _context.SearchHistories
                .Where(h => h.User.Username == username)
                .OrderByDescending(h => h.SearchedDate)
                .ToListAsync();
        }
    }
}
