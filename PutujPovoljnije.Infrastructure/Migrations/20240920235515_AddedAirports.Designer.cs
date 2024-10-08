﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PutujPovoljnije.Infrastructure.Data;

#nullable disable

namespace PutujPovoljnije.Infrastructure.Migrations
{
    [DbContext(typeof(FlightSearchDbContext))]
    [Migration("20240920235515_AddedAirports")]
    partial class AddedAirports
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.Airport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IATA")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.Departure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("At")
                        .HasColumnType("TEXT");

                    b.Property<string>("IataCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Terminal")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Departures");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.FlightOffer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("FlightSearchId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("InstantTicketingRequired")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsUpsellOffer")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastTicketingDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastTicketingDateTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("NonHomogeneous")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfBookableSeats")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("OneWay")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("PriceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ValidatingAirlineCodes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FlightSearchId");

                    b.HasIndex("PriceId");

                    b.ToTable("FlightOffers");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.FlightSearch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Adults")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Children")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DepartureAirport")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DepartureDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DestinationAirport")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ExcludedAirlineCodes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("IncludedAirlineCodes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Infants")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxOffers")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxPrice")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("NonStop")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReturnDate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SearchString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TravelClass")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FlightSearches");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.Itinerary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FlightOfferId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FlightOfferId");

                    b.ToTable("Itineraries");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.Price", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Base")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("GrandTotal")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Total")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.Segment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArrivalId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("BlacklistedInEU")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarrierCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DepartureId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ItineraryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumberOfStops")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ArrivalId");

                    b.HasIndex("DepartureId");

                    b.HasIndex("ItineraryId");

                    b.ToTable("Segments");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.FlightOffer", b =>
                {
                    b.HasOne("PutujPovoljnije.Domain.Models.FlightSearch", "FlightSearch")
                        .WithMany("FlightOffers")
                        .HasForeignKey("FlightSearchId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PutujPovoljnije.Domain.Models.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FlightSearch");

                    b.Navigation("Price");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.Itinerary", b =>
                {
                    b.HasOne("PutujPovoljnije.Domain.Models.FlightOffer", null)
                        .WithMany("Itineraries")
                        .HasForeignKey("FlightOfferId");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.Segment", b =>
                {
                    b.HasOne("PutujPovoljnije.Domain.Models.Departure", "Arrival")
                        .WithMany()
                        .HasForeignKey("ArrivalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PutujPovoljnije.Domain.Models.Departure", "Departure")
                        .WithMany()
                        .HasForeignKey("DepartureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PutujPovoljnije.Domain.Models.Itinerary", null)
                        .WithMany("Segments")
                        .HasForeignKey("ItineraryId");

                    b.Navigation("Arrival");

                    b.Navigation("Departure");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.FlightOffer", b =>
                {
                    b.Navigation("Itineraries");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.FlightSearch", b =>
                {
                    b.Navigation("FlightOffers");
                });

            modelBuilder.Entity("PutujPovoljnije.Domain.Models.Itinerary", b =>
                {
                    b.Navigation("Segments");
                });
#pragma warning restore 612, 618
        }
    }
}
