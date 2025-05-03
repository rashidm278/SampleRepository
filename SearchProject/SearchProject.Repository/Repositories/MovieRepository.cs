using Microsoft.EntityFrameworkCore;
using SearchProject.Entities;
using SearchProject.Interfaces;
using SearchProject.Repository.Data;

namespace SearchProject.Repository.Repositories
{
    /// <summary>
    /// Movie repository implementation
    /// </summary>
    public class MovieRepository : IMovieRepository
    {
        
        private readonly AppDbContext _context;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="context"></param>
        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// method to add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Movies> AddAsync(Movies movies)
        {
            _context.Movies.Add(movies);
            await _context.SaveChangesAsync();
            return movies;
        }

        /// <summary>
        /// methos for doing search
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="genre"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<MovieSearchResult> SearchAsync(string searchQuery, string genre, string sortBy, int page, int pageSize)
        {
            _context.SearchHistories.Add(new SearchHistory
            {
                SearchQuery = searchQuery,
                SearchedDate = DateTime.Now
            });
            await _context.SaveChangesAsync();

            // Perform search and filtering
            var query = _context.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(i => i.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(i => i.Title.Contains(searchQuery) || i.Description.Contains(searchQuery));
            }

            query = sortBy switch
            {
                "date" => query.OrderByDescending(i => i.PublishedDate),
                "popularity" => query.OrderByDescending(i => i.Popularity),
                _ => query.OrderBy(i => i.Title)
            };

            var total = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new MovieSearchResult
            {
                Items = items,
                TotalCount = total,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
