﻿// <copyright file="CharacterLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Security.Cryptography;
    using TimeWar.Logic.Classes;
    using TimeWar.Logic.Classes.Characters;
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Basic character logic class.
    /// </summary>
    public class CharacterLogic : ActorLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterLogic"/> class.
        /// </summary>
        /// <param name="model">Game model entity.</param>
        /// <param name="character">Moveable entity.</param>
        /// <param name="commandManager">Command manager entity.</param>
        public CharacterLogic(GameModel model, Character character, CommandManager commandManager)
            : base(model, character, commandManager)
        {
            this.AttackStopwatch.Start();
        }

        /// <inheritdoc/>
        public override void OneTick()
        {
            base.OneTick();
            this.Attack();
            this.DetectHealt();
        }

        /// <inheritdoc/>
        protected override void Attack()
        {
            if (this.CommandManager.IsFinished && this.Character.CanAttack && this.AttackStopwatch.ElapsedMilliseconds > 500)
            {
                int inaccuracy = 0;
                if (this.Character.TypeOfBullet != BulletType.Accelerating)
                {
                    inaccuracy = RandomNumberGenerator.GetInt32(0, 76);
                }

                Point attackPoint = new Point(this.Character.Position.X + this.Model.CurrentWorld.ConvertTileToPixel(1), this.Character.Position.Y + this.Model.CurrentWorld.ConvertTileToPixel(1));
                Bullet b = new Bullet(attackPoint, 4, 4, "testenemy.png", new Point(this.Character.ClickLocation.X, this.Character.ClickLocation.Y - inaccuracy), 10, this.Character.TypeOfBullet, true);
                this.Model.CurrentWorld.AddBullet(b);
                this.AttackStopwatch.Restart();
                this.Character.CanAttack = false;
            }
        }

        /// <inheritdoc/>
        protected override Point Move()
        {
            int x = 0;
            int y = 0;
            if (this.Character.ContainKey("a"))
            {
                x -= 2;
            }

            if (this.Character.ContainKey("d"))
            {
                x += 2;
            }

            if (this.Character.ContainKey("space"))
            {
                if (this.GroundCollision(new Point(0, this.Model.CurrentWorld.TileSize)))
                {
                    y += this.Jump();
                }

                if (y != 0 && Math.Abs(this.Character.MovementVector.X) >= 14)
                {
                    y -= 2;
                }
            }

            if (this.Character.ContainKey("s"))
            {
                y += 1;
            }

            return new Point(x, y);
        }

        private void DetectHealt()
        {
            if (this.Character.CurrentShield < this.Character.Shield && !this.Character.ShieldRegenTimer.IsRunning)
            {
                this.Character.ShieldRegenTimer.Start();
            }

            if (this.Character.ShieldRegenTimer.ElapsedMilliseconds > this.Character.ShieldRegenTime)
            {
                this.Character.CurrentShield += this.Character.ShieldRegenValue;
                if (this.Character.CurrentShield >= this.Character.Shield)
                {
                    this.Character.ShieldRegenTimer.Reset();
                }
            }
        }
    }
}