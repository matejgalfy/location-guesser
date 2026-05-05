using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class GameDbContext : DbContext
    {
        public DbSet<ImageList> ImageLists { get; set; }
        public DbSet<ImageLocation> ImageLocations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserListScore> UserListScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=game.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageList>().HasData(
                new ImageList
                {
                    Id = 1,
                    Name = "Slovak cities"
                }
            );

            modelBuilder.Entity<ImageLocation>().HasData(
                new ImageLocation
                {
                    Id = 1,
                    ImageListId = 1,
                    Name = "Bratislava",
                    ImageSource = "https://www.visitaustria.info/en/wp-content/uploads/sites/171/bratislava-hd.jpg",
                    Latitude = 48.148598,
                    Longitude = 17.107748
                },
                new ImageLocation
                {
                    Id = 2,
                    ImageListId = 1,
                    Name = "Banská Bystrica",
                    ImageSource = "https://www.slovaklines.sk/wp-content/uploads/2023/10/banska-bystrica.full_.jpg",
                    Latitude = 48.738611,
                    Longitude = 19.156944
                },
                new ImageLocation
                {
                    Id = 3,
                    ImageListId = 1,
                    Name = "Trebišov",
                    ImageSource = "https://www.tourismato.cz/foto/bazilika-327831.jpg",
                    Latitude = 48.633333,
                    Longitude = 21.716667
                }
            );
        }


    }
}
