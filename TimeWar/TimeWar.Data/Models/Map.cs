// <copyright file="Map.cs" company="Time War">
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
    /// Map entity class.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Gets or sets map id.
        /// </summary>
        [Key]
        public int MapId { get; set; }

        /// <summary>
        /// Gets or sets player profile navigational property.
        /// </summary>
        [Key]
        public virtual PlayerProfile Player { get; set; }

        /// <summary>
        /// Gets or sets run time.
        /// </summary>
        public int RunTime { get; set; }
    }
}
