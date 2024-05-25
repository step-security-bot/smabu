using LIT.Smabu.Domain;
using LIT.Smabu.Infrastructure;
using LIT.Smabu.UseCases;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/{0}.cshtml");
    }); 
builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructureServices(builder.Environment.IsDevelopment());
builder.Services.AddDomainServices();
builder.Services.AddUseCasesServices(builder.Environment.IsDevelopment());


var app = builder.Build();
app.SeedDatabaseAsync().GetAwaiter().GetResult();
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
