// <copyright file="ICommand.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Interfaces
{
    /// <summary>
    /// Command interface for actions.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Undo the command.
        /// </summary>
        void Undo();
    }
}
