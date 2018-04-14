using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using RestApi.Commands.Model;

namespace RestApi.Commands
{
    public class GetPostQuery : ICommand<PublishedPost>
    {
        public Guid Id { get; set; }
    }
}
