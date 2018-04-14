using System;

namespace RestApi.Commands.Model
{
    public class PublishedPost : Post
    {
        public Guid Id { get; set; }

        public DateTime PostedAtUtc { get; set; }

        public Author Author { get; set; }
    }
}
