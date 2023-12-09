using Microsoft.EntityFrameworkCore;
using SoccerFantasy.Models;
 using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

var builder = WebApplication.CreateBuilder(args);
//
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); // Adds hot-reloading
builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlite(builder.Configuration["ConnectionStrings:MatchesConnection"]);
    opts.UseSqlite(builder.Configuration["ConnectionStrings:GoalsConnection"]);
    opts.UseSqlite(builder.Configuration["ConnectionStrings:FantasyLeaguesConnection"]);
    opts.UseSqlite(builder.Configuration["ConnectionStrings:FantasyTeamsConnection"]);
    opts.UseSqlite(builder.Configuration["ConnectionStrings:TeamsConnection"]);
    opts.UseSqlite(builder.Configuration["ConnectionStrings:UsersConnection"]);
    opts.UseSqlite(builder.Configuration["ConnectionStrings:PlayersConnection"]);
    opts.UseSqlite(builder.Configuration["ConnectionStrings:FantasyTeamPlayersConnection"]);
    opts.EnableSensitiveDataLogging(true);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/api", async (context) =>
{
    await context.Response.WriteAsync("Hello World");
});

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
var matchesGenerator = new MatchesGenerator(context);
//matchesGenerator.GenerateMatches();

DataInit dataInit = new DataInit(context);

app.UseHttpsRedirection();
app.UseMiddleware<CustomMiddleware>();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Teams}/{action=Index}/{id?}");

app.Run();

