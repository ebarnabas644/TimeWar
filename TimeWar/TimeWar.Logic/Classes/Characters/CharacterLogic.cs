// <copyright file="CharacterLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
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
        }

        /// <inheritdoc/>
        protected override void Attack()
        {
            if (this.CommandManager.IsFinished && this.Character.CanAttack && this.AttackStopwatch.ElapsedMilliseconds > 500)
            {
                Bullet b = new Bullet(this.Character.Position, 4, 4, "testenemy.png", this.Character.ClickLocation, 10, BulletType.Bouncing);
                this.Model.CurrentWorld.AddBullet(b);
                this.AttackStopwatch.Restart();
                this.Character.CanAttack = false;
                Debug.WriteLine("Attacked at position:" + this.Character.ClickLocation);
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

                if (y != 0 && Math.Abs(this.MoveVector.X) >= 14)
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
    }
}