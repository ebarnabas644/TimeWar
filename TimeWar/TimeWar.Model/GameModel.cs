// <copyright file="GameModel.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model
{
    using System.Drawing;
    using TimeWar.Model.Objects;

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
            this.LevelFinished = false;
            this.InRewind = false;
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

        /// <summary>
        /// Gets or sets mouse location.
        /// </summary>
        public Point MouseLocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether level finished.
        /// </summary>
        public bool LevelFinished { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether in rewind.
        /// </summary>
        public bool InRewind { get; set; }
    }
}
