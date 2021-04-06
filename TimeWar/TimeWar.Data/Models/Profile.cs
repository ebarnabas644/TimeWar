// <copyright file="Profile.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Player profile class.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Profile"/> class.
        /// </summary>
        public Profile()
        {
            this.Records = new HashSet<Map>();
        }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [Key]
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Gets or sets the total number of kills.
        /// </summary>
        public int TotalKills { get; set; }

        /// <summary>
        /// Gets or sets the total number of deaths.
        /// </summary>
        public int TotalDeaths { get; set; }

        /// <summary>
        /// Gets or sets the number of completed runs.
        /// </summary>
        public int CompletedRuns { get; set; }

        /// <summary>
        /// Gets or sets the player auto save navigational property.
        /// </summary>
        public virtual Save AutoSave { get; set; }

        /// <summary>
        /// Gets the records navigational property.
        /// </summary>
        public virtual ICollection<Map> Records { get; }
    }
}
