// <copyright file="PlayerProfile.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Player profile class.
    /// </summary>
    public class PlayerProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerProfile"/> class.
        /// </summary>
        public PlayerProfile()
        {
            this.Records = new HashSet<MapRecord>();
        }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
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
        /// Gets or sets a value indicating whether this is the selected profile.
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets the autosave id.
        /// </summary>
        [ForeignKey("Save")]
        public int? SaveId { get; set; }

        /// <summary>
        /// Gets or sets the player auto save navigational property.
        /// </summary>
        public virtual Save Save { get; set; }

        /// <summary>
        /// Gets the records navigational property.
        /// </summary>
        public virtual ICollection<MapRecord> Records { get; }
    }
}
