using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTApi.Migrations
{
    /// <inheritdoc />
    public partial class AssignAllRolesToAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUserRoles] (UserId, RoleId) SELECT 'd8e6f11d-a53f-47ba-a406-61dc4c4d95f8', Id FROM [dbo].[AspNetRoles]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUserRoles] WHERE UserId = 'd8e6f11d-a53f-47ba-a406-61dc4c4d95f8'");
        }
    }
}