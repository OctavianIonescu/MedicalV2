using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestSmth2.Migrations.MedicalDb
{
    /// <inheritdoc />
    public partial class InitialMedicalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AntiBiotics",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsResistant = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntiBiotics", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientCNP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientCNP);
                });

            migrationBuilder.CreateTable(
                name: "ResistanceMechanisms",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResistanceMechanisms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    entryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientCNP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PathologicProduct = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Microbe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sectiaDeProvenienta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    medicSolicitant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URLHandle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    collectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    validationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Entries_Patients_PatientCNP",
                        column: x => x.PatientCNP,
                        principalTable: "Patients",
                        principalColumn: "PatientCNP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AntiBioticEntry",
                columns: table => new
                {
                    EntriesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AntiBioticEntry", x => new { x.EntriesID, x.TagsID });
                    table.ForeignKey(
                        name: "FK_AntiBioticEntry_AntiBiotics_TagsID",
                        column: x => x.TagsID,
                        principalTable: "AntiBiotics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AntiBioticEntry_Entries_EntriesID",
                        column: x => x.EntriesID,
                        principalTable: "Entries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryResistanceMechanism",
                columns: table => new
                {
                    EntriesID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResistanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryResistanceMechanism", x => new { x.EntriesID, x.ResistanceID });
                    table.ForeignKey(
                        name: "FK_EntryResistanceMechanism_Entries_EntriesID",
                        column: x => x.EntriesID,
                        principalTable: "Entries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryResistanceMechanism_ResistanceMechanisms_ResistanceID",
                        column: x => x.ResistanceID,
                        principalTable: "ResistanceMechanisms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AntiBioticEntry_TagsID",
                table: "AntiBioticEntry",
                column: "TagsID");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_PatientCNP",
                table: "Entries",
                column: "PatientCNP");

            migrationBuilder.CreateIndex(
                name: "IX_EntryResistanceMechanism_ResistanceID",
                table: "EntryResistanceMechanism",
                column: "ResistanceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AntiBioticEntry");

            migrationBuilder.DropTable(
                name: "EntryResistanceMechanism");

            migrationBuilder.DropTable(
                name: "AntiBiotics");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "ResistanceMechanisms");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
