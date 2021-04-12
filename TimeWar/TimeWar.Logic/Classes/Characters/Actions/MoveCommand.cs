// <copyright file="MoveCommand.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Logic.Interfaces;
    using TimeWar.Model;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Move commands.
    /// </summary>
    public class MoveCommand : ICommand
    {
        private GameModel model;
        private IMoveable gameEntity;
        private Point position;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveCommand"/> class.
        /// </summary>
        /// <param name="gameEntity">Moveable game entity.</param>
        /// <param name="position">Position of the game entity.</param>
        /// <param name="model">Game model.</param>
        public MoveCommand(IMoveable gameEntity, Point position, GameModel model)
        {
            this.gameEntity = gameEntity;
            this.position = position;
            this.model = model;
        }

        /// <inheritdoc/>
        public void Undo()
        {
            this.gameEntity.Position = new Point(this.position.X, this.position.Y);
        }
    }
}
