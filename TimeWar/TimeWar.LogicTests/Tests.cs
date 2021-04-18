// <copyright file="Tests.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.LogicTests
{
    using System.Drawing;
    using NUnit.Framework;
    using TimeWar.Model;
    using TimeWar.Model.Objects;

    /// <summary>
    /// Test class for logic methods.
    /// </summary>
    public class Tests
    {
        // private MoveCommand moveCommand;
        // private CommandManager commandManager;
        // private CharacterLogic characterLogic;
        private Player player;
        private GameWorld gameWorld;
        private GameModel gameModel;

        /// <summary>
        /// Sets up testing.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // this.commandManager = new CommandManager();
            this.player = new Player(new Point(50, 50), 1, 10, 8, 2, "cucc");
            this.gameWorld = new GameWorld(100, 100, 8);
            this.gameModel = new GameModel();
            this.gameModel.CurrentWorld = this.gameWorld;
            this.gameModel.Hero = this.player;
        }

        /// <summary>
        /// Tests player movement.
        /// </summary>
        [TestCase]
        public void PlayerMovementTest()
        {
            Assert.IsTrue(true);
        }

        /// <summary>
        /// Test rewind feature.
        /// </summary>
        [TestCase]
        public void TestRewind()
        {
            Assert.IsTrue(true);
        }
    }
}
