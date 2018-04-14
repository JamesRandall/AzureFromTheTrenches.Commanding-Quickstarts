using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestApi.Web.Helpers
{
    // This resource filter allows us to inject a claim matching one of our authors to demonstrate
    // claims mapping
    public class FakeClaimsProvider : IResourceFilter
    {
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context.HttpContext.User != null)
            {
                if (!context.HttpContext.User.HasClaim(x => x.Type == "UserId"))
                {
                    context.HttpContext.User.AddIdentity(new ClaimsIdentity(new[]
                        {new Claim("UserId", "C8242561-222E-4533-802C-7BFC61B403E9")}));
                }                
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            
        }
    }
}
