using Microsoft.EntityFrameworkCore;
using System;
using FilmStudio.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FilmStudio.Entities
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts):base(opts)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<RentalRecord> RentalRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Films
            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 1,
                    FilmName = "Fast & Furious",
                    ReleaseYear = 2001,
                    Country = "US",
                    Director = "Vin Diesel",
                    TotalNumberOfCopies = 4,
                    NumberOfRentedCopies = 1,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/fast_furious.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 2,
                    FilmName = "Taxi Driver",
                    ReleaseYear = 1976,
                    Country = "US",
                    Director = "Martin Scorsese",
                    TotalNumberOfCopies = 3,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/taxi_driver.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 3,
                    FilmName = "Marriage Story",
                    ReleaseYear = 2019,
                    Country = "US",
                    Director = "Noah Baumbach",
                    TotalNumberOfCopies = 10,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/marriage_story.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 4,
                    FilmName = "Black Widow",
                    ReleaseYear = 2021,
                    Country = "US",
                    Director = "Cate Shortland",
                    TotalNumberOfCopies = 10,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/black_widow.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 5,
                    FilmName = "Falling",
                    ReleaseYear = 2020,
                    Country = "US",
                    Director = "Viggo Mortenson",
                    TotalNumberOfCopies = 15,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/falling.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 6,
                    FilmName = "Arctic Dogs",
                    ReleaseYear = 2019,
                    Country = "US, Canada, India, Italy, Russia",
                    Director = "Aaron Woodley",
                    TotalNumberOfCopies = 20,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/arctic_dogs.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 7,
                    FilmName = "Mank",
                    ReleaseYear = 2020,
                    Country = "US",
                    Director = "David Fincher",
                    TotalNumberOfCopies = 14,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/mank.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 8,
                    FilmName = "The Breadwinner",
                    ReleaseYear = 2017,
                    Country = "Canada, Irland, Luxamburg",
                    Director = "Nora Twomey",
                    TotalNumberOfCopies = 20,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/bread_winner.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 9,
                    FilmName = "Helmut Newton: The Bad and the Beautiful",
                    ReleaseYear = 2020,
                    Country = "Germany",
                    Director = "Gero von Bohem",
                    TotalNumberOfCopies = 20,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/helmut_newton.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 10,
                    FilmName = "I am Greta",
                    ReleaseYear = 2020,
                    Country = "Sweden, US, UK, Germany",
                    Director = "Nathan Grossman",
                    TotalNumberOfCopies = 15,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/i_am_greta.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 11,
                    FilmName = "Min Pappa Marianne",
                    ReleaseYear = 2020,
                    Country = "Sweden",
                    Director = "Mårten Klingberg",
                    TotalNumberOfCopies = 10,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/min_pappa_marianne.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 12,
                    FilmName = "Wolfwalkers",
                    ReleaseYear = 2020,
                    Country = "US",
                    Director = "Tomm Moore, Ross Stewart",
                    TotalNumberOfCopies = 20,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/wolfwalkers.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 13,
                    FilmName = "Come play",
                    ReleaseYear = 2020,
                    Country = "US",
                    Director = "Jacob Chase",
                    TotalNumberOfCopies = 20,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/come_play.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 14,
                    FilmName = "Rocks",
                    ReleaseYear = 2019,
                    Country = "UK",
                    Director = "Sarah Gavron",
                    TotalNumberOfCopies = 20,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/rocks.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 15,
                    FilmName = "Shirley",
                    ReleaseYear = 2020,
                    Country = "US",
                    Director = "Joesephine Decker",
                    TotalNumberOfCopies = 20,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/shirley.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 16,
                    FilmName = "Honest Thief",
                    ReleaseYear = 2020,
                    Country = "US",
                    Director = "Mark Williams",
                    TotalNumberOfCopies = 20,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/honest_thief.png"
                });

            modelBuilder.Entity<Film>().HasData(
                new Film
                {
                    FilmId = 17,
                    FilmName = "Chapter",
                    ReleaseYear = 2020,
                    Country = "Sweden",
                    Director = "Amanda Kernell",
                    TotalNumberOfCopies = 20,
                    NumberOfRentedCopies = 0,
                    ImageUrl = "https://raw.githubusercontent.com/masoom-gh/FilmStudion_images/main/chapter.png"
                });

            //Rental Records
            modelBuilder.Entity<RentalRecord>().HasData(
                new
                {
                    OrderId = 1,
                    RentalDate = new DateTime(2021,01,29),
                    FilmStudioId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
                    FilmId = 1,
                });

           

            //Film Studio
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "user",
                NormalizedName = "USER",
                Id = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
                ConcurrencyStamp = "341743f0-asd2–42de-afbf-59kmkkmk72cf6"
            });

            var filmStudio = new ApplicationUser
            {
                Id = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
                StudioName = "studio 1",
                City = "Stockholm",
                ChairmanName = "John Smith",
                Email = "smith@studio1.com",
                NormalizedEmail = "SMITH@STUDIO1.COM",
                ChairmanMobileNumber = "073-6767453",
                EmailConfirmed = false,
                UserName = "smith@studio1.com",
                NormalizedUserName = "SMITH@STUDIO1.COM",
                LockoutEnabled = true,
            };

            // set password for filmStudio
            PasswordHasher<ApplicationUser> password = new PasswordHasher<ApplicationUser>();
            filmStudio.PasswordHash = password.HashPassword(filmStudio, "Test2021!");

            //seed filmstudio
            modelBuilder.Entity<ApplicationUser>().HasData(filmStudio);

            //set USER role to filmStudio
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6",
                UserId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6"
            });
        }
    }
}

