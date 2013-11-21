using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiOrNot.EntityModels;

namespace TaxiOrNot.Data
{
    public class TaxiOrNotDbContext : DbContext
    {
        public IDbSet<City> Cities { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Taxi> Taxis { get; set; }

        public IDbSet<TaxiImage> TaxiImages { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Vote> Votes { get; set; }

        public IDbSet<VoteType> VoteTypes { get; set; }

        public TaxiOrNotDbContext() : base("TaxiOrNotDb")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                        .Property(c => c.Latitude)
                        .HasPrecision(18, 15);

            modelBuilder.Entity<Taxi>()
                        .Property(t => t.Description)
                        .HasColumnType("ntext");

            base.OnModelCreating(modelBuilder);
        }
    }
}