using MediatR;
using SearchProject.Application.Command;
using SearchProject.Domain.Interfaces;
using SearchProject.Domain.Entities;

namespace SearchProject.Application.Handlers
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Movies>
    {
        private readonly IMovieRepository _MovieRepository;

        public CreateMovieCommandHandler(IMovieRepository MovieRepository)
        {
            _MovieRepository = MovieRepository;
        }

        public async Task<Movies> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movies = new Movies
            {
                Title = request.Title,
                Genre = request.Genre,
                Description = request.Description,
                Rating = request.Rating,
                PublishedDate = DateTime.Now,
                Content = request.Description
            };

            await _MovieRepository.AddAsync(movies);
            return movies;
        }
    }
}
