using Autofac;
using Autofac.Extensions.DependencyInjection;
using LIT.Smabu.Domain;
using LIT.Smabu.Domain.CustomerAggregate;
using LIT.Smabu.Infrastructure;
using LIT.Smabu.UseCases.Customers.Create;
using QuestPDF.Infrastructure;
using System.Reflection;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/{0}.cshtml");
    }); 
builder.Services.AddHttpContextAccessor();

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultDomainModule());
    containerBuilder.RegisterModule(new AutofacInfrastructureModule(builder.Environment.IsDevelopment(),
        [
            Assembly.GetAssembly(typeof(Customer))!,
            Assembly.GetAssembly(typeof(AutofacInfrastructureModule))!,
            Assembly.GetAssembly(typeof(CreateCustomerCommand))!
        ]));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
