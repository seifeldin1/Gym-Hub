using Backend.Database;
using Backend.Controllers;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Backend.Middleware;
using Backend.Utils;
using Backend.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<GymDatabase>();      // Add Database service as Scoped (to be injected into controllers)

    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new Backend.Utils.DateOnlyJsonConverter());
    });
builder.Services.AddScoped<MySqlConnection>(provider =>
{
    // Get the connection string from the configuration
    var connectionString = builder.Configuration.GetConnectionString("myConnectionString"); //change the value for the myConnectionString , you will find it in "appsettings.Development.json" ... also you need to change the connection in the ProductDatabase

    return new MySqlConnection(connectionString);  // Return a new MySqlConnection using the connection string
});

// Register Services as Scoped, to be injected into controllers
builder.Services.AddScoped<CredentialServices>();
builder.Services.AddScoped<ApplicationServices>();
builder.Services.AddScoped<Workout>();
builder.Services.AddScoped<Supplements>();
builder.Services.AddScoped<Branch>();
builder.Services.AddScoped<JobPosting>();
builder.Services.AddScoped<Clients>();
builder.Services.AddScoped<Equipments>();
builder.Services.AddScoped<NotificationServices>();
builder.Services.AddScoped<NutritionPlan>();
builder.Services.AddScoped<CoachesServices>();
builder.Services.AddScoped<BranchManagers>();





builder.Services.AddSignalR();
//builder.Services.AddSingleton<NotificationServices>();

var app = builder.Build();
app.MapHub<NotificationHub>("/notifications");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var gymDatabase = services.GetRequiredService<GymDatabase>();  // Get the ProductDatabase service

    try
    {
        // Set up the database and tables if they don't exist
        gymDatabase.DatabaseSetUp();  // Initialize the database
    }
    catch (Exception ex)
    {
        // Log the exception (this is an example, implement proper logging)
        Console.WriteLine($"An error occurred while setting up the database: {ex.Message}");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
//app.UseMiddleware<AuthorizationMiddleware>();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
