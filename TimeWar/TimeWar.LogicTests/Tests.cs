namespace TimeWar.LogicTests
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using TimeWar.Logic;
    using TimeWar.Logic.Classes;
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Logic.Interfaces;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Test class for logic methods.
    /// </summary>
    public class Tests
    {
        private MoveCommand moveCommand;
        private CommandManager commandManager;
        private CharacterLogic characterLogic;
        private Player player;
        private GameWorld gameWorld;
        private GameModel gameModel;

        /// <summary>
        /// Sets up testing.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.commandManager = new CommandManager();
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
            Assert.AreEqual(new Point(50, 50), this.player.Position);
            this.moveCommand = new MoveCommand(this.player, new Point(0, 1), this.gameModel);
            Assert.AreNotEqual(new Point(0, 1), this.player.Position);
            this.moveCommand.Execute();
            Assert.AreNotEqual(new Point(0, 1), this.player.Position);
            Assert.AreEqual(new Point(50, 51), this.player.Position);
            this.moveCommand.Undo();
            Assert.AreEqual(new Point(50, 50), this.player.Position);
        }

        /// <summary>
        /// Test rewind feature.
        /// </summary>
        [TestCase]
        public void TestRewind()
        {
            Assert.AreEqual(new Point(50, 50), this.player.Position);
            this.moveCommand = new MoveCommand(this.player, new Point(0, 1), this.gameModel);
            this.commandManager.AddCommand(this.moveCommand);
            Assert.AreEqual(new Point(50, 50), this.player.Position);
            this.moveCommand.Execute();

            Assert.AreNotEqual(new Point(0, 1), this.player.Position);
            Assert.AreEqual(new Point(50, 51), this.player.Position);
            this.moveCommand.Undo();
            Assert.AreEqual(new Point(50, 50), this.player.Position);

            for (int i = 0; i < 4; i++)
            {
                this.moveCommand = new MoveCommand(this.player, new Point(0, 1), this.gameModel);
                this.moveCommand.Execute();
                this.commandManager.AddCommand(this.moveCommand);
                Assert.AreEqual(new Point(50, 51 + i), this.player.Position);
            }

            this.commandManager.Rewind();
            Assert.AreNotEqual(new Point(50, 50), this.player.Position);
            Thread.Sleep(1000);
            Assert.AreNotEqual(new Point(50, 50), this.player.Position);

            this.moveCommand = new MoveCommand(this.player, new Point(0, 1), this.gameModel);
            this.moveCommand.Execute();
            this.commandManager.AddCommand(this.moveCommand);
            this.commandManager.ClearBuffer();
            this.commandManager.Rewind();
            Assert.AreNotEqual(new Point(50, 50), this.player.Position);
        }
    }
}
