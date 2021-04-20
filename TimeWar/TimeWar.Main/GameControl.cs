// <copyright file="GameControl.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using TimeWar.Logic;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Renderer;

    /// <summary>
    /// Game controlling class.
    /// </summary>
    public class GameControl : FrameworkElement
    {
        private GameModel model;
        private InitLogic initLogic;
        private GameRenderer renderer;
        private Logic.Classes.CommandManager commandManager;
        private CharacterLogic characterLogic;
        private Window win;
        private Stopwatch time = new Stopwatch();
        private int fps;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameControl"/> class.
        /// </summary>
        public GameControl()
        {
            this.Loaded += this.GameControl_Loaded;
        }

        /// <summary>
        /// Gets or sets current map.
        /// </summary>
        public string MapName { get; set; }

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
            this.initLogic = new InitLogic(this.model, this.MapName);
            this.model.Camera = new Viewport((int)this.ActualWidth, (int)this.ActualHeight, (int)this.model.CurrentWorld.GameWidth, (int)this.model.CurrentWorld.GameHeight, this.model.Hero);
            this.renderer = new GameRenderer(this.model, false);
            this.commandManager = new Logic.Classes.CommandManager();
            this.characterLogic = new CharacterLogic(this.model, this.model.Hero, this.commandManager);
            this.time.Start();
            this.fps = 0;
            this.win = Window.GetWindow(this);
            if (this.win != null)
            {
                this.win.KeyDown += this.Win_KeyDown;
                this.win.KeyUp += this.Win_KeyUp;
                this.win.SizeChanged += this.Win_SizeChanged;
                this.win.MouseMove += this.Win_MouseMove;
                this.win.MouseDown += this.Win_MouseDown;
                CompositionTarget.Rendering += this.CompositionTarget_Rendering;
            }

            this.InvalidateVisual();
        }

        private void Win_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        private void Win_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                System.Windows.Point clickPosition = e.GetPosition(this);
                System.Drawing.Point clickRelativePos = new System.Drawing.Point(this.model.Camera.GetRelativeCharacterPosX, this.model.Camera.GetRelativeCharacterPosY);
                clickRelativePos.X -= (int)clickPosition.X;
                clickRelativePos.X *= -1;
                clickRelativePos.Y -= (int)clickPosition.Y;
                clickRelativePos.X += this.model.Hero.Position.X;
                clickRelativePos.Y -= this.model.Hero.Position.Y;
                clickRelativePos.Y *= -1;
                this.model.Hero.CanAttack = true;
                this.model.Hero.ClickLocation = clickRelativePos;
            }
        }

        private void Win_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.model.Camera.WindowHeight = (int)this.ActualHeight;
            this.model.Camera.WindowWidth = (int)this.ActualWidth;
            this.renderer.WindowChanged = true;
        }

        private void Win_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    if (this.commandManager.IsFinished)
                    {
                        this.model.Hero.RemoveKey("space");
                    }

                    break;
                case Key.A:
                    if (this.commandManager.IsFinished)
                    {
                        this.model.Hero.RemoveKey("a");
                    }

                    break;
                case Key.S:
                    if (this.commandManager.IsFinished)
                    {
                        this.model.Hero.RemoveKey("s");
                    }

                    break;
                case Key.D:
                    if (this.commandManager.IsFinished)
                    {
                        this.model.Hero.RemoveKey("d");
                    }

                    break;
                case Key.E:
                    this.commandManager.Rewind().Start();
                    break;
            }

            e.Handled = true;
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    if (this.commandManager.IsFinished)
                    {
                        this.model.Hero.AddKey("space");
                    }

                    break;
                case Key.A:
                    if (this.commandManager.IsFinished)
                    {
                        this.model.Hero.AddKey("a");
                    }

                    break;
                case Key.S:
                    if (this.commandManager.IsFinished)
                    {
                        this.model.Hero.AddKey("s");
                    }

                    break;
                case Key.D:
                    if (this.commandManager.IsFinished)
                    {
                        this.model.Hero.AddKey("d");
                    }

                    break;
                case Key.E:
                    this.commandManager.Rewind().Start();
                    break;
            }

            e.Handled = true;
            this.InvalidateVisual();
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            this.characterLogic.OneTick();
            this.InvalidateVisual();
            if (this.time.Elapsed.TotalSeconds >= 1)
            {
                this.time.Stop();
                this.time.Reset();
                this.win.Title = this.fps.ToString(System.Globalization.CultureInfo.CurrentCulture) + " FPS";
                this.fps = 0;
                this.time.Start();
            }

            this.fps++;
        }
    }
}
