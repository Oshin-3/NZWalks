using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforRegionandDifficulty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01b66037-49ed-4425-91ed-8ba53a1c9960"), "Medium" },
                    { new Guid("4cee7041-0184-4cf6-89c3-cb9807d81dff"), "Easy" },
                    { new Guid("810aeabe-33fe-4cc6-8a90-6fe81240b8a2"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("32940b4e-3e60-448b-8d6f-177ce640542d"), "AKL", "Auckland", "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3e/Auckland_skyline_from_Mt_Eden.jpg/2560px-Auckland_skyline_from_Mt_Eden.jpg" },
                    { new Guid("4c4b19d3-91ea-4994-94f4-abbda20a95d7"), "WAI", "Waikato", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Waikato_River.jpg/2560px-Waikato_River.jpg" },
                    { new Guid("63273344-59d7-46b4-a85b-f9fbfbd0c78e"), "OTA", "Otago", null },
                    { new Guid("8d7790f4-321a-4676-9445-d5145ac31f90"), "CAN", "Canterbury", null },
                    { new Guid("9939da5a-f9d2-4400-a7cb-0f222c6f46ac"), "TAR", "Taranaki", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/Mt_Taranaki.jpg/2560px-Mt_Taranaki.jpg" },
                    { new Guid("dd7ace6f-18ec-4b07-9248-1847786c04a2"), "BOP", "Bay of Plenty", null },
                    { new Guid("fd00093f-3493-445f-bd87-fb9b2ff1297b"), "WLG", "Wellington", "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5e/Wellington_City.jpg/2560px-Wellington_City.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("01b66037-49ed-4425-91ed-8ba53a1c9960"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("4cee7041-0184-4cf6-89c3-cb9807d81dff"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("810aeabe-33fe-4cc6-8a90-6fe81240b8a2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("32940b4e-3e60-448b-8d6f-177ce640542d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4c4b19d3-91ea-4994-94f4-abbda20a95d7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("63273344-59d7-46b4-a85b-f9fbfbd0c78e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8d7790f4-321a-4676-9445-d5145ac31f90"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9939da5a-f9d2-4400-a7cb-0f222c6f46ac"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("dd7ace6f-18ec-4b07-9248-1847786c04a2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("fd00093f-3493-445f-bd87-fb9b2ff1297b"));
        }
    }
}
