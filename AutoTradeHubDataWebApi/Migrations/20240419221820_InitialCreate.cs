using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AutoTradeHubDataWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "marks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MarkaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_models_marks_MarkaId",
                        column: x => x.MarkaId,
                        principalTable: "marks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "generations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ModelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_generations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_generations_models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MarkaId = table.Column<int>(type: "integer", nullable: false),
                    ModelId = table.Column<int>(type: "integer", nullable: false),
                    GenerationId = table.Column<int>(type: "integer", nullable: false),
                    ColorId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    EngineVolume = table.Column<float>(type: "real", nullable: false),
                    EnginePower = table.Column<int>(type: "integer", nullable: false),
                    SteeringWheel = table.Column<bool>(type: "boolean", nullable: false),
                    Gearbox = table.Column<byte>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    EngineType = table.Column<byte>(type: "smallint", nullable: false),
                    Privod = table.Column<byte>(type: "smallint", nullable: false),
                    BodyType = table.Column<byte>(type: "smallint", nullable: false),
                    Probeg = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cars_colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cars_generations_GenerationId",
                        column: x => x.GenerationId,
                        principalTable: "generations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cars_marks_MarkaId",
                        column: x => x.MarkaId,
                        principalTable: "marks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cars_models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_ColorId",
                table: "cars",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_GenerationId",
                table: "cars",
                column: "GenerationId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_MarkaId",
                table: "cars",
                column: "MarkaId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_ModelId",
                table: "cars",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_generations_ModelId",
                table: "generations",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_models_MarkaId",
                table: "models",
                column: "MarkaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "colors");

            migrationBuilder.DropTable(
                name: "generations");

            migrationBuilder.DropTable(
                name: "models");

            migrationBuilder.DropTable(
                name: "marks");
        }
    }
}
