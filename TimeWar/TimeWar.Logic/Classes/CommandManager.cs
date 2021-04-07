﻿// <copyright file="CommandManager.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using TimeWar.Logic.Interfaces;

    /// <summary>
    /// Command manager class.
    /// </summary>
    public class CommandManager : ICommandManager
    {
        private List<ICommand> commandBuffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        public CommandManager()
        {
            this.commandBuffer = new List<ICommand>();
        }

        /// <inheritdoc/>
        public void AddCommand(ICommand command)
        {
            this.commandBuffer.Add(command);
        }

        /// <inheritdoc/>
        public void ClearBuffer()
        {
            this.commandBuffer.Clear();
        }

        /// <inheritdoc/>
        public Task Rewind()
        {
            int counter = 0;
            Task task = new Task(
                () =>
            {
                foreach (ICommand command in Enumerable.Reverse(this.commandBuffer))
                {
                    if (counter < 50)
                    {
                        command.Undo();
                        Thread.Sleep(10);
                    }

                    counter++;
                }

                this.ClearBuffer();
            }, TaskCreationOptions.LongRunning);

            return task;
        }
    }
}
