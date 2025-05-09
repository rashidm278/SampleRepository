﻿using MediatR;
using SearchProject.Domain.Dtos;
using SearchProject.Domain.Query;
using SearchProject.Domain.Interfaces;

namespace SearchProject.Application.Handlers
{
    public class GetSearchHistoryQueryHandler : IRequestHandler<GetSearchHistoryQuery, IEnumerable<SearchHistoryDto>>
    {
        private readonly ISearchHistoryRepository _repository;

        public GetSearchHistoryQueryHandler(ISearchHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SearchHistoryDto>> Handle(GetSearchHistoryQuery request, CancellationToken cancellationToken)
        {
            var searchHistoryList = await _repository.GetByUsernameAsync(request.Username);
            var histories = searchHistoryList.Select(e => new SearchHistoryDto
            {
                SearchedDate = e.SearchedDate,
                SearchHistoryId = e.SearchHistoryId,
                SearchQuery = e.SearchQuery

            });
            return histories;
        }
    }
}
