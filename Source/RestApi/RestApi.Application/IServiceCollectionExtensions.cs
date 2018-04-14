using AzureFromTheTrenches.Commanding.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RestApi.Application.Repositories;
using RestApi.Application.Validation;
using RestApi.Commands;

namespace RestApi.Application
{
    // ReSharper disable once InconsistentNaming - interface extensions
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection serviceCollection,
            ICommandRegistry commandRegistry)
        {
            // Repositories
            serviceCollection.AddSingleton(PostRepository.Get());
            serviceCollection.AddSingleton(AuthorRepository.Get());
            
            // Validators
            serviceCollection.AddTransient<IValidator<CreatePostCommand>, CreatePostCommandValidator>();

            // Command Handlers
            commandRegistry.Discover(typeof(IServiceCollectionExtensions).Assembly);

            return serviceCollection;
        }
    }
}
