using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linkd.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostandConnectionEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequests_AbpUsers_ReceiverId",
                table: "ConnectionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequests_AbpUsers_SenderId",
                table: "ConnectionRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Posts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "ConnectionRequests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "ConnectionRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ConnectionRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequests_AbpUsers_ReceiverId",
                table: "ConnectionRequests",
                column: "ReceiverId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequests_AbpUsers_SenderId",
                table: "ConnectionRequests",
                column: "SenderId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequests_AbpUsers_ReceiverId",
                table: "ConnectionRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionRequests_AbpUsers_SenderId",
                table: "ConnectionRequests");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "ConnectionRequests");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "ConnectionRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ConnectionRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequests_AbpUsers_ReceiverId",
                table: "ConnectionRequests",
                column: "ReceiverId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionRequests_AbpUsers_SenderId",
                table: "ConnectionRequests",
                column: "SenderId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
