using MediatR;
using SearchProject.Entities;

namespace SearchProject.Api.Command
{

    public class CreateMovieCommand : IRequest<Movies>
    {

        public string Title { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public string Genre { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public string Rating { get; set; } = null!;
        public string Description { get; set; } = null!;

    }
}
