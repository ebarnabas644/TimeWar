// <copyright file="TimeWarContext.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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
        public virtual DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Gets or sets maps table.
        /// </summary>
        public virtual DbSet<Map> Maps { get; set; }

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
    }
}
