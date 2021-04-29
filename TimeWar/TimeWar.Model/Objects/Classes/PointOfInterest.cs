// <copyright file="PointOfInterest.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Types of POI.
    /// </summary>
    public enum POIType
    {
        /// <summary>
        /// Checkpoint.
        /// </summary>
        Checkpoint,

        /// <summary>
        /// Finish point.
        /// </summary>
        Finish,

        /// <summary>
        /// Adds health point to character.
        /// </summary>
        HealthKit,

        /// <summary>
        /// Character can jump higher than normal.
        /// </summary>
        HighJump,

        /// <summary>
        /// Unlocks accelerating bullet.
        /// </summary>
        UnlockWeapon,

        /// <summary>
        /// The character's health doesn't deplete.
        /// </summary>
        Invincibility,

        /// <summary>
        /// Player can spam bullets.
        /// </summary>
        RapidFire,
    }

    /// <summary>
    /// Class for checkpoints, finish point, powerups.
    /// </summary>
    public class PointOfInterest : IGameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointOfInterest"/> class.
        /// </summary>
        /// <param name="type">Type of a poi.</param>
        /// <param name="height">Height.</param>
        /// <param name="width">Width.</param>
        /// <param name="spritefile">Sprite file.</param>
        /// <param name="position">Position.</param>
        /// <param name="stanceless">Stanceless.</param>
        public PointOfInterest(POIType type, int height, int width, string spritefile, Point position, bool stanceless = true)
        {
            this.Type = type;
            this.Height = height;
            this.Width = width;
            this.SpriteFile = spritefile;
            this.Position = position;
            this.StanceLess = stanceless;
        }

        /// <summary>
        /// Gets or sets the type of a POI.
        /// </summary>
        public POIType Type { get; set; }

        /// <inheritdoc/>
        public int Height { get; set; }

        /// <inheritdoc/>
        public int Width { get; set; }

        /// <inheritdoc/>
        public string SpriteFile { get; set; }

        /// <inheritdoc/>
        public Stances Stance { get; set; }

        /// <inheritdoc/>
        public bool StanceLess { get; set; }

        /// <inheritdoc/>
        public Point MovementVector { get; set; }

        /// <inheritdoc/>
        public int CurrentSprite { get; set; }

        /// <inheritdoc/>
        public Point Position { get; set; }
    }
}