// <copyright file="ICommandManager.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Command manager interface.
    /// </summary>
    public interface ICommandManager
    {
        /// <summary>
        /// Add new command.
        /// </summary>
        /// <param name="command">Command.</param>
        void AddCommand(ICommand command);

        /// <summary>
        /// Clear command buffer.
        /// </summary>
        void ClearBuffer();

        /// <summary>
        /// Rewind all command.
        /// </summary>
        /// <returns>Task with rewind logic.</returns>
        Task Rewind();
    }
}
