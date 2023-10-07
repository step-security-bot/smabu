using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using LIT.Smabu.Infrastructure.Mapping;
using LIT.Smabu.Infrastructure.Persistence;
using LIT.Smabu.Business.Service.Extensions;
using LIT.Smabu.Server.Services;
using LIT.Smabu.Shared.Identity;
using LIT.Smabu.Infrastructure.Shared.Contracts;
using LIT.Smabu.Business.Service.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
        .EnableTokenAcquisitionToCallDownstreamApi()
            .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
            .AddInMemoryTokenCaches();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddMediatR();

builder.Services.AddFileAggregateStore();
builder.Services.AddAggregateResolver();
builder.Services.AddMapping<MapperSettings>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
