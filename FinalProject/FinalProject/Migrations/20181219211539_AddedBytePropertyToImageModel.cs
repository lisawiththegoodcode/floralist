using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class AddedBytePropertyToImageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Designers_DesignerId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "DesignerId",
                table: "Images",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            //migrationBuilder.AddColumn<byte[]>(
            //    name: "FileImage",
            //    table: "Images",
            //    nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Designers_DesignerId",
                table: "Images",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Designers_DesignerId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "FileImage",
                table: "Images");

            //migrationBuilder.AlterColumn<int>(
            //    name: "DesignerId",
            //    table: "Images",
            //    nullable: true,
            //    oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Designers_DesignerId",
                table: "Images",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
