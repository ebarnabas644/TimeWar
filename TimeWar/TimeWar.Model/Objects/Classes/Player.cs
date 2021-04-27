// <copyright file="Player.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects
{
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
        }
    }
}
