// <copyright file="BulletLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.Characters.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Model;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Default bullet logic.
    /// </summary>
    public class BulletLogic
    {
        private GameModel model;
        private Bullet bullet;
        private CommandManager commandManager;
        private int acceleration;
        private Point destination;
        private Point moveVector;

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletLogic"/> class.
        /// </summary>
        /// <param name="model">Game Model.</param>
        /// <param name="bullet">Bullet.</param>
        /// <param name="commandManager">Command manager.</param>
        /// <param name="destination">Destination.</param>
        public BulletLogic(GameModel model, Bullet bullet, CommandManager commandManager, Point destination)
        {
            this.model = model;
            this.bullet = bullet;
            this.commandManager = commandManager;
            this.destination = destination;
            this.acceleration = 50;
        }

        /// <summary>
        /// One tick method.
        /// </summary>
        public void OneTick()
        {
            this.Movement();
            this.DetectEntity();
            this.DetectGround();
        }

        private void Movement()
        {
            switch (this.bullet.Type)
            {
                case BulletType.Basic:
                    this.BasicMovement();
                    break;
                case BulletType.Accelerating:
                    this.AcceleratigMovement();
                    break;
                case BulletType.Bouncing:
                    this.BouncingMovement();
                    break;
                case BulletType.CurvedBouncing:
                    this.CurvedMovement();
                    break;
                default:
                    break;
            }
        }

        private void BasicMovement()
        {
            throw new NotImplementedException();
        }

        private void AcceleratigMovement()
        {
            throw new NotImplementedException();
        }

        private void BouncingMovement()
        {
            throw new NotImplementedException();
        }

        private void CurvedMovement()
        {
            throw new NotImplementedException();
        }

        private bool DetectGround()
        {
            throw new NotImplementedException();
        }

        private bool DetectEntity()
        {
            throw new NotImplementedException();
        }

        private void Despawn()
        {
            throw new NotImplementedException();
        }
    }
}
