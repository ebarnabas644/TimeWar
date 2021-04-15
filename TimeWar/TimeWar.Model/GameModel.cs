// <copyright file="GameModel.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Main game model class.
    /// </summary>
    public class GameModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// </summary>
        public GameModel()
        {
        }

        /// <summary>
        /// Gets or sets the current world data property.
        /// </summary>
        public GameWorld CurrentWorld { get; set; }

        /// <summary>
        /// Gets or sets the hero character.
        /// </summary>
        public Player Hero { get; set; }

        /// <summary>
        /// Gets or sets the camera.
        /// </summary>
        public Viewport Camera { get; set; }
    }
}
