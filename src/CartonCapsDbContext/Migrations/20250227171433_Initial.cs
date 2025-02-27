using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CartonCapsDbContext.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAccountID = table.Column<int>(type: "int", nullable: false),
                    SenderReferralCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvitedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvitedFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvitedLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvitedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AcceptedAccountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Invitations_Accounts_AcceptedAccountID",
                        column: x => x.AcceptedAccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitations_Accounts_SenderAccountID",
                        column: x => x.SenderAccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_AcceptedAccountID",
                table: "Invitations",
                column: "AcceptedAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_SenderAccountID",
                table: "Invitations",
                column: "SenderAccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
