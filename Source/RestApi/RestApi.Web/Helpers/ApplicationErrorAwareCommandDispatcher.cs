using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.Abstractions.Model;
using AzureFromTheTrenches.Commanding.AspNetCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestApi.Application;

namespace RestApi.Web.Helpers
{
    // The purpose of this is to translate well known errors from the internal applications
    // to REST friendly calls. This approach means that the command handlers can be agnostic
    // of calling protocols.
    public class ApplicationErrorAwareCommandDispatcher : ICommandDispatcher
    {
        private readonly IFrameworkCommandDispatcher _underlyingCommandDispatcher;

        public ApplicationErrorAwareCommandDispatcher(IFrameworkCommandDispatcher underlyingCommandDispatcher)
        {
            _underlyingCommandDispatcher = underlyingCommandDispatcher;
        }

        public async Task<CommandResult<TResult>> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                CommandResult<TResult> result = await _underlyingCommandDispatcher.DispatchAsync(command, cancellationToken);
                if (result.Result == null)
                {
                    throw new RestApiException(HttpStatusCode.NotFound);
                }
                return result;
            }
            catch (CommandModelException ex)
            {
                ModelStateDictionary modelStateDictionary = new ModelStateDictionary();
                modelStateDictionary.AddModelError(ex.Property, ex.Message);
                throw new RestApiException(HttpStatusCode.BadRequest, modelStateDictionary);
            }
        }

        public Task<CommandResult> DispatchAsync(ICommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            return _underlyingCommandDispatcher.DispatchAsync(command, cancellationToken);
        }

        public ICommandExecuter AssociatedExecuter { get; } = null;
    }
}
