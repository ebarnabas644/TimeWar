// <copyright file="CharacterLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Logic.Classes;
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Basic character logic class.
    /// </summary>
    public class CharacterLogic
    {
        private GameModel model;
        private Character character;
        private CommandManager commandManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterLogic"/> class.
        /// </summary>
        /// <param name="model">Game model entity.</param>
        /// <param name="character">Moveable entity.</param>
        /// <param name="commandManager">Command manager entity.</param>
        public CharacterLogic(GameModel model, Character character, CommandManager commandManager)
        {
            this.model = model;
            this.character = character;
            this.commandManager = commandManager;
        }

        /// <summary>
        /// 1 frame event.
        /// </summary>
        public void OneTick()
        {
            MoveCommand command = new MoveCommand(this.character, this.Move(), this.model);
            command.Execute();
            this.commandManager.AddCommand(command);
        }

        private Point Move()
        {
            Point direction;
            switch (this.character.Direction)
            {
                case Directions.Stand:
                    direction = new Point(0, 0);
                    break;
                case Directions.Right:
                    direction = new Point(1, 0);
                    break;
                case Directions.Left:
                    direction = new Point(-1, 0);
                    break;
                case Directions.Up:
                    direction = new Point(0, -1);
                    break;
                case Directions.Down:
                    direction = new Point(0, 1);
                    break;
                default:
                    direction = new Point(0, 0);
                    break;
            }

            return direction;
        }
    }
}
