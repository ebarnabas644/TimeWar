// <copyright file="Player.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Player detail class.
    /// </summary>
    public class Player : Character
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="health">Health value.</param>
        /// <param name="height">Height.</param>
        /// <param name="width">Width.</param>
        /// <param name="spriteFile">Spritesheet file name.</param>
        public Player(Point pos, int health, int height, int width, string spriteFile)
            : base(pos, health, height, width, spriteFile)
        {
            this.Stance = Interfaces.Stances.StandRight;
            this.Shield = 100;
            this.CurrentShield = 100;
            this.NumOfWeaponUnlocked = 1;
            this.Checkpoint = this.Position;
        }

        /// <summary>
        /// Gets or sets player kills.
        /// </summary>
        public int Kills { get; set; }

        /// <summary>
        /// Gets or sets player deaths.
        /// </summary>
        public int Deaths { get; set; }

        /// <summary>
        /// Gets or sets checkpoint.
        /// </summary>
        public Point Checkpoint { get; set; }

        /// <summary>
        /// Gets or sets number of unlocked weapons.
        /// </summary>
        public int NumOfWeaponUnlocked { get; set; }

        /// <summary>
        /// Method is called when the player is dead.
        /// </summary>
        public void PlayerDeath()
        {
            this.Position = this.Checkpoint;
            this.MovementVector = new Point(0, 0);
        }
    }
}
