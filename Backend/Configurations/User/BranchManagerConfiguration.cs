using System.Runtime.Intrinsics.X86;
using Backend.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Backend.Configurations{
    public class BranchManagerConfiguration : IEntityTypeConfiguration<Branch_Manager>{
        public void Configure(EntityTypeBuilder<Branch_Manager> builder){
                builder.ToTable("Branch_Manager")
                        .HasKey(bm => bm.Branch_ManagerID);
                builder.Property(bm=>bm.Branch_ManagerID)
                        .HasColumnName("Branch_Manager_ID");


                builder.Property(c => c.Salary)
                        .IsRequired()
                        .HasColumnType("decimal(10,2)")
                        .HasDefaultValue(5000);

                builder.Property(c => c.Penalties)
                        .HasDefaultValue(0);

                builder.Property(c => c.Bonuses)
                        .HasDefaultValue(0);

                builder.Property(c => c.Hire_Date)
                        .IsRequired()
                        .HasColumnType("DATE");
                
                builder.Property(bm=>bm.Renewal_Date)
                        .HasColumnType("DATE");

                builder.Property(c => c.Fire_Date)
                        .HasColumnType("DATE");

                builder.Property(bm=>bm.Employee_Under_Supervision)
                        .IsRequired()
                        .HasDefaultValue(0);  

                builder.Property(bm=>bm.Contract_Length)
                        .HasDefaultValue(5);

                builder.HasOne(u => u.User)
                        .WithOne()
                        .HasForeignKey<Branch_Manager>(bm =>bm.Branch_ManagerID)
                        .OnDelete(DeleteBehavior.Cascade);
                
                builder.HasOne(b => b.Branch)
                        .WithOne()
                        .HasForeignKey<Branch_Manager>(bm=>bm.Manages_Branch_ID)
                        .OnDelete(DeleteBehavior.SetNull); 
                
        }
    }
}