// <copyright file="CharacterLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using TimeWar.Logic.Classes;
    using TimeWar.Logic.Classes.Characters;
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
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
        }

        /// <inheritdoc/>
        public override void OneTick()
        {
            base.OneTick();
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

            if (this.Character.ContainKey("s"))
            {
                y += 1;
            }

            if (this.Character.ContainKey("space"))
            {
                if (!this.IsJumping && this.JumpingTimeOut.ElapsedMilliseconds > 250)
                {
                    this.JumpingTimeOut.Restart();
                    this.IsJumping = true;
                    this.AccelerationStopwatch.Start();
                    if (Math.Abs(this.MoveVector.X) >= 15)
                    {
                        y -= this.MaxJumpHeight + 2;
                    }
                    else
                    {
                        y -= this.MaxJumpHeight;
                    }
                }
            }

            return new Point(x, y);
        }
    }
}