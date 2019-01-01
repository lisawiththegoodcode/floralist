﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations
{
    public partial class AddedUserIdProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Images_Designers_DesignerId",
            //    table: "Image");

            //migrationBuilder.AlterColumn<int>(
            //    name: "DesignerId",
            //    table: "Image",
            //    nullable: true,
            //    oldClrType: typeof(int));

            //migrationBuilder.AddColumn<string>(
            //    name: "UserId",
            //    table: "Designers",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "UserId",
            //    table: "Customers",
            //    nullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Images_Designers_DesignerId",
            //    table: "Image",
            //    column: "DesignerId",
            //    principalTable: "Designers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Images_Designers_DesignerId",
            //    table: "Image");

            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "Designers");

            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "Customers");

            //migrationBuilder.AlterColumn<int>(
            //    name: "DesignerId",
            //    table: "Image",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldNullable: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Images_Designers_DesignerId",
            //    table: "Image",
            //    column: "DesignerId",
            //    principalTable: "Designers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
