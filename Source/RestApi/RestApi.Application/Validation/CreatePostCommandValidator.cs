using FluentValidation;
using RestApi.Commands;

namespace RestApi.Application.Validation
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.Body).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.AuthenticatedUserId).NotEmpty();
        }
    }
}
