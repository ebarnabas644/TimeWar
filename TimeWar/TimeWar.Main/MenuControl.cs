// <copyright file="MenuControl.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using CommonServiceLocator;
    using TimeWar.Logic;
    using TimeWar.Main.BL;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Renderer;

    /// <summary>
    /// Main menu control class.
    /// </summary>
    internal class MenuControl : FrameworkElement
    {
        private GameModel model;
        private InitLogic initLogic;
        private GameRenderer renderer;
        private Logic.Classes.CommandManager commandManager;
        private CharacterLogic characterLogic;
        private Window win;
        private bool exit;
        private Factory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuControl"/> class.
        /// </summary>
        public MenuControl()
            : this(ServiceLocator.Current.GetInstance<Factory>())
        {
            this.exit = false;
            this.Loaded += this.GameControl_Loaded;
            this.DataContext = this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuControl"/> class.
        /// </summary>
        /// <param name="factory">Factory.</param>
        public MenuControl(Factory factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// Gets or sets current map.
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether page about to close.
        /// </summary>
        public bool Exit
        {
            get { return this.exit; }
            set { this.exit = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether scrolling enabled.
        /// </summary>
        public bool ScrollMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether title enabled.
        /// </summary>
        public bool TitleEnabled { get; set; }

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
            this.initLogic = new InitLogic(this.model, this.MapName, this.factory.ViewerLogic, false);
            this.model.Camera = new Viewport((int)this.ActualWidth, (int)this.ActualHeight, (int)this.model.CurrentWorld.GameWidth, (int)this.model.CurrentWorld.GameHeight, this.model.Hero);
            this.renderer = new GameRenderer(this.model, true, this.ScrollMode, this.TitleEnabled);
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
            this.renderer.WindowChanged = true;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (!this.exit)
            {
                this.characterLogic.OneTick();
                this.InvalidateVisual();
            }
            else
            {
                this.win.SizeChanged -= this.Win_SizeChanged;
                this.win.MouseMove -= this.Win_MouseMove;
                CompositionTarget.Rendering -= this.CompositionTarget_Rendering;
            }
        }
    }
}
