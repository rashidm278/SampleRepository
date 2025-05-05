using MediatR;
using SearchProject.Domain.Entities;

namespace SearchProject.Domain.Command
{

    public class CreateMovieCommand : IRequest<Movies>
    {
        /// <summary>
        /// get and set movie title
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// get and set movie genre
        /// </summary>
        public string Genre { get; set; } = null!;

        /// <summary>
        /// get and set rating
        /// </summary>
        public string Rating { get; set; } = null!;

        /// <summary>
        /// get and set description
        /// </summary>
        public string Description { get; set; } = null!;

    }
}
