using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Services;

public class AuthOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authAttr = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Distinct();

        if (authAttr.Any())
        {
            operation.Responses.Add("401", new OpenApiResponse() { Description = "Unauthorized"});
            operation.Responses.Add("403", new OpenApiResponse() { Description = "Forbidden"});

            operation.Security = new List<OpenApiSecurityRequirement>();

            var seqReq = new List<OpenApiSecurityRequirement>();
            
            operation.Security.Add(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Description = "Add token to header",
                        Name = "Auth",
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    }, new List<string>()
                }
            });
            
        }
    }
}