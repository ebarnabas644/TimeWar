// <copyright file="Save.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Game save entity class.
    /// </summary>
    public class Save
    {
        /// <summary>
        /// Gets or sets save id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets point.
        /// </summary>
        public string Playerdata { get; set; }

        /// <summary>
        /// Gets or sets checkpoint.
        /// </summary>
        public string Enemydata { get; set; }

        /// <summary>
        /// Gets or sets checkpoint.
        /// </summary>
        public string Poidata { get; set; }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets player navigational property.
        /// </summary>
        public virtual PlayerProfile Player { get; set; }
    }
}
