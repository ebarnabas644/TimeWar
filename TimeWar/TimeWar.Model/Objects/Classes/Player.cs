// <copyright file="Player.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Player detail class.
    /// </summary>
    public class Player : Character
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="speed">Speed value.</param>
        /// <param name="health">Health value.</param>
        public Player(Point pos, int speed, int health)
            : base(pos, speed, health)
        {
        }
    }
}
