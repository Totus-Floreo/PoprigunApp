using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PoprigunApp.ModelEF
{
    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<Agent> Agent { get; set; }
        public virtual DbSet<AgentType> AgentType { get; set; }
        public virtual DbSet<ProductSale> ProductSale { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agent>()
                .Property(e => e.INN)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .Property(e => e.KPP)
                .IsUnicode(false);

            modelBuilder.Entity<Agent>()
                .HasMany(e => e.ProductSale)
                .WithRequired(e => e.Agent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AgentType>()
                .HasMany(e => e.Agent)
                .WithRequired(e => e.AgentType)
                .WillCascadeOnDelete(false);
        }
    }
}
