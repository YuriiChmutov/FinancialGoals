using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FinancialGoals.API.Filters
{
    public class SwaggerAuthorizationOperationFilter : IOperationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SwaggerAuthorizationOperationFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var token = GetToken();
            if (!string.IsNullOrEmpty(token))
            {
                operation.Security ??= new List<OpenApiSecurityRequirement>();
                var securityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                };
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [securityScheme] = new List<string>()
                });
                operation.Parameters ??= new List<OpenApiParameter>();
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Access token",
                    Required = true,
                    Schema = new OpenApiSchema { Type = "string" },
                    Example = new OpenApiString(token)
                });
            }
        }

        private string GetToken()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
            {
                return token[7..];
            }
            return null;
        }
    }
}
