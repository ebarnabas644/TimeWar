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
    using TimeWar.Model;

    /// <summary>
    /// Command manager class.
    /// </summary>
    public class CommandManager : ICommandManager
    {
        private const int RewindTime = 750;
        private List<ICommand> commandBuffer;
        private Stopwatch rewindStopwatch;
        private GameModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        public CommandManager(GameModel model)
        {
            this.model = model;
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
        public Task Rewind(int number)
        {
            Task task = new Task(() => Debug.WriteLine("Rewind not finished yet"));
            if (this.IsFinished)
            {
                this.IsFinished = false;
                this.model.InRewind = true;
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
                        }
                        else
                        {
                            break;
                        }

                        if (counter % number == 0)
                        {
                            Thread.Sleep(32);
                        }

                        counter++;
                    }

                    this.rewindStopwatch.Reset();
                    this.IsFinished = true;
                    this.model.InRewind = false;
                    this.ClearBuffer();
                }, TaskCreationOptions.LongRunning);
            }

            return task;
        }
    }
}
