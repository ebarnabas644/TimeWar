// <copyright file="FastEnemyLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters
{
    using System.Security.Cryptography;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Fast enemy class.
    /// </summary>
    public class FastEnemyLogic : BasicEnemyLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FastEnemyLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="character">Character.</param>
        /// <param name="commandManager">Command manager.</param>
        public FastEnemyLogic(GameModel model, Character character, CommandManager commandManager)
            : base(model, character, commandManager)
        {
            this.MaxJumpHeight = 30;
            this.MaxMovementSpeed = 40;
            this.MaxMoveTime = 1000;
            this.Character.Health = 25;
            this.DefaultFollowDistance = RandomNumberGenerator.GetInt32(1, 4);
            this.DetectionRange = 10;
            this.AttackTime = 500;
            this.DetectionTime = 5000;
            this.TypeOfBullet = BulletType.Accelerating;
            this.AttackValue = 45;
        }
    }
}
