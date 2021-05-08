// <copyright file="EnemyLogics.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.LogicCollections
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Logic.Classes.Characters;
    using TimeWar.Model;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Enemy logic collection.
    /// </summary>
    public class EnemyLogics
    {
        private const int TickDistance = 100;
        private GameModel model;
        private List<BasicEnemyLogic> enemies;
        private CommandManager commandManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyLogics"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="commandManager">Command manager.</param>
        public EnemyLogics(GameModel model, CommandManager commandManager)
        {
            this.model = model;
            this.commandManager = commandManager;
            this.enemies = new List<BasicEnemyLogic>();
            this.GetEnemies();
        }

        /// <summary>
        /// Saves enemies into a string.
        /// </summary>
        /// <returns>A list of all enemies.</returns>
        public ICollection<string> SaveEnemies()
        {
            List<string> enemiesString = new List<string>();
            foreach (var item in this.enemies)
            {
                enemiesString.Add((item.Character as Enemy).ToString());
            }

            return enemiesString;
        }

        /// <summary>
        /// Gets enemies.
        /// </summary>
        public void GetEnemies()
        {
            this.enemies = new List<BasicEnemyLogic>();
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
            if (this.commandManager.IsFinished)
            {
                for (int i = 0; i < this.enemies.Count; i++)
                {
                    if (this.DistanceFromPlayer((Enemy)this.enemies[i].Character) < TickDistance)
                    {
                        this.enemies[i].OneTick();
                        this.Despawn(this.enemies[i]);
                    }
                }
            }
        }

        private int DistanceFromPlayer(Enemy enemy)
        {
            return this.model.CurrentWorld.ConvertPixelToTile((int)Math.Sqrt(Math.Pow(enemy.Position.X - this.model.Hero.Position.X, 2) + Math.Pow(enemy.Position.Y - this.model.Hero.Position.Y, 2)));
        }

        private void Despawn(BasicEnemyLogic enemyLogic)
        {
            if (enemyLogic.Character.CurrentHealth <= 0 || (enemyLogic.Character.Position.X >= this.model.CurrentWorld.GameWidth || enemyLogic.Character.Position.X <= 0) || (enemyLogic.Character.Position.Y >= this.model.CurrentWorld.GameHeight || enemyLogic.Character.Position.Y <= 0))
            {
                if (enemyLogic.Character.CurrentHealth <= 0)
                {
                    this.model.Hero.Kills++;
                }

                this.model.CurrentWorld.RemoveEnemy((Enemy)enemyLogic.Character);
                Debug.WriteLine("Enemy Despawned");
                this.enemies.Remove(enemyLogic);
            }
        }
    }
}
