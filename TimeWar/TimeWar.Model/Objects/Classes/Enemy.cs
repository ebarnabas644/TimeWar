// <copyright file="Enemy.cs" company="Time War">
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

    /// <summary>
    /// Enemy character class.
    /// </summary>
    public class Enemy : Character
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <param name="health">Health value.</param>
        /// <param name="height">Height.</param>
        /// <param name="width">Width.</param>
        /// <param name="spriteFile">Spritesheet file name.</param>
        public Enemy(Point pos, int health, int height, int width, string spriteFile)
            : base(pos, health, height, width, spriteFile)
        {
        }
    }
}
