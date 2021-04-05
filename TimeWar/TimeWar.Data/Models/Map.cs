// <copyright file="Map.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Map entity class.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Gets or sets record id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets map id.
        /// </summary>
        public int MapId { get; set; }

        /// <summary>
        /// Gets or sets player profile.
        /// </summary>
        public Profile Player { get; set; }

        /// <summary>
        /// Gets or sets run time.
        /// </summary>
        public int Time { get; set; }
    }
}
