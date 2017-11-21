using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Web_db.Models;

namespace Project_Web_db.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

       

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

    
        }


        public DbSet<User> Users { get; set; }
        
        public DbSet<Personnel> Personnels { get; set; }

        public DbSet<Ban> Bans { get; set; }

        public DbSet<Bus_schedule> Bus_schedules { get; set; }

        public DbSet<Personnel_Add_User> Personnel_Add_Users { get; set; }

        public DbSet<Reservations> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {   
            builder.Entity<User>()
                        .HasIndex(x => x.email).IsUnique();

            builder.Entity<Personnel>()
                       .HasIndex(x => x.email).IsUnique();

            base.OnModelCreating(builder);

        }

       
    }
}
