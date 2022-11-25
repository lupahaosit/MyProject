using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7
{
    public class DB : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<City> cities{ get; set; }
        
        public DbSet<Bankomat> Bankomats { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=MyProject;Trusted_Connection=True;");
        }
        
    }
}
