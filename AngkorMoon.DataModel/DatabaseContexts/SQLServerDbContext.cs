using System;
using System.Data.Entity;
using AngkorMoon.DataModel.Models;
using System.Linq;

namespace AngkorMoon.DataModel.DatabaseContexts
{
    public class SQLServerDbContext : DbContext
    {
        public SQLServerDbContext()
            : base("SQLServerDatabase")
        {
        }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // for handling many to many relationship
            modelBuilder.Entity<Item>()
                .HasMany(p => p.Findings)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("ProductID");
                    m.MapRightKey("FindingID");
                    m.ToTable("item_finding");
                });

            modelBuilder.Types().Configure(c => c.Ignore("IsDirty"));

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // update the modified date and if newly created entity then update the creation date
            var newOrModifiedEntityHistory = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IModificationHistory && (e.State == EntityState.Added || e.State == EntityState.Modified))
                .Select(e => e.Entity as IModificationHistory);
            foreach(var history in newOrModifiedEntityHistory)
            {
                history.DateModified = DateTime.Now;
                if (history.DateCreated == DateTime.MinValue)
                {
                    history.DateCreated = DateTime.Now;
                }
            }

            int result = base.SaveChanges();

            // The update to IsDirty is soley for client tracking purposes
            foreach(var history in this.ChangeTracker.Entries()
                .Where(e => e is IModificationHistory)
                .Select(e => e as IModificationHistory))
            {
                history.IsDirty = false;
            }

            return result;
        }
    }
}
