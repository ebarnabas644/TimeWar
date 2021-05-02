// <copyright file="BurstEnemyLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Burst enemy logic.
    /// </summary>
    public class BurstEnemyLogic : BasicEnemyLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BurstEnemyLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="character">Character.</param>
        /// <param name="commandManager">Command manager.</param>
        public BurstEnemyLogic(GameModel model, Character character, CommandManager commandManager)
            : base(model, character, commandManager)
        {
            this.Character.Health = 150;
            this.MaxMovementSpeed = 6;
            this.MaxJumpHeight = 15;
            this.AttackTime = 3000;
            this.TypeOfBullet = BulletType.BasicEnemyBullet;
            this.MaxMoveTime = 1000;
            this.DefaultFollowDistance = RandomNumberGenerator.GetInt32(20, 24);
            this.DetectionRange = 20;
            this.DetectionTime = 15000;
        }

        /// <summary>
        /// Attack method.
        /// </summary>
        protected override void Attack()
        {
            if (this.IsPlayerDetected && this.CommandManager.IsFinished && this.Character.CanAttack && this.AttackStopwatch.ElapsedMilliseconds > this.AttackTime)
            {
                for (int i = 0; i < RandomNumberGenerator.GetInt32(0, 7); i++)
                {
                    int inaccuracy = RandomNumberGenerator.GetInt32(-75, 76);
                    Point attackPoint = new Point(this.Character.Position.X + this.Model.CurrentWorld.ConvertTileToPixel(1), this.Character.Position.Y + this.Model.CurrentWorld.ConvertTileToPixel(1));

                    Bullet b = new Bullet(attackPoint, 4, 4, "testenemy.png", new Point(this.TileToPixel(this.LastKnownPlayerLocation.X), this.TileToPixel(this.LastKnownPlayerLocation.Y + 2) - inaccuracy), 10, this.TypeOfBullet);
                    this.Model.CurrentWorld.AddBullet(b);
                }

                this.AttackStopwatch.Restart();
            }
        }
    }
}
