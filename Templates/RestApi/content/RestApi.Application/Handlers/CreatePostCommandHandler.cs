using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using RestApi.Commands;
using RestApi.Commands.Model;

namespace RestApi.Application.Handlers
{
    internal class CreatePostCommandHandler : ICommandHandler<CreatePostCommand, PublishedPost>
    {
        private readonly IList<Author> _authorRepository;
        private readonly IList<PublishedPost> _postRepository;

        public CreatePostCommandHandler(
            IList<Author> authorRepository,
            IList<PublishedPost> postRepository)
        {
            _authorRepository = authorRepository;
            _postRepository = postRepository;
        }

        public Task<PublishedPost> ExecuteAsync(CreatePostCommand command, PublishedPost previousResult)
        {
            Author author = _authorRepository.SingleOrDefault(x => x.Id == command.AuthenticatedUserId);
            if (author == null)
            {
                throw new CommandModelException("AuthenticatedUserId", "An authenticated user cannot be found has an author");
            }

            PublishedPost post = new PublishedPost
            {
                Author = author,
                Body = command.Body,
                Id = Guid.NewGuid(),
                PostedAtUtc = DateTime.UtcNow,
                Title = command.Title
            };
            _postRepository.Add(post);
            return Task.FromResult(post);
        }
    }
}
