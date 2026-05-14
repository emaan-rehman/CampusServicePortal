using CampusServicePortal.Components;
using CampusServicePortal.Data;
using CampusServicePortal.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 2. Data Access Layer: Register Database Context with Retry Logic
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

// 3. Business Logic Layer: Register Repository
builder.Services.AddScoped<ICampusRepository, CampusRepository>();

var app = builder.Build();

// 4. Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

// Static files are mandatory for Bootstrap/Figma styles
app.UseStaticFiles();

app.UseAntiforgery();

// 5. Map Components - Use only ONCE to avoid routing errors
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();