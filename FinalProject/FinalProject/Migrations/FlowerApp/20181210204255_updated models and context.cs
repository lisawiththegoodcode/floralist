using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject.Migrations.FlowerApp
{
    public partial class updatedmodelsandcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProposalItems_Images_ImageId",
                table: "ProposalItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProposalItems_Proposals_ProposalId",
                table: "ProposalItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Images_ImageId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ImageId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProposalItems",
                table: "ProposalItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Designers",
                table: "Designers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Tags");

            migrationBuilder.RenameIndex(
                name: "IX_Proposals_DesignerId",
                table: "Proposals",
                newName: "IX_Proposal_Designer");

            migrationBuilder.RenameIndex(
                name: "IX_Proposals_CustomerId",
                table: "Proposals",
                newName: "IX_Proposal_Customer");

            migrationBuilder.RenameIndex(
                name: "IX_ProposalItems_ProposalId",
                table: "ProposalItems",
                newName: "IX_ProposalItem_Proposal");

            migrationBuilder.RenameIndex(
                name: "IX_ProposalItems_ImageId",
                table: "ProposalItems",
                newName: "IX_ProposalItem_Image");

            migrationBuilder.AlterColumn<int>(
                name: "ProposalId",
                table: "ProposalItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "ProposalItems",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProposalItems",
                table: "ProposalItems",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Designers",
                table: "Designers",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateTable(
                name: "ImageTags",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageTags", x => new { x.ImageId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ImageTags_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageTags_TagId",
                table: "ImageTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProposalItems_Images_ImageId",
                table: "ProposalItems",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProposalItems_Proposals_ProposalId",
                table: "ProposalItems",
                column: "ProposalId",
                principalTable: "Proposals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProposalItems_Images_ImageId",
                table: "ProposalItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProposalItems_Proposals_ProposalId",
                table: "ProposalItems");

            migrationBuilder.DropTable(
                name: "ImageTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProposalItems",
                table: "ProposalItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Designers",
                table: "Designers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_Proposal_Designer",
                table: "Proposals",
                newName: "IX_Proposals_DesignerId");

            migrationBuilder.RenameIndex(
                name: "IX_Proposal_Customer",
                table: "Proposals",
                newName: "IX_Proposals_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProposalItem_Proposal",
                table: "ProposalItems",
                newName: "IX_ProposalItems_ProposalId");

            migrationBuilder.RenameIndex(
                name: "IX_ProposalItem_Image",
                table: "ProposalItems",
                newName: "IX_ProposalItems_ImageId");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Tags",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProposalId",
                table: "ProposalItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "ProposalItems",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProposalItems",
                table: "ProposalItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Designers",
                table: "Designers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ImageId",
                table: "Tags",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProposalItems_Images_ImageId",
                table: "ProposalItems",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProposalItems_Proposals_ProposalId",
                table: "ProposalItems",
                column: "ProposalId",
                principalTable: "Proposals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Images_ImageId",
                table: "Tags",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
