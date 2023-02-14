using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CoreEntitesChanged2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Folders_FolderId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_UserId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "Server",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Files");

            migrationBuilder.AddColumn<Guid>(
                name: "AccessLevelId",
                table: "Folders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FolderColorId",
                table: "Folders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FolderIconId",
                table: "Folders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FolderTypeId",
                table: "Folders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AccessLevelId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AccessLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FolderColors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FolderIcons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IconCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderIcons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FolderTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Folders_AccessLevelId",
                table: "Folders",
                column: "AccessLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_FolderColorId",
                table: "Folders",
                column: "FolderColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_FolderIconId",
                table: "Folders",
                column: "FolderIconId");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_FolderTypeId",
                table: "Folders",
                column: "FolderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_AccessLevelId",
                table: "Files",
                column: "AccessLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_AccessLevels_AccessLevelId",
                table: "Files",
                column: "AccessLevelId",
                principalTable: "AccessLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Folders_FolderId",
                table: "Files",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_AccessLevels_AccessLevelId",
                table: "Folders",
                column: "AccessLevelId",
                principalTable: "AccessLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_FolderColors_FolderColorId",
                table: "Folders",
                column: "FolderColorId",
                principalTable: "FolderColors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_FolderIcons_FolderIconId",
                table: "Folders",
                column: "FolderIconId",
                principalTable: "FolderIcons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_FolderTypes_FolderTypeId",
                table: "Folders",
                column: "FolderTypeId",
                principalTable: "FolderTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_UserId",
                table: "Folders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_AccessLevels_AccessLevelId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Folders_FolderId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_AccessLevels_AccessLevelId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_FolderColors_FolderColorId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_FolderIcons_FolderIconId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_FolderTypes_FolderTypeId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_UserId",
                table: "Folders");

            migrationBuilder.DropTable(
                name: "AccessLevels");

            migrationBuilder.DropTable(
                name: "FolderColors");

            migrationBuilder.DropTable(
                name: "FolderIcons");

            migrationBuilder.DropTable(
                name: "FolderTypes");

            migrationBuilder.DropIndex(
                name: "IX_Folders_AccessLevelId",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_FolderColorId",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_FolderIconId",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_FolderTypeId",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Files_AccessLevelId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "AccessLevelId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "FolderColorId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "FolderIconId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "FolderTypeId",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "AccessLevelId",
                table: "Files");

            migrationBuilder.AddColumn<byte>(
                name: "AccessLevel",
                table: "Folders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Server",
                table: "Folders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Folders_FolderId",
                table: "Files",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_UserId",
                table: "Folders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
