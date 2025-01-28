using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class TablesCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BlacklistedTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlacklistedTokens", x => x.Token);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Branch_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Branch_Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Opening_Hour = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    Closing_Hour = table.Column<TimeOnly>(type: "TIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Branch_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Candidate_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    First_Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Last_Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    National_Number = table.Column<long>(type: "bigint", maxLength: 18, nullable: false),
                    Phone_Number = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResumeLink = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LinkedinAccountLink = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Candidate_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Holiday",
                columns: table => new
                {
                    Holiday_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Start_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holiday", x => x.Holiday_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Nutrition",
                columns: table => new
                {
                    Nutrition_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Goal = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Protein_grams = table.Column<int>(type: "int", nullable: false),
                    Carbohydrates_grams = table.Column<int>(type: "int", nullable: false),
                    Fat_grams = table.Column<int>(type: "int", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrition", x => x.Nutrition_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Supplements",
                columns: table => new
                {
                    Supplement_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Brand = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Selling_Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Purchased_Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Flavor = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "No Flavor")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Manufactured_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Expiration_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Purchase_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Scoop_Size_grams = table.Column<int>(type: "int", nullable: false),
                    Scoop_Number_package = table.Column<int>(type: "int", nullable: false),
                    Scoop_Detail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplements", x => x.Supplement_ID);
                    table.CheckConstraint("CK_Flavor", "`Flavor` IN ('Vanilla', 'Chocolate', 'No Flavor')");
                    table.CheckConstraint("CK_ManufacturedDate_ExpirationDate", "`Manufactured_Date` < `Expiration_Date`");
                    table.CheckConstraint("CK_ScoopSize_ScoopNumber", "`Scoop_Size_grams` > 0 AND `Scoop_Number_package` > 0");
                    table.CheckConstraint("CK_SellingPrice_PurchasedPrice", "`Selling_Price` >= `Purchased_Price` AND `Purchased_Price` > 0");
                    table.CheckConstraint("CK_Type", "`Type` IN ('Protein', 'Vitamins', 'Creatine')");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHashed = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    First_Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Last_Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone_Number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Age = table.Column<int>(type: "int", nullable: true),
                    National_Number = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    Equipment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "Available")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Purchase_Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Purchase_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SerialNumber = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Belong_to_Branch_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.Equipment_ID);
                    table.ForeignKey(
                        name: "FK_Equipment_Branch_Belong_to_Branch_ID",
                        column: x => x.Belong_to_Branch_ID,
                        principalTable: "Branch",
                        principalColumn: "Branch_ID",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Job_Posting",
                columns: table => new
                {
                    Post_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Branch_Posted_ID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date_Posted = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Skills_Required = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Experience_Years_Required = table.Column<int>(type: "int", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Location = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Posting", x => x.Post_ID);
                    table.ForeignKey(
                        name: "FK_Job_Posting_Branch_Branch_Posted_ID",
                        column: x => x.Branch_Posted_ID,
                        principalTable: "Branch",
                        principalColumn: "Branch_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Supplements_Needed",
                columns: table => new
                {
                    Supplement_ID = table.Column<int>(type: "int", nullable: false),
                    Nutrition_Plan_ID = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reason = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Start_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    End_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Mandatory = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Scoops_Per_Day_Of_Usage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplements_Needed", x => new { x.Supplement_ID, x.Nutrition_Plan_ID });
                    table.ForeignKey(
                        name: "FK_Supplements_Needed_Nutrition_Nutrition_Plan_ID",
                        column: x => x.Nutrition_Plan_ID,
                        principalTable: "Nutrition",
                        principalColumn: "Nutrition_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supplements_Needed_Supplements_Supplement_ID",
                        column: x => x.Supplement_ID,
                        principalTable: "Supplements",
                        principalColumn: "Supplement_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Announcements_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Author_ID = table.Column<int>(type: "int", nullable: false),
                    Author_Role = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date_Posted = table.Column<DateTime>(type: "datetime", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Announcements_ID);
                    table.ForeignKey(
                        name: "FK_Announcements_User_Author_ID",
                        column: x => x.Author_ID,
                        principalTable: "User",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Branch_Manager",
                columns: table => new
                {
                    Branch_Manager_ID = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 5000m),
                    Penalties = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Bonuses = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Hire_Date = table.Column<DateOnly>(type: "DATE", nullable: false),
                    Employee_Under_Supervision = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Fire_Date = table.Column<DateOnly>(type: "DATE", nullable: true),
                    Renewal_Date = table.Column<DateOnly>(type: "DATE", nullable: true),
                    Manages_Branch_ID = table.Column<int>(type: "int", nullable: true),
                    Contract_Length = table.Column<int>(type: "int", nullable: true, defaultValue: 5),
                    BranchID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch_Manager", x => x.Branch_Manager_ID);
                    table.ForeignKey(
                        name: "FK_Branch_Manager_Branch_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branch",
                        principalColumn: "Branch_ID");
                    table.ForeignKey(
                        name: "FK_Branch_Manager_Branch_Manages_Branch_ID",
                        column: x => x.Manages_Branch_ID,
                        principalTable: "Branch",
                        principalColumn: "Branch_ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Branch_Manager_User_Branch_Manager_ID",
                        column: x => x.Branch_Manager_ID,
                        principalTable: "User",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Coach",
                columns: table => new
                {
                    Coach_ID = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 5000m),
                    Penalties = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Bonuses = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Hire_Date = table.Column<DateOnly>(type: "DATE", nullable: false),
                    Fire_Date = table.Column<DateOnly>(type: "DATE", nullable: true),
                    Experience_Years = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    Works_For_Branch = table.Column<int>(type: "int", nullable: true),
                    Daily_Hours_Worked = table.Column<int>(type: "int", nullable: false),
                    Shift_Start = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    Shift_Ends = table.Column<TimeOnly>(type: "TIME", nullable: false),
                    Speciality = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Contract_Length = table.Column<int>(type: "int", nullable: true, defaultValue: 5),
                    Renewal_Date = table.Column<DateOnly>(type: "DATE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coach", x => x.Coach_ID);
                    table.ForeignKey(
                        name: "FK_Coach_Branch_Works_For_Branch",
                        column: x => x.Works_For_Branch,
                        principalTable: "Branch",
                        principalColumn: "Branch_ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Coach_User_Coach_ID",
                        column: x => x.Coach_ID,
                        principalTable: "User",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Owner_ID = table.Column<int>(type: "int", nullable: false),
                    Established_branches = table.Column<int>(type: "int", nullable: false),
                    SharePercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Owner_ID);
                    table.ForeignKey(
                        name: "FK_Owner_User_Owner_ID",
                        column: x => x.Owner_ID,
                        principalTable: "User",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Applicant_ID = table.Column<int>(type: "int", nullable: false),
                    Post_ID = table.Column<int>(type: "int", nullable: false),
                    Applied_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Years_of_Experience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => new { x.Applicant_ID, x.Post_ID });
                    table.ForeignKey(
                        name: "FK_Applications_Candidate_Applicant_ID",
                        column: x => x.Applicant_ID,
                        principalTable: "Candidate",
                        principalColumn: "Candidate_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Job_Posting_Post_ID",
                        column: x => x.Post_ID,
                        principalTable: "Job_Posting",
                        principalColumn: "Post_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Event_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Start_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_By_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Event_ID);
                    table.ForeignKey(
                        name: "FK_Events_Branch_Manager_Created_By_ID",
                        column: x => x.Created_By_ID,
                        principalTable: "Branch_Manager",
                        principalColumn: "Branch_Manager_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Interview_Times",
                columns: table => new
                {
                    Interview_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Manager_ID = table.Column<int>(type: "int", nullable: true),
                    Free_Interview_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, defaultValue: "Available")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview_Times", x => x.Interview_ID);
                    table.ForeignKey(
                        name: "FK_Interview_Times_Branch_Manager_Manager_ID",
                        column: x => x.Manager_ID,
                        principalTable: "Branch_Manager",
                        principalColumn: "Branch_Manager_ID",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Report_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Manager_Reported_ID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Generated_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "Monthly Report")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "To be sent")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Content = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Report_ID);
                    table.ForeignKey(
                        name: "FK_Reports_Branch_Manager_Manager_Reported_ID",
                        column: x => x.Manager_Reported_ID,
                        principalTable: "Branch_Manager",
                        principalColumn: "Branch_Manager_ID",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Client_ID = table.Column<int>(type: "int", nullable: false),
                    Join_Date = table.Column<DateOnly>(type: "date", nullable: false),
                    BMR = table.Column<int>(type: "int", nullable: true),
                    WeightKg = table.Column<double>(type: "double", nullable: true),
                    HeightCm = table.Column<double>(type: "double", nullable: true),
                    Belong_To_Coach_ID = table.Column<int>(type: "int", nullable: true),
                    AccountActivated = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    Start_Date_Membership = table.Column<DateOnly>(type: "date", nullable: false),
                    End_Date_Membership = table.Column<DateOnly>(type: "date", nullable: false),
                    Membership_Type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValue: "Silver")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fees_Of_Membership = table.Column<int>(type: "int", nullable: false),
                    Membership_Period_Months = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Client_ID);
                    table.ForeignKey(
                        name: "FK_Client_Coach_Belong_To_Coach_ID",
                        column: x => x.Belong_To_Coach_ID,
                        principalTable: "Coach",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Client_User_Client_ID",
                        column: x => x.Client_ID,
                        principalTable: "User",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Meeting_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Coach_ID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Meeting_ID);
                    table.ForeignKey(
                        name: "FK_Meetings_Coach_Coach_ID",
                        column: x => x.Coach_ID,
                        principalTable: "Coach",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Skill_Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Coach_Skilled_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => new { x.Skill_Name, x.Coach_Skilled_ID });
                    table.ForeignKey(
                        name: "FK_Skills_Coach_Coach_Skilled_ID",
                        column: x => x.Coach_Skilled_ID,
                        principalTable: "Coach",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    Workout_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Muscle_Targeted = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Goal = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created_By_Coach_ID = table.Column<int>(type: "int", nullable: true),
                    Calories_Burnt = table.Column<int>(type: "int", nullable: false),
                    Reps_Per_Set = table.Column<int>(type: "int", nullable: true),
                    Sets = table.Column<int>(type: "int", nullable: true),
                    Duration_Min = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.Workout_ID);
                    table.ForeignKey(
                        name: "FK_Workout_Coach_Created_By_Coach_ID",
                        column: x => x.Created_By_Coach_ID,
                        principalTable: "Coach",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ClientProgress",
                columns: table => new
                {
                    Client_Progress_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Client_ID = table.Column<int>(type: "int", nullable: true),
                    Coach_ID = table.Column<int>(type: "int", nullable: true),
                    ReportDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProgressSummary = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GoalsAchieved = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ChallengesFaced = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NextSteps = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProgress", x => x.Client_Progress_ID);
                    table.ForeignKey(
                        name: "FK_ClientProgress_Client_Client_ID",
                        column: x => x.Client_ID,
                        principalTable: "Client",
                        principalColumn: "Client_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientProgress_Coach_Coach_ID",
                        column: x => x.Coach_ID,
                        principalTable: "Coach",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Diet",
                columns: table => new
                {
                    Nutrition_Plan_ID = table.Column<int>(type: "int", nullable: false),
                    Client_Assigned_To_ID = table.Column<int>(type: "int", nullable: false),
                    Supplement_ID = table.Column<int>(type: "int", nullable: false),
                    Coach_Created_ID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, defaultValue: "Not choosed")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Start_Date = table.Column<DateOnly>(type: "DATE", nullable: false),
                    End_Date = table.Column<DateOnly>(type: "DATE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diet", x => new { x.Nutrition_Plan_ID, x.Client_Assigned_To_ID });
                    table.ForeignKey(
                        name: "FK_Diet_Client_Client_Assigned_To_ID",
                        column: x => x.Client_Assigned_To_ID,
                        principalTable: "Client",
                        principalColumn: "Client_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diet_Coach_Coach_Created_ID",
                        column: x => x.Coach_Created_ID,
                        principalTable: "Coach",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Diet_Nutrition_Nutrition_Plan_ID",
                        column: x => x.Nutrition_Plan_ID,
                        principalTable: "Nutrition",
                        principalColumn: "Nutrition_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diet_Supplements_Supplement_ID",
                        column: x => x.Supplement_ID,
                        principalTable: "Supplements",
                        principalColumn: "Supplement_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Progress",
                columns: table => new
                {
                    Progress_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Client_ID = table.Column<int>(type: "int", nullable: false),
                    Weight_Kg = table.Column<double>(type: "double", nullable: false),
                    DateInserted = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progress", x => x.Progress_ID);
                    table.ForeignKey(
                        name: "FK_Progress_Client_Client_ID",
                        column: x => x.Client_ID,
                        principalTable: "Client",
                        principalColumn: "Client_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Rating_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Coach_ID = table.Column<int>(type: "int", nullable: false),
                    Client_ID = table.Column<int>(type: "int", nullable: true),
                    Rate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Rating_ID);
                    table.ForeignKey(
                        name: "FK_Ratings_Client_Client_ID",
                        column: x => x.Client_ID,
                        principalTable: "Client",
                        principalColumn: "Client_ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ratings_Coach_Coach_ID",
                        column: x => x.Coach_ID,
                        principalTable: "Coach",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Recommendation",
                columns: table => new
                {
                    Recommendation_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Client_ID = table.Column<int>(type: "int", nullable: false),
                    Plan_ID = table.Column<int>(type: "int", nullable: true),
                    Supplement_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendation", x => x.Recommendation_ID);
                    table.ForeignKey(
                        name: "FK_Recommendation_Client_Client_ID",
                        column: x => x.Client_ID,
                        principalTable: "Client",
                        principalColumn: "Client_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendation_Nutrition_Plan_ID",
                        column: x => x.Plan_ID,
                        principalTable: "Nutrition",
                        principalColumn: "Nutrition_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendation_Supplements_Supplement_ID",
                        column: x => x.Supplement_ID,
                        principalTable: "Supplements",
                        principalColumn: "Supplement_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Perform_Workout",
                columns: table => new
                {
                    Workout_ID = table.Column<int>(type: "int", nullable: false),
                    Client_ID = table.Column<int>(type: "int", nullable: false),
                    Order_Of_Workout = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Day_Number = table.Column<int>(type: "int", nullable: false),
                    Performed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perform_Workout", x => new { x.Workout_ID, x.Client_ID });
                    table.ForeignKey(
                        name: "FK_Perform_Workout_Client_Client_ID",
                        column: x => x.Client_ID,
                        principalTable: "Client",
                        principalColumn: "Client_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Perform_Workout_Workout_Workout_ID",
                        column: x => x.Workout_ID,
                        principalTable: "Workout",
                        principalColumn: "Workout_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_Author_ID",
                table: "Announcements",
                column: "Author_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_Post_ID",
                table: "Applications",
                column: "Post_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Manager_BranchID",
                table: "Branch_Manager",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Manager_Manages_Branch_ID",
                table: "Branch_Manager",
                column: "Manages_Branch_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Email",
                table: "Candidate",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_National_Number",
                table: "Candidate",
                column: "National_Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Phone_Number",
                table: "Candidate",
                column: "Phone_Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_Belong_To_Coach_ID",
                table: "Client",
                column: "Belong_To_Coach_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProgress_Client_ID",
                table: "ClientProgress",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProgress_Coach_ID",
                table: "ClientProgress",
                column: "Coach_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Coach_Works_For_Branch",
                table: "Coach",
                column: "Works_For_Branch");

            migrationBuilder.CreateIndex(
                name: "IX_Diet_Client_Assigned_To_ID",
                table: "Diet",
                column: "Client_Assigned_To_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diet_Coach_Created_ID",
                table: "Diet",
                column: "Coach_Created_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Diet_Supplement_ID",
                table: "Diet",
                column: "Supplement_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_Belong_to_Branch_ID",
                table: "Equipment",
                column: "Belong_to_Branch_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_SerialNumber",
                table: "Equipment",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_Created_By_ID",
                table: "Events",
                column: "Created_By_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Interview_Times_Manager_ID",
                table: "Interview_Times",
                column: "Manager_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Posting_Branch_Posted_ID",
                table: "Job_Posting",
                column: "Branch_Posted_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_Coach_ID",
                table: "Meetings",
                column: "Coach_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Perform_Workout_Client_ID",
                table: "Perform_Workout",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Progress_Client_ID",
                table: "Progress",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Client_ID",
                table: "Ratings",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Coach_ID",
                table: "Ratings",
                column: "Coach_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_Client_ID",
                table: "Recommendation",
                column: "Client_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_Plan_ID",
                table: "Recommendation",
                column: "Plan_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_Supplement_ID",
                table: "Recommendation",
                column: "Supplement_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Manager_Reported_ID",
                table: "Reports",
                column: "Manager_Reported_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_Coach_Skilled_ID",
                table: "Skills",
                column: "Coach_Skilled_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Supplements_Needed_Nutrition_Plan_ID",
                table: "Supplements_Needed",
                column: "Nutrition_Plan_ID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_National_Number",
                table: "User",
                column: "National_Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workout_Created_By_Coach_ID",
                table: "Workout",
                column: "Created_By_Coach_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "BlacklistedTokens");

            migrationBuilder.DropTable(
                name: "ClientProgress");

            migrationBuilder.DropTable(
                name: "Diet");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Holiday");

            migrationBuilder.DropTable(
                name: "Interview_Times");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "Perform_Workout");

            migrationBuilder.DropTable(
                name: "Progress");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Recommendation");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Supplements_Needed");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "Job_Posting");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Branch_Manager");

            migrationBuilder.DropTable(
                name: "Nutrition");

            migrationBuilder.DropTable(
                name: "Supplements");

            migrationBuilder.DropTable(
                name: "Coach");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
