using Microsoft.EntityFrameworkCore.Migrations;

namespace EFHotelQuiz.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelSpecial_Hotels_HotelsId",
                table: "HotelSpecial");

            migrationBuilder.DropForeignKey(
                name: "FK_Price_RoomType_RoomTypeId",
                table: "Price");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomType_Hotels_HotelId",
                table: "RoomType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Price",
                table: "Price");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelSpecial",
                table: "HotelSpecial");

            migrationBuilder.RenameTable(
                name: "RoomType",
                newName: "RoomTypes");

            migrationBuilder.RenameTable(
                name: "Price",
                newName: "RoomPrices");

            migrationBuilder.RenameTable(
                name: "HotelSpecial",
                newName: "HotelSpecials");

            migrationBuilder.RenameIndex(
                name: "IX_RoomType_HotelId",
                table: "RoomTypes",
                newName: "IX_RoomTypes_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Price_RoomTypeId",
                table: "RoomPrices",
                newName: "IX_RoomPrices_RoomTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelSpecial_HotelsId",
                table: "HotelSpecials",
                newName: "IX_HotelSpecials_HotelsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomTypes",
                table: "RoomTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomPrices",
                table: "RoomPrices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelSpecials",
                table: "HotelSpecials",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelSpecials_Hotels_HotelsId",
                table: "HotelSpecials",
                column: "HotelsId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomPrices_RoomTypes_RoomTypeId",
                table: "RoomPrices",
                column: "RoomTypeId",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypes_Hotels_HotelId",
                table: "RoomTypes",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelSpecials_Hotels_HotelsId",
                table: "HotelSpecials");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomPrices_RoomTypes_RoomTypeId",
                table: "RoomPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypes_Hotels_HotelId",
                table: "RoomTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomTypes",
                table: "RoomTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomPrices",
                table: "RoomPrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelSpecials",
                table: "HotelSpecials");

            migrationBuilder.RenameTable(
                name: "RoomTypes",
                newName: "RoomType");

            migrationBuilder.RenameTable(
                name: "RoomPrices",
                newName: "Price");

            migrationBuilder.RenameTable(
                name: "HotelSpecials",
                newName: "HotelSpecial");

            migrationBuilder.RenameIndex(
                name: "IX_RoomTypes_HotelId",
                table: "RoomType",
                newName: "IX_RoomType_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomPrices_RoomTypeId",
                table: "Price",
                newName: "IX_Price_RoomTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelSpecials_HotelsId",
                table: "HotelSpecial",
                newName: "IX_HotelSpecial_HotelsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomType",
                table: "RoomType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Price",
                table: "Price",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelSpecial",
                table: "HotelSpecial",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelSpecial_Hotels_HotelsId",
                table: "HotelSpecial",
                column: "HotelsId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Price_RoomType_RoomTypeId",
                table: "Price",
                column: "RoomTypeId",
                principalTable: "RoomType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomType_Hotels_HotelId",
                table: "RoomType",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
