using System;
using System.Net.Http;
using AzureFromTheTrenches.Commanding;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.AspNetCore;
using AzureFromTheTrenches.Commanding.AspNetCore.Swashbuckle;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RestApi.Application;
using RestApi.Commands;
using RestApi.Web.Helpers;
using Swashbuckle.AspNetCore.Swagger;

namespace RestApi.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private IServiceProvider ServiceProvider { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure a dependency resolver adapter around IServiceCollection and add the commanding
            // system to the service collection
            ICommandingDependencyResolverAdapter commandingAdapter =
                new CommandingDependencyResolverAdapter(
                    (fromType,toInstance) => services.AddSingleton(fromType, toInstance),
                    (fromType,toType) => services.AddTransient(fromType, toType),
                    (resolveType) => ServiceProvider.GetService(resolveType)
                );
            ICommandRegistry commandRegistry = commandingAdapter.AddCommanding();

            services
                .AddApplication(commandRegistry)
                // We replace the default command dispatcher with one that understands how to translate validation responses into REST respones
                .Replace(new ServiceDescriptor(typeof(ICommandDispatcher), typeof(ApplicationErrorAwareCommandDispatcher), ServiceLifetime.Transient))
                // Add the MVC framework
                // NOTE: The FakeClaimsProvider should be REMOVED from production code. To aid in the example it adds a UserId claim into the pipeline
                .AddMvc(mvc => mvc.Filters.Add(new FakeClaimsProvider()))
                // Configure our REST endpoints based on commands
                .AddAspNetCoreCommanding(cfg => cfg
                    // first setup a default controller route - optional, we've added versioning
                    .DefaultControllerRoute("/api/v1/[controller]")
                    // configure our controller and actions
                    .Controller("Posts", controller => controller
                        .Action<GetPostQuery>(HttpMethod.Get, "{Id}")
                        .Action<GetPostsQuery,FromQueryAttribute>(HttpMethod.Get)
                        .Action<CreatePostCommand>(HttpMethod.Post)
                    )
                    // setup the system so that any property called AuthenticatedUserId will be mapped from
                    // the claim UserId
                    .Claims(mapping => mapping.MapClaimToPropertyName("UserId", "AuthenticatedUserId"))
                )
                // Add a validation system that is separated from command / type definition
                .AddFluentValidation();

            // Configure swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Headless Blog API", Version = "v1" });
                // Add the commanding specific configuration to Swagger - note this is only required
                // if you have properties marked [SecurityProperty] that you wish to hide from the API
                // definition
                c.AddAspNetCoreCommanding();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ServiceProvider = app.ApplicationServices;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Headless Blog API V1");
            });


            app.UseMvc();
        }
    }
}
