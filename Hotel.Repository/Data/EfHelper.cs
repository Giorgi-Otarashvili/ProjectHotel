using Hotel.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Data
{
    public static class EfHelper
    {
      
            public static void ConfigureHotel(this ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<hotel>(entity =>
                {
                    entity.HasOne(h => h.Manager)
                          .WithOne(m => m.Hotel)
                          .HasForeignKey<ApplicationUser>(m => m.HotelId)
                          .OnDelete(DeleteBehavior.Restrict);

                    entity.HasMany(h => h.Rooms)
                          .WithOne(r => r.Hotel)
                          .HasForeignKey(r => r.HotelId)
                          .OnDelete(DeleteBehavior.Cascade);
                });
            }

            public static void ConfigureManager(this ModelBuilder modelBuilder)
            {
            }

            public static void ConfigureGuest(this ModelBuilder modelBuilder)
            {

            }

            public static void ConfigureReservation(this ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Reservation>(entity =>
                {
                    entity.HasOne(res => res.Room)
                          .WithMany()
                          .HasForeignKey(res => res.RoomId)
                          .OnDelete(DeleteBehavior.Restrict);

                    entity.HasOne(res => res.Guest)
                          .WithMany(g => g.Reservations)
                          .HasForeignKey(res => res.GuestId)
                          .OnDelete(DeleteBehavior.Restrict);
                });
            }
        

     
            public static void SeedData(this ModelBuilder modelBuilder)
            {
                //modelBuilder.Entity<hotel>().HasData(
                //    new hotel { Id = 1, Name = "Grand Palace", Rating = 5, Country = "Georgia", City = "Tbilisi", Address = "Rustaveli Ave 12" },
                //    new hotel { Id = 2, Name = "Sea View Resort", Rating = 4, Country = "Georgia", City = "Batumi", Address = "Black Sea St 7" }
                //);

                //modelBuilder.Entity<Manager>().HasData(
                //    new Manager { Id = 1, FirstName = "Giorgi", LastName = "Beridze", PersonalId = "12345678901", Email = "giorgi@example.com", PhoneNumber = "+995555123456", HotelId = 1 },
                //    new Manager { Id = 2, FirstName = "Nino", LastName = "Mchedlidze", PersonalId = "98765432109", Email = "nino@example.com", PhoneNumber = "+995599654321", HotelId = 2 }
                //);

                //modelBuilder.Entity<Guest>().HasData(
                //    new Guest { Id = 1, FirstName = "Luka", LastName = "Giorgadze", PersonalId = "11122233344", PhoneNumber = "+995577123456" },
                //    new Guest { Id = 2, FirstName = "Ana", LastName = "Kiknavelidze", PersonalId = "55566677788", PhoneNumber = "+995599876543" }
                //);

                //modelBuilder.Entity<Room>().HasData(
                //    new Room { Id = 1, Name = "Deluxe Room", IsAvailable = true, Price = 150, HotelId = 1 },
                //    new Room { Id = 2, Name = "Standard Room", IsAvailable = true, Price = 80, HotelId = 1 },
                //    new Room { Id = 3, Name = "Luxury Suite", IsAvailable = false, Price = 200, HotelId = 2 }
                //);

                //modelBuilder.Entity<Reservation>().HasData(
                //    new Reservation { Id = 1, CheckIn = new DateTime(2025, 4, 10), CheckOut = new DateTime(2025, 4, 15), RoomId = 1, GuestId = 1 },
                //    new Reservation { Id = 2, CheckIn = new DateTime(2025, 5, 1), CheckOut = new DateTime(2025, 5, 5), RoomId = 3, GuestId = 2 }
                //);
            }
        }
    }
