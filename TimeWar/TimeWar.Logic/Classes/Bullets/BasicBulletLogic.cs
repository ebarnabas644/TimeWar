// <copyright file="BasicBulletLogic.cs" company="Time War">
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
    public class BasicBulletLogic
    {
        private GameModel model;
        private Bullet bullet;
        private CommandManager commandManager;
        private int acceleration;
        private Point destination;
        private Point moveVector;

        public BasicBulletLogic(GameModel model, Bullet bullet, CommandManager commandManager, Point destination)
        {
            this.model = model;
            this.bullet = bullet;
            this.commandManager = commandManager;
            this.destination = destination;
            this.acceleration = 50;
        }

        public void OneTick()
        {
            throw new NotImplementedException();
        }

        public void Movement()
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
