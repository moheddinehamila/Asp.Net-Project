using Dari.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dari.Data
{
   public class DariContext : DbContext
    {
        public DariContext() : base("MaChaine") { }
        public DbSet<Verification> Verificaitions { get; set; }
        public DbSet<Counselor> Counselors { get; set; }

        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<AnnonceVente> AnnonceVentes { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<RendezVous> RendezVouss { get; set; }
    }
}
