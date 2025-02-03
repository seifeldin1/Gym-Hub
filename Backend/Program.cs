using Backend.Context;
using Backend.Controllers;
using Backend.Hubs;
using Backend.Middleware;
using Backend.Services;
using Backend.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Add controllers with views support
builder.Services.AddControllersWithViews();

// Configure JWT Bearer Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9c1b3f43-df57-4a9a-88d3-b6e9e58c6f2e")),
            ValidateIssuer = false, // Optional: Set to true if you have a specific issuer
            ValidateAudience = false, // Optional: Set to true if you have a specific audience
        };
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

// Add Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CoachPolicy", policy => policy.RequireRole("Coach"));
    options.AddPolicy("OwnerPolicy", policy => policy.RequireRole("Owner"));
    options.AddPolicy("BranchManagerPolicy", policy => policy.RequireRole("BranchManager"));
    options.AddPolicy("ClientPolicy", policy => policy.RequireRole("Client"));
});

// Add custom JSON converters (if needed)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

// Register AppDbContext using MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 41)));
});

// Register application services
builder.Services.AddScoped<CredentialServices>();
builder.Services.AddScoped<ApplicationServices>();
builder.Services.AddScoped<WorkoutService>();
builder.Services.AddScoped<SupplementsServices>();
builder.Services.AddScoped<BranchService>();
builder.Services.AddScoped<JobPostingService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<EquipmentService>();
//builder.Services.AddScoped<NotificationServices>(); //! Under development
builder.Services.AddScoped<NutritionPlanService>();
builder.Services.AddScoped<CoachesServices>();
builder.Services.AddScoped<BranchManagerServices>();
builder.Services.AddScoped<PerformWorkoutService>();
builder.Services.AddScoped<AnnouncementsServices>();
builder.Services.AddScoped<SalaryServices>();
builder.Services.AddScoped<PenaltyServices>();
builder.Services.AddScoped<BonusServices>();
builder.Services.AddScoped<MeetingService>();
builder.Services.AddScoped<ProgressServices>();
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<TalentPoolService>();
builder.Services.AddScoped<ReportsServices>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<HolidayService>();
builder.Services.AddScoped<CalendarServices>();
builder.Services.AddScoped<RecommendationServices>();
builder.Services.AddScoped<StatisticsServices>();
builder.Services.AddScoped<InterviewService>();
builder.Services.AddScoped<SignUpCheckerServices>();
builder.Services.AddScoped<InterestServices>();
builder.Services.AddScoped<SessionService>();

// Add SignalR for real-time communications
builder.Services.AddSignalR();

// Build the application
var app = builder.Build();

// Configure SignalR hubs
app.MapHub<NotificationHub>("/notifications");

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Automatically apply EF Core migrations
using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate(); // Apply pending migrations
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
    }
}

app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Ensure Authentication and Authorization middleware is properly configured
app.UseAuthentication();
app.UseAuthorization();

// Map controllers for routing
app.MapControllers();

// Default route for controllers (if needed)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run the application
app.Run();
