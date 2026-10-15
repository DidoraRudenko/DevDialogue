using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevDialogue.Migrations
{
    /// <inheritdoc />
    public partial class AddSimulationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimulationSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetRole = table.Column<string>(type: "text", nullable: false),
                    TargetLevel = table.Column<string>(type: "text", nullable: false),
                    TargetLanguage = table.Column<string>(type: "text", nullable: false),
                    TargetFramework = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationSessions_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimulationAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    UserAnswer = table.Column<string>(type: "text", nullable: true),
                    AiScore = table.Column<int>(type: "integer", nullable: true),
                    AiFeedback = table.Column<string>(type: "text", nullable: true),
                    IsAnswered = table.Column<bool>(type: "boolean", nullable: false),
                    AskedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AnsweredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulationAttempts_SimulationSessions_SimulationSessionId",
                        column: x => x.SimulationSessionId,
                        principalTable: "SimulationSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimulationAttempts_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimulationAttempts_QuestionId",
                table: "SimulationAttempts",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulationAttempts_SimulationSessionId_IsAnswered",
                table: "SimulationAttempts",
                columns: new[] { "SimulationSessionId", "IsAnswered" });

            migrationBuilder.CreateIndex(
                name: "IX_SimulationSessions_UserId",
                table: "SimulationSessions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulationAttempts");

            migrationBuilder.DropTable(
                name: "SimulationSessions");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
