using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class AuthUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Authorisation_AuthId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_AuthId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "People");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Authorisation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Authorisation_PersonId",
                table: "Authorisation",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Authorisation_People_PersonId",
                table: "Authorisation",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authorisation_People_PersonId",
                table: "Authorisation");

            migrationBuilder.DropIndex(
                name: "IX_Authorisation_PersonId",
                table: "Authorisation");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Authorisation");

            migrationBuilder.AddColumn<int>(
                name: "AuthId",
                table: "People",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_AuthId",
                table: "People",
                column: "AuthId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Authorisation_AuthId",
                table: "People",
                column: "AuthId",
                principalTable: "Authorisation",
                principalColumn: "Id");
        }
    }
}
