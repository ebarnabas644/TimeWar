﻿// <copyright file="RapidFireEnemyLogic.cs" company="Time War">
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
    /// Rapid fire enemy.
    /// </summary>
    public class RapidFireEnemyLogic : BasicEnemyLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RapidFireEnemyLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="character">Character.</param>
        /// <param name="commandManager">Command manager.</param>
        public RapidFireEnemyLogic(GameModel model, Character character, CommandManager commandManager)
            : base(model, character, commandManager)
        {
            this.Character.Health = 150;
            this.MaxMovementSpeed = 5;
            this.MaxJumpHeight = 10;
            this.AttackTime = 100;
            this.TypeOfBullet = BulletType.Basic;
            this.MaxMoveTime = 2500;
            this.DefaultFollowDistance = 20;
            this.DetectionRange = 25;
            this.DetectionTime = 10000;
        }
    }
}
