// <copyright file="CharacterLogic.cs" company="Time War">
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
            this.EffectStopwatch = new Stopwatch();
            this.AttackTime = 200;
            this.EffectCounter = 0;
        }

        /// <summary>
        /// Gets or sets time between attacks.
        /// </summary>
        public int AttackTime { get; set; }

        /// <summary>
        /// Gets or sets effect stopwatch.
        /// </summary>
        public Stopwatch EffectStopwatch { get; set; }

        /// <summary>
        /// Gets or sets the number of effects.
        /// </summary>
        public int EffectCounter { get; set; }

        /// <inheritdoc/>
        public override void OneTick()
        {
            base.OneTick();
            this.Attack();
            this.DetectHealt();
            this.ManageEffects();
        }

        /// <inheritdoc/>
        protected override void Attack()
        {
            int attackDamage = 0;
            double delay = this.AttackTime;
            switch (this.Character.TypeOfBullet)
            {
                case BulletType.Basic:
                    attackDamage = 5;
                    delay *= 2;
                    break;
                case BulletType.Accelerating:
                    attackDamage = 40;
                    delay *= 4;
                    break;
                case BulletType.Bouncing:
                    attackDamage = 15;
                    delay *= 3;
                    break;
                case BulletType.CurvedBouncing:
                    attackDamage = 30;
                    delay *= 3.5;
                    break;
                default:
                    break;
            }

            if (this.CommandManager.IsFinished && this.Character.CanAttack && this.AttackStopwatch.ElapsedMilliseconds > delay)
            {
                int inaccuracy = 0;
                if (this.Character.TypeOfBullet != BulletType.Accelerating)
                {
                    inaccuracy = RandomNumberGenerator.GetInt32(-76, 76);
                }

                Point attackPoint = new Point(this.Character.Position.X, this.Character.Position.Y);
                Bullet b = new Bullet(attackPoint, 4, 4, "testenemy.png", new Point(this.Character.ClickLocation.X, this.Character.ClickLocation.Y - inaccuracy), attackDamage, this.Character.TypeOfBullet, true);
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

            if (this.Character.CurrentHealth < 0)
            {
                this.Model.Hero.PlayerDeath();
                this.CommandManager.ClearBuffer();
                this.Model.CurrentWorld.LoadEnemies();
            }
        }

        private void ManageEffects()
        {
            if (this.EffectCounter != 0 && !this.EffectStopwatch.IsRunning)
            {
                this.EffectStopwatch.Start();
            }

            if (this.EffectStopwatch.IsRunning && this.EffectCounter == 0)
            {
                this.EffectStopwatch.Reset();
            }
        }
    }
}