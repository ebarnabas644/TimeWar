﻿// <copyright file="MenuControl.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using TimeWar.Logic;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Renderer;

    /// <summary>
    /// Main menu control class.
    /// </summary>
    public class MenuControl : FrameworkElement
    {
        private GameModel model;
        private InitLogic initLogic;
        private GameRenderer renderer;
        private Logic.Classes.CommandManager commandManager;
        private CharacterLogic characterLogic;
        private Window win;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuControl"/> class.
        /// </summary>
        public MenuControl()
        {
            this.Loaded += this.GameControl_Loaded;
        }

        /// <summary>
        /// Render drawing groups.
        /// </summary>
        /// <param name="drawingContext">Canvas.</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.renderer != null && drawingContext != null)
            {
                drawingContext.DrawDrawing(this.renderer.BuildDrawing());
            }
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.model = new GameModel();
            this.initLogic = new InitLogic(this.model, "test");
            this.model.Camera = new Viewport((int)this.ActualWidth, (int)this.ActualHeight, (int)this.model.CurrentWorld.GameWidth, (int)this.model.CurrentWorld.GameHeight, this.model.Hero);
            this.renderer = new GameRenderer(this.model, true);
            this.commandManager = new Logic.Classes.CommandManager();
            this.characterLogic = new CharacterLogic(this.model, this.model.Hero, this.commandManager);
            this.win = Window.GetWindow(this);
            if (this.win != null)
            {
                this.win.SizeChanged += this.Win_SizeChanged;
                this.win.MouseMove += this.Win_MouseMove;
                CompositionTarget.Rendering += this.CompositionTarget_Rendering;
            }

            this.InvalidateVisual();
        }

        private void Win_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        private void Win_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.model.Camera.WindowHeight = (int)this.ActualHeight;
            this.model.Camera.WindowWidth = (int)this.ActualWidth;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            this.characterLogic.OneTick();
            this.InvalidateVisual();
        }
    }
}
