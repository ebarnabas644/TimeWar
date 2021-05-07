// <copyright file="ICommandManager.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// Command manager interface.
    /// </summary>
    public interface ICommandManager
    {
        /// <summary>
        /// Gets or sets a value indicating whether the rewind is finished.
        /// </summary>
        bool IsFinished { get; set; }

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
        /// <param name="number">Number of moving object.</param>
        /// <returns>Task with rewind logic.</returns>
        Task Rewind(int number);
    }
}
