using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using TestBackgroundWorkerForm.Models;

namespace TestBackgroundWorkerForm.DataLayer
{
    public class StopTrackerDbContext : DbContext
    {
        public StopTrackerDbContext() : base("StopwatchDbContext")
        {
            Database.SetInitializer<StopTrackerDbContext>(null);            
        }

        public DbSet<StopwatchTracker> stopwatchTrackers { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StopWatchHistory());
        }

        internal class StopWatchHistory : EntityTypeConfiguration<StopwatchTracker>
        {
            public StopWatchHistory()
            {
                this.ToTable("StopwatchHistory");
                this.HasKey(k => k.Id);
            }
        }
    }
}
