// <copyright file="EnemyLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.LogicCollections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Logic.Classes.Characters;
    using TimeWar.Model;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Enemy logic collection.
    /// </summary>
    public class EnemyLogic
    {
        private GameModel model;
        private List<BasicEnemyLogic> enemies;
        private CommandManager commandManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="commandManager">Command manager.</param>
        public EnemyLogic(GameModel model, CommandManager commandManager)
        {
            this.model = model;
            this.commandManager = commandManager;
            this.enemies = new List<BasicEnemyLogic>();
            this.GetEnemies();
        }

        /// <summary>
        /// Gets enemies.
        /// </summary>
        public void GetEnemies()
        {
            foreach (Enemy enemy in this.model.CurrentWorld.GetEnemies)
            {
                switch (enemy.Type)
                {
                    case EnemyType.Basic:
                        BasicEnemyLogic basicEnemy = new BasicEnemyLogic(this.model, enemy, this.commandManager);
                        this.enemies.Add(basicEnemy);
                        break;
                    case EnemyType.Fast:
                        FastEnemyLogic fastEnemy = new FastEnemyLogic(this.model, enemy, this.commandManager);
                        this.enemies.Add(fastEnemy);
                        break;
                    case EnemyType.Heavy:
                        HeavyEnemyLogic heavyEnemy = new HeavyEnemyLogic(this.model, enemy, this.commandManager);
                        this.enemies.Add(heavyEnemy);
                        break;
                    case EnemyType.RapidFire:
                        RapidFireEnemyLogic rapidFireEnemyLogic = new RapidFireEnemyLogic(this.model, enemy, this.commandManager);
                        this.enemies.Add(rapidFireEnemyLogic);
                        break;
                    case EnemyType.Burst:
                        BurstEnemyLogic burstEnemy = new BurstEnemyLogic(this.model, enemy, this.commandManager);
                        this.enemies.Add(burstEnemy);
                        break;
                    default:
                        BasicEnemyLogic defaultEnemy = new BasicEnemyLogic(this.model, enemy, this.commandManager);
                        this.enemies.Add(defaultEnemy);
                        break;
                }
            }
        }

        /// <summary>
        /// Tick enemies in list.
        /// </summary>
        public void TickEnemies()
        {
            foreach (BasicEnemyLogic enemyLogic in this.enemies)
            {
                enemyLogic.OneTick();
                this.Despawn(enemyLogic);
            }
        }

        private void Despawn(BasicEnemyLogic enemyLogic)
        {
            if (enemyLogic.Character.Health <= 0 || (enemyLogic.Character.Position.X > this.model.CurrentWorld.GameWidth || enemyLogic.Character.Position.X < 0) || (enemyLogic.Character.Position.Y > this.model.CurrentWorld.GameHeight || enemyLogic.Character.Position.X < 0))
            {
                this.model.CurrentWorld.RemoveEnemy((Enemy)enemyLogic.Character);
                this.enemies.Remove(enemyLogic);
            }
        }
    }
}
