using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Backend.Configurations{

    public class RepoortConfiguration : IEntityTypeConfiguration<Report>{
        public void Configure(EntityTypeBuilder<Report> builder){
            builder.ToTable("Reports")
                    .HasKey(r => r.ReportID);
            builder.Property(r=>r.ReportID)
                    .HasColumnName("Report_ID")
                    .ValueGeneratedOnAdd();
            
            builder.Property(r=>r.Title)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(r=>r.GeneratedDate)
                    .HasColumnName("Generated_Date")
                    .HasColumnType("date")
                    .IsRequired();
            
            builder.Property(r=>r.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValue("Monthly Report");
            
            builder.Property(r=>r.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValue("To be sent");
            
            builder.Property(r=>r.Content)
                    .HasMaxLength(500)
                    .IsRequired();

            builder.HasOne(bm=>bm.Branch_Manager)
                    .WithMany(r=>r.Reports)
                    .HasForeignKey(r=>r.ManagerReportedID)
                    .OnDelete(DeleteBehavior.SetNull);
            
            builder.Property(r=>r.ManagerReportedID)
                    .HasColumnName("Manager_Reported_ID");
    }
    }
    
}