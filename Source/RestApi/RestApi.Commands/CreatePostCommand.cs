using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using RestApi.Commands.Model;

namespace RestApi.Commands
{
    public class CreatePostCommand : ICommand<PublishedPost>
    {
        // Marking this property with the SecurityProperty attribute means that the ASP.Net Core Commanding
        // framework will not allow binding to the property except by the claims mapper
        [SecurityProperty]
        public Guid AuthenticatedUserId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}
