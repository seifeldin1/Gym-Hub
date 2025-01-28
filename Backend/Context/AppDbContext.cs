using Microsoft.EntityFrameworkCore;
using Backend.DbModels;
using Microsoft.Extensions.Configuration;

namespace Backend.Context
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        // Parameterless constructor for design-time services
        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 41)));
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Branch_Manager> Branch_Managers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BlacklistedToken> blacklistedTokens { get; set; }
        public DbSet<SupplementsNeeded> supplementsNeeded { get; set; }
        public DbSet<Supplement> supplements { get; set; }
        public DbSet<Equipment> equipments { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Candidate> candidates { get; set; }
        public DbSet<Application> applications { get; set; }
        public DbSet<InterviewTime> interviewTimes { get; set; }
        public DbSet<PerformWorkout> performWorkouts { get; set; }
        public DbSet<Workout> workouts { get; set; }
        public DbSet<Recommendation> recommendations { get; set; }
        public DbSet<Progress> Progress { get; set; }
        public DbSet<Meeting> Meeting { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<ClientProgress> ClientProgress { get; set; }
        public DbSet<Announcement> Announcement { get; set; }
        public DbSet<Nutrition> Nutrition { get; set; }
        public DbSet<Diet> Diet { get; set; }
        public DbSet<Report> Report { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Automatically apply configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Example for manual configurations if needed
            // modelBuilder.ApplyConfiguration(new EventConfiguration());
        }
    }
}
