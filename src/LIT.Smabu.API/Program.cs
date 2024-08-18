using MediatR;
using LIT.Smabu.UseCases;
using LIT.Smabu.Domain;
using LIT.Smabu.Infrastructure;
using Microsoft.OpenApi.Models;
using LIT.Smabu.API.Endpoints;

var builder = WebApplication.CreateSlimBuilder(args);

var azureClientId = builder.Configuration["AzureAD:ClientId"]!;
var azureClientSecret = builder.Configuration["AzureAD:ClientSecret"]!;

builder.Services.AddEndpointsApiExplorer();

AddSwagger(builder, azureClientId);

builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructureServices();
builder.Services.AddDomainServices();
builder.Services.AddUseCasesServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.{json|yaml}";
    });
    app.UseSwaggerUI(options =>
    {
        options.OAuthAppName("Swagger Client");
        options.OAuthClientId(azureClientId);
        options.OAuthClientSecret(azureClientSecret);
        options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    });
}

app.RegisterCustomersEndpoints();
app.RegisterInvoicesEndpoints();

app.Run();

static void AddSwagger(WebApplicationBuilder builder, string azureClientId)
{
    builder.Services.AddSwaggerGen(c =>
    {
        var scopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ')?.ToDictionary(x => x) ?? [];
        scopes.Add($"api://{azureClientId}/access_as_user", "Access application on user behalf");
        c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                },
                Scheme = "oauth2",
                Name = "oauth2",
                In = ParameterLocation.Header
            },
            new List <string> ()
        }
    });
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow()
                {
                    AuthorizationUrl = new Uri("https://login.microsoftonline.com/common/oauth2/v2.0/authorize"),
                    TokenUrl = new Uri("https://login.microsoftonline.com/common/common/v2.0/token"),
                    Scopes = scopes
                }
            }
        });
    });
}