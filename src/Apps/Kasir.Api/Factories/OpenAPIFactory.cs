using Mapster.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema.Generation;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kasir.Api.Factories
{
    public static class OpenAPIFactory
    {
        public static IServiceCollection AddOurOpenAPI(this IServiceCollection services)
        {

            services.AddOpenApiDocument(options =>
            {
                options.Title = "Kasir API";

                options.SchemaNameGenerator = new NSwagNameGenerator();

                // Defining the security schema
                var securitySchema = new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.Http,
                    Scheme = "bearer",
                };

                // Adding the bearer token authentaction option to the ui
                options.AddSecurity("Bearer", securitySchema);

                options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));

            });

            return services;
        }

        public static IApplicationBuilder ConfigureOurOpenAPI(this IApplicationBuilder app)
        {

            app.UseOpenApi();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            return app;
        }

        internal class NSwagNameGenerator : DefaultSchemaNameGenerator, ISchemaNameGenerator
        {
            public override string Generate(Type type)
            {
                return type.FullName.Replace("Dto", "");
            }

        }

    }
}
