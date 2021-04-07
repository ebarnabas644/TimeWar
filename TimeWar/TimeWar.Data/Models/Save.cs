// <copyright file="Save.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Game save entity class.
    /// </summary>
    public class Save
    {
        /// <summary>
        /// Gets or sets save id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets point.
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// Gets or sets checkpoint.
        /// </summary>
        public int Checkpoint { get; set; }

        /// <summary>
        /// Gets or sets player navigational property.
        /// </summary>
        [ForeignKey("AutoSave")]
        public virtual Profile Player { get; set; }
    }
}
