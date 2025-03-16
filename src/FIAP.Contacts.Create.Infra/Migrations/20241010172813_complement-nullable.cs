using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP.Contacts.Create.Infra.Migrations
{
    /// <inheritdoc />
    public partial class complementnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Addresses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Phones",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMPTZ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Phones",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMPTZ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contacts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMPTZ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contacts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMPTZ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Addresses",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMPTZ");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Addresses",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMPTZ");

            migrationBuilder.AlterColumn<string>(
                name: "Complement",
                table: "Addresses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Phones",
                type: "TIMESTAMPTZ",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Phones",
                type: "TIMESTAMPTZ",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Phones",
                type: "TIMESTAMPTZ",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Contacts",
                type: "TIMESTAMPTZ",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Contacts",
                type: "TIMESTAMPTZ",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Contacts",
                type: "TIMESTAMPTZ",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Addresses",
                type: "TIMESTAMPTZ",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Addresses",
                type: "TIMESTAMPTZ",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Complement",
                table: "Addresses",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Addresses",
                type: "TIMESTAMPTZ",
                nullable: true);
        }
    }
}
