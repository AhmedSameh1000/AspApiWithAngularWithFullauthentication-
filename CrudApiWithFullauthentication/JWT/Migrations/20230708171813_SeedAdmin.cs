using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO[dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES(N'd8e6f11d-a53f-47ba-a406-61dc4c4d95f8', N'Ahmed', N'Sameh', N'Ahmeds14', N'AHMEDS14', N'Ahmed@gmail.com', N'AHMED@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEP8dF4QbFEbn0F6TXYPAt0dHAgWaNhXd5DmIoZ0vs4JeSQpm3ebHuC5L09OrBDw4+A==', N'IVJWL2X7KX7PQLY7QPR45UKD6HHVC623', N'11c87045-656b-4844-9dfb-222c0e91cb8e', NULL, 0, 0, NULL, 1, 0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id = 'd8e6f11d-a53f-47ba-a406-61dc4c4d95f8'");
        }
    }
}