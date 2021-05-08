// <copyright file="TimeWarContext.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Data.Models
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// TimeWar database context class.
    /// </summary>
    public class TimeWarContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeWarContext"/> class.
        /// </summary>
        public TimeWarContext()
        {
            this.Database.EnsureCreated();
        }

        /// <summary>
        /// Gets or sets profiles table.
        /// </summary>
        public virtual DbSet<PlayerProfile> Profiles { get; set; }

        /// <summary>
        /// Gets or sets maps table.
        /// </summary>
        public virtual DbSet<MapRecord> MapRecords { get; set; }

        /// <summary>
        /// Gets or sets saves table.
        /// </summary>
        public virtual DbSet<Save> Saves { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder != null)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseLazyLoadingProxies().UseSqlServer("data source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=|DataDirectory|\\TimeWarDatabase.mdf;integrated security=True;MultipleActiveResultSets=True");
                }
            }
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Entity<MapRecord>(entity =>
                {
                    entity.HasKey(x => x.MapRecordId);
                    entity.HasOne(x => x.Player)
                    .WithMany(x => x.Records)
                    .HasForeignKey(x => x.PlayerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired(true)
                    .HasConstraintName("player_fk");
                });

                modelBuilder.Entity<Save>(entity =>
                {
                    entity.HasOne(x => x.Player)
                    .WithOne(x => x.Save).HasForeignKey<PlayerProfile>(x => x.SaveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("save_fk");
                });

                modelBuilder.Entity<PlayerProfile>(entity =>
                {
                    entity.HasKey(x => x.PlayerId);
                    entity.HasMany(x => x.Records)
                    .WithOne(x => x.Player)
                    .HasForeignKey(x => x.PlayerId)
                    .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(x => x.Save)
                    .WithOne(x => x.Player)
                    .HasForeignKey<Save>(x => x.PlayerId);
                });
            }
        }
    }
}
