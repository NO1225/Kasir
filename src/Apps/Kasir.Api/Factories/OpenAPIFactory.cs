using Mapster.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema.Generation;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
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
            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "Kasir API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            services.AddOpenApiDocument(options =>
            {
                options.Title = "Kasir API";

                options.SchemaNameGenerator = new NSwagNameGenerator();
                //// Adding swagger document
                //options.SwaggerDoc("v1.0", new OpenApiInfo() { Title = "Main API v1.0", Version = "v1.0" });

                //// Include the comments that we wrote in the documentation
                //options.IncludeXmlComments("United.Machines.Api.xml");
                //options.IncludeXmlComments("United.Machines.Core.xml");

                //// To use unique names with the requests and responses
                //options.CustomSchemaIds(x => x.FullName);

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

                options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));

                //// use the token provided with the endpoints call
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    { securitySchema, new[] { "Bearer" } }
                //});

                //options.AddEnumsWithValuesFixFilters(services, o =>
                //{
                //    // add schema filter to fix enums (add 'x-enumNames' for NSwag) in schema
                //    o.ApplySchemaFilter = true;

                //    // add parameter filter to fix enums (add 'x-enumNames' for NSwag) in schema parameters
                //    o.ApplyParameterFilter = true;

                //    // add document filter to fix enums displaying in swagger document
                //    o.ApplyDocumentFilter = true;

                //    // add descriptions from DescriptionAttribute or xml-comments to fix enums (add 'x-enumDescriptions' for schema extensions) for applied filters
                //    o.IncludeDescriptions = true;

                //    // get descriptions from DescriptionAttribute then from xml-comments
                //    o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;

                //    // the same for another xml-files...
                //});


                //options.AddOperationFilter(filter=> { 
                //    filter.
                //});

                //options.OperationFilter<AuthorizeCheckOperationFilter>();
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

            //app.UseSwagger();

            //// Add swagger UI
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Versioned API v1.0");

            //    c.DocExpansion(DocExpansion.None);

            //});

            return app;
        }

        internal class NSwagNameGenerator : DefaultSchemaNameGenerator, ISchemaNameGenerator
        {
            public override string Generate(Type type)
            {
                return type.FullName.Replace("Dto", "");
            }

        }

//        public class ApiSwaggerFilter : IOperationProcessor
//        {
//            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
//            {
//                var nonApiRoutes = swaggerDoc.Paths
//                    .Where(x => !x.Key.ToLower().StartsWith("/api/"))
//                    .ToList();
//                nonApiRoutes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
//            }

//            public bool Process(OperationProcessorContext context)
//            {
//                context.
//                throw new NotImplementedException();
//            }
//        }
//        public class AuthorizeCheckOperationFilter : IOperationFilter
//        {
//            public void Apply(OpenApiOperation operation, OperationFilterContext context)
//            {
//                var hasAuthorize =
//                      (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any() == false &&
//                      context.MethodInfo.GetCustomAttributes(true).OfType<AllowAnonymousAttribute>().Any() == false) && (
//                      context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
//                      || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

//                if (hasAuthorize)
//                {
//                    operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
//                    operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

//                    // Defining the security schema
//                    var securitySchema = new OpenApiSecurityScheme()
//                    {
//                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
//                        Name = "Authorization",
//                        In = ParameterLocation.Header,
//Type = SecuritySchemeType.Http,
//                        Scheme = "bearer",
//                        Reference = new OpenApiReference
//                        {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                        }
//                    };

//                    operation.Security = new List<OpenApiSecurityRequirement>
//                {
//                    new OpenApiSecurityRequirement
//                    {
//                        { securitySchema, new[] { "Bearer" } }
//                    }
//                };

//                }

//            }
//        }
    }
}
