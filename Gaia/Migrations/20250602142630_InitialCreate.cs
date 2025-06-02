using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gaia.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    IdLocation = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    City = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    StartAccident = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    EndAccident = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Station = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Active = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.IdLocation);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Name = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Active = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TypeUser = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Accident",
                columns: table => new
                {
                    IdAccident = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DateAccidentStart = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DateAccidentEnd = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TypeSeverity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TypeAccident = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LocationId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accident", x => x.IdAccident);
                    table.ForeignKey(
                        name: "FK_Accident_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "IdLocation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requestion",
                columns: table => new
                {
                    IdRequestion = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Title = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(1000)", maxLength: 1000, nullable: false),
                    Unit = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Active = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UserId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    UserIdUser = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    LocationId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    LocationIdLocation = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requestion", x => x.IdRequestion);
                    table.ForeignKey(
                        name: "FK_Requestion_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "IdLocation",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requestion_Location_LocationIdLocation",
                        column: x => x.LocationIdLocation,
                        principalTable: "Location",
                        principalColumn: "IdLocation");
                    table.ForeignKey(
                        name: "FK_Requestion_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requestion_USERS_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "USERS",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accident_LocationId",
                table: "Accident",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Requestion_LocationId",
                table: "Requestion",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Requestion_LocationIdLocation",
                table: "Requestion",
                column: "LocationIdLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Requestion_UserId",
                table: "Requestion",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requestion_UserIdUser",
                table: "Requestion",
                column: "UserIdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accident");

            migrationBuilder.DropTable(
                name: "Requestion");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
