// <copyright file="CommandManager.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using TimeWar.Logic.Interfaces;

    /// <summary>
    /// Command manager class.
    /// </summary>
    public class CommandManager : ICommandManager
    {
        private const int RewindTime = 1500;
        private List<ICommand> commandBuffer;
        private Stopwatch rewindStopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        public CommandManager()
        {
            this.commandBuffer = new List<ICommand>();
            this.IsFinished = true;
            this.rewindStopwatch = new Stopwatch();
        }

        /// <inheritdoc/>
        public bool IsFinished { get; set; }

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
            Task task = new Task(() => Debug.WriteLine("Rewind not finished yet"));
            if (this.IsFinished)
            {
                this.IsFinished = false;
                int counter = 0;
                task = new Task(
                    () =>
                {
                    this.rewindStopwatch.Start();
                    foreach (ICommand command in Enumerable.Reverse(this.commandBuffer))
                    {
                        if (this.rewindStopwatch.ElapsedMilliseconds < RewindTime)
                        {
                            command.Undo();
                            Thread.Sleep(5);
                        }

                        counter++;
                    }

                    this.rewindStopwatch.Reset();
                    this.IsFinished = true;
                    this.ClearBuffer();
                }, TaskCreationOptions.LongRunning);
            }

            return task;
        }
    }
}
