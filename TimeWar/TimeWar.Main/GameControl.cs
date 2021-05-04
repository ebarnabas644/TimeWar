// <copyright file="GameControl.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using TimeWar.Logic;
    using TimeWar.Logic.Classes.Characters;
    using TimeWar.Logic.Classes.Characters.Actions;
    using TimeWar.Logic.Classes.LogicCollections;
    using TimeWar.Main.BL;
    using TimeWar.Main.ViewModel;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;
    using TimeWar.Renderer;

    /// <summary>
    /// Game controlling class.
    /// </summary>
    internal class GameControl : FrameworkElement, IDisposable
    {
        private GameModel model;
        private InitLogic initLogic;
        private GameRenderer renderer;
        private Logic.Classes.CommandManager commandManager;
        private CharacterLogic characterLogic;
        private BulletLogics bulletLogic;
        private Timer timer;
        private EnemyLogics enemyLogic;
        private PointOfInterestLogics pointOfInterestLogics;

        // private Factory factory;
        private Window win;
        private Stopwatch time = new Stopwatch();
        private Stopwatch deltatime = new Stopwatch();
        private ushort mouseScrollPos;
        private bool exit;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameControl"/> class.
        /// </summary>
        public GameControl()
        {
            this.exit = false;
            this.Loaded += this.GameControl_Loaded;
            this.mouseScrollPos = 1;
        }

        /// <summary>
        /// Gets or sets current map.
        /// </summary>
        public string MapName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether true if game is paused.
        /// </summary>
        public bool IsPaused { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether page about to close.
        /// </summary>
        public bool Exit
        {
            get { return this.exit; }
            set { this.exit = value; }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.timer.Dispose();
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
            // this.factory = new Factory();
            this.model = new GameModel();
            this.initLogic = new InitLogic(this.model, this.MapName);
            this.model.Camera = new Viewport((int)this.ActualWidth, (int)this.ActualHeight, (int)this.model.CurrentWorld.GameWidth, (int)this.model.CurrentWorld.GameHeight, this.model.Hero);
            this.renderer = new GameRenderer(this.model, false);
            this.commandManager = new Logic.Classes.CommandManager();
            this.characterLogic = new CharacterLogic(this.model, this.model.Hero, this.commandManager);
            this.bulletLogic = new BulletLogics(this.model, (ICollection<Bullet>)this.model.CurrentWorld.GetBullets, this.commandManager);
            this.enemyLogic = new EnemyLogics(this.model, this.commandManager);
            this.pointOfInterestLogics = new PointOfInterestLogics(this.model, this.characterLogic, this.commandManager);
            this.mouseScrollPos = 0;
            this.time.Start();

            this.deltatime.Start();

            this.model.CurrentWorld.SaveEnemies();
            this.win = Window.GetWindow(this);
            if (this.win != null)
            {
                this.win.KeyDown += this.Win_KeyDown;
                this.win.KeyUp += this.Win_KeyUp;
                this.win.SizeChanged += this.Win_SizeChanged;
                this.win.MouseMove += this.Win_MouseMove;
                this.win.MouseDown += this.Win_MouseDown;
                this.win.MouseWheel += this.Win_MouseScroll;
                this.timer = new Timer(this.Timer_Elapsed, null, 0, 16);
                CompositionTarget.Rendering += (sender, args) => this.InvalidateVisual();
            }

            this.InvalidateVisual();
        }

        private void Timer_Elapsed(object stateInfo)
        {
            this.Tick();
        }

        private void Tick()
        {
            if (!this.Exit)
            {
                if (!this.IsPaused)
                {
                    if (this.model.CurrentWorld.EnemiesLoaded)
                    {
                        this.model.CurrentWorld.EnemiesLoaded = false;
                        this.enemyLogic.GetEnemies();
                    }

                    this.characterLogic.OneTick();
                    this.enemyLogic.TickEnemies();
                    this.pointOfInterestLogics.TickPois();
                    this.bulletLogic.Addbullets((ICollection<Bullet>)this.model.CurrentWorld.GetBullets);
                    this.bulletLogic.OneTick();
                }
            }
            else
            {
                this.win.KeyDown -= this.Win_KeyDown;
                this.win.KeyUp -= this.Win_KeyUp;
                this.win.SizeChanged -= this.Win_SizeChanged;
                this.win.MouseMove -= this.Win_MouseMove;
                this.win.MouseDown -= this.Win_MouseDown;
                this.win.MouseWheel -= this.Win_MouseScroll;
                this.Dispose();
                CompositionTarget.Rendering -= (sender, args) => this.InvalidateVisual();
            }
        }

        private void Win_MouseScroll(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.mouseScrollPos++;
            }
            else if (e.Delta < 0)
            {
                this.mouseScrollPos++;
            }

            switch (this.mouseScrollPos % this.model.Hero.NumOfWeaponUnlocked)
            {
                case 0:
                    this.model.Hero.TypeOfBullet = BulletType.Basic;
                    break;

                case 1:
                    this.model.Hero.TypeOfBullet = BulletType.Accelerating;
                    break;

                case 2:
                    this.model.Hero.TypeOfBullet = BulletType.Bouncing;
                    break;

                case 3:
                    this.model.Hero.TypeOfBullet = BulletType.CurvedBouncing;
                    break;

                default:
                    this.model.Hero.TypeOfBullet = BulletType.Basic;
                    break;
            }
        }

        private void Win_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        private void Win_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !this.IsPaused)
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
            if (!this.IsPaused)
            {
                switch (e.Key)
                {
                    case Key.Space:
                        this.model.Hero.RemoveKey("space");
                        break;

                    case Key.A:
                        this.model.Hero.RemoveKey("a");
                        break;

                    case Key.S:
                        this.model.Hero.RemoveKey("s");

                        break;
                    case Key.D:
                        this.model.Hero.RemoveKey("d");

                        break;
                    case Key.Escape:
                        GameViewModel asd = this.DataContext as GameViewModel;
                        asd.MenuVisibility = !asd.MenuVisibility;
                        this.IsPaused = !this.IsPaused;
                        break;
                }
            }
            else
            {
                if (e.Key == Key.Escape)
                {
                    GameViewModel asd = this.DataContext as GameViewModel;
                    asd.MenuVisibility = !asd.MenuVisibility;
                    this.IsPaused = !this.IsPaused;
                }
            }

            e.Handled = true;
        }

        private void Win_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (this.commandManager.IsFinished)
            {
                switch (e.Key)
                {
                    case Key.Space:
                        this.model.Hero.AddKey("space");
                        break;
                    case Key.A:
                        this.model.Hero.AddKey("a");
                        break;
                    case Key.S:
                        this.model.Hero.AddKey("s");
                        break;
                    case Key.D:
                        this.model.Hero.AddKey("d");

                        break;
                    case Key.E:
                        this.commandManager.Rewind(this.renderer.MovingObjectsCount).Start();
                        break;
                }

                e.Handled = true;
            }
        }
    }
}
