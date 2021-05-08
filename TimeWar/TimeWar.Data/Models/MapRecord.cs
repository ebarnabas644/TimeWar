// <copyright file="MapRecord.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Map record entity class.
    /// </summary>
    public class MapRecord
    {
        /// <summary>
        /// Gets or sets map id.
        /// </summary>
        public int MapRecordId { get; set; }

        /// <summary>
        /// Gets or sets map name.
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        /// Gets or sets player id.
        /// </summary>
        public int? PlayerId { get; set; }

        /// <summary>
        /// Gets or sets player profile navigational property.
        /// </summary>
        public virtual PlayerProfile Player { get; set; }

        /// <summary>
        /// Gets or sets run time.
        /// </summary>
        public TimeSpan RunTime { get; set; }
    }
}
