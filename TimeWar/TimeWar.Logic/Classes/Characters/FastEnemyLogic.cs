// <copyright file="FastEnemyLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters
{
    using TimeWar.Model;
    using TimeWar.Model.Objects;

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
            this.Character.Health = 25;
        }
    }
}
