using System;
using System.Collections.Generic;
using System.Linq;
using RestApi.Commands.Model;

namespace RestApi.Application.Repositories
{
    internal static class PostRepository
    {
        private const string LoremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        public static IList<PublishedPost> Get()
        {
            Author authorOne = AuthorRepository.Get().First();
            Author authorTwo = AuthorRepository.Get().Skip(1).First();

            return new List<PublishedPost>
            {
                new PublishedPost
                {
                    Author = authorOne,
                    Body = LoremIpsum,
                    Id = Guid.Parse("01C1BB0B-BE60-4FD8-B857-260AA0E83C64"),
                    PostedAtUtc = DateTime.UtcNow.AddDays(-5),
                    Title = "My First Post"
                },
                new PublishedPost
                {
                    Author = authorTwo,
                    Body = LoremIpsum,
                    Id = Guid.Parse("E1E1CFF0-2DA3-4F1F-811B-10E3811D68A6"),
                    PostedAtUtc = DateTime.UtcNow.AddDays(-7),
                    Title = "Blog Launch"
                },
                new PublishedPost
                {
                    Author = authorOne,
                    Body = LoremIpsum,
                    Id = Guid.Parse("6F5A5886-41F9-42B6-84D6-C1E4BD3EB6AC"),
                    PostedAtUtc = DateTime.UtcNow.AddDays(-3),
                    Title = "Another Post"
                }
            };
        }
    }
}
