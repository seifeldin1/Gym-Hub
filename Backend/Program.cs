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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()  // Allow requests from any origin
                .AllowAnyMethod()     // Allow any HTTP method (GET, POST, etc.)
                .AllowAnyHeader();    // Allow any headers
        });
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
builder.Services.AddScoped<PerformWorkout>();
builder.Services.AddScoped<UsersServices>();
builder.Services.AddScoped<AnnouncementsServices>();
builder.Services.AddScoped<SalaryServices>();
builder.Services.AddScoped<PenaltyServices>();
builder.Services.AddScoped<BonusServices>();
builder.Services.AddScoped<MeetingsServices>();
builder.Services.AddScoped<ProgressServices>();
builder.Services.AddScoped<Owner>();
builder.Services.AddScoped<TalentPoolServices>();
builder.Services.AddScoped<ReportsServices>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<HolidayService>();
builder.Services.AddScoped<ApplicationServices>();
builder.Services.AddScoped<CalendarServices>();
builder.Services.AddScoped<RecommendationServices>();

builder.Services.AddSignalR();

var app = builder.Build();
app.MapHub<NotificationHub>("/notifications");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable CORS with the policy before routing
app.UseCors();

app.UseRouting();
app.UseAuthentication();

app.MapControllers();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
