using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PutujPovoljnije.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IataCode = table.Column<string>(type: "TEXT", nullable: false),
                    Terminal = table.Column<string>(type: "TEXT", nullable: false),
                    At = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightSearches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SearchString = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureAirport = table.Column<string>(type: "TEXT", nullable: false),
                    DestinationAirport = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureDate = table.Column<string>(type: "TEXT", nullable: false),
                    ReturnDate = table.Column<string>(type: "TEXT", nullable: false),
                    Adults = table.Column<int>(type: "INTEGER", nullable: false),
                    Children = table.Column<int>(type: "INTEGER", nullable: false),
                    Infants = table.Column<int>(type: "INTEGER", nullable: false),
                    TravelClass = table.Column<string>(type: "TEXT", nullable: false),
                    IncludedAirlineCodes = table.Column<string>(type: "TEXT", nullable: false),
                    ExcludedAirlineCodes = table.Column<string>(type: "TEXT", nullable: false),
                    NonStop = table.Column<bool>(type: "INTEGER", nullable: true),
                    Currency = table.Column<string>(type: "TEXT", nullable: false),
                    MaxPrice = table.Column<int>(type: "INTEGER", nullable: true),
                    MaxOffers = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSearches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: false),
                    Total = table.Column<string>(type: "TEXT", nullable: false),
                    Base = table.Column<string>(type: "TEXT", nullable: false),
                    GrandTotal = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlightOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Source = table.Column<string>(type: "TEXT", nullable: false),
                    InstantTicketingRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    NonHomogeneous = table.Column<bool>(type: "INTEGER", nullable: false),
                    OneWay = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsUpsellOffer = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastTicketingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastTicketingDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NumberOfBookableSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    PriceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ValidatingAirlineCodes = table.Column<string>(type: "TEXT", nullable: false),
                    FlightSearchId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightOffers_FlightSearches_FlightSearchId",
                        column: x => x.FlightSearchId,
                        principalTable: "FlightSearches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FlightOffers_Prices_PriceId",
                        column: x => x.PriceId,
                        principalTable: "Prices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Itineraries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Duration = table.Column<string>(type: "TEXT", nullable: false),
                    FlightOfferId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itineraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Itineraries_FlightOffers_FlightOfferId",
                        column: x => x.FlightOfferId,
                        principalTable: "FlightOffers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ArrivalId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DepartureId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CarrierCode = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Duration = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOfStops = table.Column<int>(type: "INTEGER", nullable: false),
                    BlacklistedInEU = table.Column<bool>(type: "INTEGER", nullable: false),
                    ItineraryId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segments_Departures_ArrivalId",
                        column: x => x.ArrivalId,
                        principalTable: "Departures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Segments_Departures_DepartureId",
                        column: x => x.DepartureId,
                        principalTable: "Departures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Segments_Itineraries_ItineraryId",
                        column: x => x.ItineraryId,
                        principalTable: "Itineraries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightOffers_FlightSearchId",
                table: "FlightOffers",
                column: "FlightSearchId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightOffers_PriceId",
                table: "FlightOffers",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Itineraries_FlightOfferId",
                table: "Itineraries",
                column: "FlightOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_ArrivalId",
                table: "Segments",
                column: "ArrivalId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_DepartureId",
                table: "Segments",
                column: "DepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_ItineraryId",
                table: "Segments",
                column: "ItineraryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropTable(
                name: "Departures");

            migrationBuilder.DropTable(
                name: "Itineraries");

            migrationBuilder.DropTable(
                name: "FlightOffers");

            migrationBuilder.DropTable(
                name: "FlightSearches");

            migrationBuilder.DropTable(
                name: "Prices");
        }
    }
}
