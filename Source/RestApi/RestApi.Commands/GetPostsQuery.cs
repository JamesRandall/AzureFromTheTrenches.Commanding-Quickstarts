using System.Collections.Generic;
using AzureFromTheTrenches.Commanding.Abstractions;
using RestApi.Commands.Model;

namespace RestApi.Commands
{
    public class GetPostsQuery : ICommand<IReadOnlyCollection<PublishedPost>>
    {
        public int? Page { get; set; } = 0;
    }
}
