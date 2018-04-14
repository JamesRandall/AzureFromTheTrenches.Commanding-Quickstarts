using System;
using System.Collections.Generic;
using RestApi.Commands.Model;

namespace RestApi.Application.Repositories
{
    internal static class AuthorRepository
    {
        public static IList<Author> Get()
        {
            return new List<Author>
            {
                new Author
                {
                    Id = Guid.Parse("C8242561-222E-4533-802C-7BFC61B403E9"),
                    Name = "Fred Bloggs"
                },
                new Author
                {
                    Id = Guid.Parse("F4826248-2790-4E33-A82C-0314E76F45CA"),
                    Name = "Jane Porter"
                }
            };
        }
    }
}
