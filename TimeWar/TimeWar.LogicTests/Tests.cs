// <copyright file="Tests.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.LogicTests
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading;
    using NUnit.Framework;
    using TimeWar.Logic;
    using TimeWar.Logic.Classes;
    using TimeWar.Logic.Classes.Characters;
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Logic.Classes.LogicCollections;
    using TimeWar.Logic.Classes.POIs;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Test class for logic methods.
    /// </summary>
    public class Tests
    {
        private CommandManager commandManager;
        private CharacterLogic characterLogic;
        private Player player;
        private EnemyLogics enemyLogics;
        private PointOfInterestLogics pois;
        private BulletLogics bullets;

        private GameWorld gameWorld;
        private GameModel gameModel;

        /// <summary>
        /// Sets up testing.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.commandManager = new CommandManager();
            this.player = new Player(new Point(50, 50), 1, 8, 2, "cucc");
            this.gameWorld = new GameWorld(100, 100, 8);
            this.gameModel = new GameModel();
            this.gameModel.CurrentWorld = this.gameWorld;
            this.gameModel.Hero = this.player;
            this.characterLogic = new CharacterLogic(this.gameModel, this.gameModel.Hero, this.commandManager);
            BasicEnemyLogic basicEnemyLogic;
            this.enemyLogics = new EnemyLogics(this.gameModel, this.commandManager);
            Enemy enemy = new Enemy(new Point(55, 55), 1, 8, 2, EnemyType.Basic, "cucc");
            basicEnemyLogic = new BasicEnemyLogic(this.gameModel, enemy, this.commandManager);
            this.gameModel.CurrentWorld.AddEnemy(enemy);
            this.enemyLogics.GetEnemies();
            this.bullets = new BulletLogics(this.gameModel, new List<Bullet>(), this.commandManager);
            this.pois = new PointOfInterestLogics(this.gameModel, this.characterLogic, this.commandManager);
        }

        /// <summary>
        /// Tests player movement.
        /// </summary>
        [TestCase]
        public void PlayerMovementTest()
        {
            this.characterLogic.Character.MovementVector = new Point(-5, 0);

            for (int i = 0; i < 5; i++)
            {
                int prevPos = this.gameModel.Hero.Position.X;
                this.characterLogic.OneTick();
                Assert.IsTrue(this.gameModel.Hero.Position.X < prevPos);
            }

            this.characterLogic.Character.MovementVector = new Point(0, 0);
            this.characterLogic.Character.MovementVector = new Point(5, 0);
            for (int i = 0; i < 5; i++)
            {
                int prevPos = this.gameModel.Hero.Position.X;
                this.characterLogic.OneTick();
                Assert.IsTrue(this.gameModel.Hero.Position.X > prevPos);
            }

            this.characterLogic.Character.MovementVector = new Point(0, 0);
            this.characterLogic.Character.MovementVector = new Point(0, -5);
            for (int i = 0; i < 5; i++)
            {
                int prevPos = this.gameModel.Hero.Position.Y;
                this.characterLogic.OneTick();
                Assert.IsTrue(this.gameModel.Hero.Position.Y < prevPos);
            }

            this.characterLogic.Character.MovementVector = new Point(0, 0);
            this.characterLogic.Character.MovementVector = new Point(0, 5);
            for (int i = 0; i < 5; i++)
            {
                int prevPos = this.gameModel.Hero.Position.Y;
                this.characterLogic.OneTick();
                Assert.IsTrue(this.gameModel.Hero.Position.Y > prevPos);
            }
        }

        /// <summary>
        /// Test rewind feature.
        /// </summary>
        [TestCase]
        public void TestRewind()
        {
            this.characterLogic.Character.MovementVector = new Point(-5, 0);
            for (int i = 0; i < 5; i++)
            {
                int prevPos = this.gameModel.Hero.Position.X;
                this.characterLogic.OneTick();
                Assert.IsTrue(this.gameModel.Hero.Position.X < prevPos);
            }

            int prevPosX = this.gameModel.Hero.Position.X;
            this.commandManager.Rewind().Start();
            while (!this.commandManager.IsFinished)
            {
                Thread.Sleep(1);
            }

            Assert.IsTrue(this.gameModel.Hero.Position.X > prevPosX);
        }

        /// <summary>
        /// Test shootin feature.
        /// </summary>
        [TestCase]
        public void ShootingTest()
        {
            this.characterLogic.Character.Position = new Point(50, 50);
            this.characterLogic.Character.MovementVector = new Point(0, 0);
            Assert.IsTrue(!this.characterLogic.Character.CanAttack);
            this.characterLogic.AttackTime = 0;
            this.characterLogic.OneTick();
            Thread.Sleep(1);
            this.characterLogic.Character.CanAttack = true;
            this.characterLogic.OneTick();
            this.characterLogic.Character.ClickLocation = new Point(55, 55);
            Thread.Sleep(100);
            Assert.AreEqual(1, this.gameModel.CurrentWorld.BulletCount);
        }

        /// <summary>
        /// Test player damageing feature.
        /// </summary>
        [TestCase]
        public void BulletDirectionTest()
        {
            this.characterLogic.Character.Position = new Point(50, 50);
            Bullet enemyBullet = new Bullet(new Point(40, 40), 4, 4, " ", this.player.Position, 10, BulletType.BasicEnemyBullet);
            this.gameModel.CurrentWorld.AddBullet(enemyBullet);
            this.bullets.Addbullets((List<Bullet>)this.gameModel.CurrentWorld.GetBullets);
            Assert.AreEqual(1, this.gameModel.CurrentWorld.GetBullets.Count);
            for (int i = 0; i < 5; i++)
            {
                this.bullets.OneTick();
            }

            Assert.IsTrue(enemyBullet.Position.X > this.characterLogic.Character.Position.X);
            Assert.IsTrue(enemyBullet.Position.Y > this.characterLogic.Character.Position.Y);
        }

        /// <summary>
        /// Test rewind feature.
        /// </summary>
        [TestCase]
        public void EffectTest()
        {
            this.player.Position = new Point(50, 50);
            this.gameModel.Hero.Width = 8;
            this.player.Health = 100;
            this.player.CurrentHealth = 1;
            PointOfInterest poi = new PointOfInterest(POIType.HealthKit, 4, 4, " ", new Point(this.gameModel.CurrentWorld.ConvertPixelToTile(50), this.gameModel.CurrentWorld.ConvertPixelToTile(50)));
            HealthKitLogic healthKitLogic = new HealthKitLogic(this.gameModel, poi, 25);
            this.gameModel.CurrentWorld.AddPOI(poi);
            this.pois = new PointOfInterestLogics(this.gameModel, this.characterLogic, this.commandManager);
            this.player.MovementVector = new Point(5);
            this.characterLogic.OneTick();
            this.pois.TickPois();
            Assert.AreEqual(26, this.gameModel.Hero.CurrentHealth);
        }
    }
}
