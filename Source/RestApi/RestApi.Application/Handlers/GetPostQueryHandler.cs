using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using RestApi.Commands;
using RestApi.Commands.Model;

namespace RestApi.Application.Handlers
{
    internal class GetPostQueryHandler : ICommandHandler<GetPostQuery, PublishedPost>
    {
        private readonly IList<PublishedPost> _repository;

        public GetPostQueryHandler(IList<PublishedPost> repository)
        {
            _repository = repository;
        }

        public Task<PublishedPost> ExecuteAsync(GetPostQuery command, PublishedPost previousResult)
        {
            return Task.FromResult(_repository.SingleOrDefault(x => x.Id == command.Id));
        }
    }
}
