// <copyright file="HeavyEnemyLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Heavy enemy.
    /// </summary>
    public class HeavyEnemyLogic : BasicEnemyLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeavyEnemyLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="character">Character.</param>
        /// <param name="commandManager">Command manager.</param>
        public HeavyEnemyLogic(GameModel model, Character character, CommandManager commandManager)
            : base(model, character, commandManager)
        {
            this.Character.Health = 200;
            this.MaxMovementSpeed = 7;
            this.MaxJumpHeight = 15;
            this.BulletType = BulletType.Accelerating;
        }
    }
}
