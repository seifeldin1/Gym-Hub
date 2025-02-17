using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class EditBranchManagerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_Manager_Branch_Manages_Branch_ID",
                table: "Branch_Manager");

            migrationBuilder.DropIndex(
                name: "IX_Branch_Manager_Manages_Branch_ID",
                table: "Branch_Manager");

            migrationBuilder.DropColumn(
                name: "Manages_Branch_ID",
                table: "Branch_Manager");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Manages_Branch_ID",
                table: "Branch_Manager",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branch_Manager_Manages_Branch_ID",
                table: "Branch_Manager",
                column: "Manages_Branch_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_Manager_Branch_Manages_Branch_ID",
                table: "Branch_Manager",
                column: "Manages_Branch_ID",
                principalTable: "Branch",
                principalColumn: "Branch_ID",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
