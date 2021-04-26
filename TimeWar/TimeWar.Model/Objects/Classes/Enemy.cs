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
    /// Types of enemies.
    /// </summary>
    public enum EnemyType
    {
        /// <summary>
        /// Basic enemy type.
        /// </summary>
        Basic,

        /// <summary>
        /// Fast enemy type.
        /// </summary>
        Fast,

        /// <summary>
        /// Heavy enemy type.
        /// </summary>
        Heavy,

        /// <summary>
        /// Rapid Fire enemy.
        /// </summary>
        RapidFire,

        /// <summary>
        /// Burst shot enemy.
        /// </summary>
        Burst,
    }

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
        /// <param name="enemyType">Type of enemy.</param>
        /// <param name="spriteFile">Spritesheet file name.</param>
        public Enemy(Point pos, int health, int height, int width, EnemyType enemyType, string spriteFile)
            : base(pos, health, height, width, spriteFile)
        {
            this.Type = enemyType;
            this.StanceLess = true;
        }

        /// <summary>
        /// Gets or sets the type of enemy.
        /// </summary>
        public EnemyType Type { get; set; }
    }
}
