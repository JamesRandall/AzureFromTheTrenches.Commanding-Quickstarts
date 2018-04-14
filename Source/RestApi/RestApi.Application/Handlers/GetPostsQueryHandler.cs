using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using RestApi.Commands;
using RestApi.Commands.Model;

namespace RestApi.Application.Handlers
{
    internal class GetPostsQueryHandler : ICommandHandler<GetPostsQuery, IReadOnlyCollection<PublishedPost>>
    {
        private readonly IList<PublishedPost> _repository;
        private const int PageSize = 5;

        public GetPostsQueryHandler(IList<PublishedPost> repository)
        {
            _repository = repository;
        }

        public Task<IReadOnlyCollection<PublishedPost>> ExecuteAsync(GetPostsQuery command, IReadOnlyCollection<PublishedPost> previousResult)
        {
            int pageNumber = command.Page ?? 0;

            IReadOnlyCollection<PublishedPost> result = _repository
                .OrderByDescending(x => x.PostedAtUtc)
                .Skip(pageNumber * PageSize)
                .Take(PageSize).ToArray();

            return Task.FromResult(result);
        }
    }
}
