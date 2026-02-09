using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _2026_RoomHub_backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoomData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "Location", "RoomCode", "RoomName" },
                values: new object[,]
                {
                    { 1, 40, "Gedung A Lantai 1", "LK-101", "Lab Komputer 1" },
                    { 2, 40, "Gedung A Lantai 1", "LK-102", "Lab Komputer 2" },
                    { 3, 35, "Gedung A Lantai 2", "LK-201", "Lab Komputer 3" },
                    { 4, 50, "Gedung B Lantai 1", "RK-101", "Ruang Kelas 101" },
                    { 5, 50, "Gedung B Lantai 1", "RK-102", "Ruang Kelas 102" },
                    { 6, 45, "Gedung B Lantai 1", "RK-103", "Ruang Kelas 103" },
                    { 7, 50, "Gedung B Lantai 2", "RK-201", "Ruang Kelas 201" },
                    { 8, 50, "Gedung B Lantai 2", "RK-202", "Ruang Kelas 202" },
                    { 9, 45, "Gedung B Lantai 2", "RK-203", "Ruang Kelas 203" },
                    { 10, 40, "Gedung B Lantai 3", "RK-301", "Ruang Kelas 301" },
                    { 11, 20, "Gedung C Lantai 1", "RM-101", "Ruang Meeting 1" },
                    { 12, 15, "Gedung C Lantai 1", "RM-102", "Ruang Meeting 2" },
                    { 13, 25, "Gedung C Lantai 2", "RM-201", "Ruang Meeting 3" },
                    { 14, 300, "Gedung D", "AUD-01", "Auditorium Utama" },
                    { 15, 150, "Gedung D", "AUD-02", "Auditorium Mini" },
                    { 16, 30, "Gedung E Lantai 1", "LB-101", "Lab Bahasa" },
                    { 17, 30, "Gedung F Lantai 1", "LF-101", "Lab Fisika 1" },
                    { 18, 30, "Gedung F Lantai 1", "LF-102", "Lab Fisika 2" },
                    { 19, 25, "Gedung F Lantai 2", "LK-103", "Lab Kimia" },
                    { 20, 20, "Gedung G Lantai 1", "ST-101", "Studio Multimedia" },
                    { 21, 15, "Gedung G Lantai 1", "ST-102", "Studio Recording" },
                    { 22, 48, "Gedung B Lantai 1", "RK-104", "Ruang Kelas 104" },
                    { 23, 48, "Gedung B Lantai 2", "RK-204", "Ruang Kelas 204" },
                    { 24, 40, "Gedung B Lantai 3", "RK-302", "Ruang Kelas 302" },
                    { 25, 35, "Gedung A Lantai 2", "LP-101", "Lab Pemrograman" },
                    { 26, 30, "Gedung A Lantai 3", "LJ-101", "Lab Jaringan" },
                    { 27, 100, "Gedung H Lantai 1", "RS-101", "Ruang Seminar 1" },
                    { 28, 80, "Gedung H Lantai 2", "RS-102", "Ruang Seminar 2" },
                    { 29, 12, "Gedung C Lantai 2", "RM-103", "Ruang Meeting 4" },
                    { 30, 25, "Gedung G Lantai 2", "LD-101", "Lab Desain Grafis" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 30);
        }
    }
}
