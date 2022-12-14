// <copyright file="Character.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using TimeWar.Model.Objects.Classes;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Basic character information class.
    /// </summary>
    public abstract class Character : IMoveable, IGameObject
    {
        private List<string> keys;

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="pos">Character position.</param>
        /// <param name="health">Base health.</param>
        /// <param name="height">Character height.</param>
        /// <param name="width">Character width.</param>
        /// <param name="spriteFile">Name of the sprite file.</param>
        protected Character(Point pos, int health, int height, int width, string spriteFile)
        {
            this.Position = pos;
            this.Health = health;
            this.CurrentHealth = health;
            this.Height = height;
            this.Width = width;
            this.SpriteFile = spriteFile;
            this.CurrentSprite = 0;
            this.Stance = Stances.StandRight;
            this.StanceLess = false;
            this.keys = new List<string>();
            this.CurrentSprite = 0;
            this.ShieldRegenTime = 7000;
            this.ShieldRegenTimer = new Stopwatch();
            this.ShieldRegenValue = 1;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the character is invincible or not.
        /// </summary>
        public bool IsInvincible { get; set; }

        /// <summary>
        /// Gets or sets mouse click location.
        /// </summary>
        public Point ClickLocation { get; set; }

        /// <summary>
        /// Gets or sets current character health.
        /// </summary>
        public int CurrentHealth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player can attack or not.
        /// </summary>
        public bool CanAttack { get; set; }

        /// <summary>
        /// Gets or sets character height in pixel.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets character width in pixel.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the character sprite file name.
        /// </summary>
        public string SpriteFile { get; set; }

        /// <summary>
        /// Gets or sets current sprite frame.
        /// </summary>
        public int CurrentSprite { get; set; }

        /// <summary>
        /// Gets or sets moving direction.
        /// </summary>
        public Stances Stance { get; set; }

        /// <inheritdoc/>
        public Point Position { get; set; }

        /// <summary>
        /// Gets or sets the character health.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets the character shield.
        /// </summary>
        public int Shield { get; set; }

        /// <summary>
        /// Gets or sets the character current shield.
        /// </summary>
        public int CurrentShield { get; set; }

        /// <summary>
        /// Gets or sets shield regen amount.
        /// </summary>
        public int ShieldRegenValue { get; set; }

        /// <inheritdoc/>
        public bool StanceLess { get; set; }

        /// <inheritdoc/>
        public Point MovementVector { get; set; }

        /// <summary>
        /// Gets or sets type of bullet.
        /// </summary>
        public BulletType TypeOfBullet { get; set; }

        /// <summary>
        /// Gets or sets shield regen time.
        /// </summary>
        public int ShieldRegenTime { get; set; }

        /// <summary>
        /// Gets or sets shield regen timer stopwatch.
        /// </summary>
        public Stopwatch ShieldRegenTimer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether player can jump.
        /// </summary>
        public bool CanJump { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            string retString = string.Empty;
            retString += this.Position.X + ";"; // Character position X.
            retString += this.Position.Y + ";"; // Character position Y.
            retString += this.CurrentHealth + ";"; // Character health.
            retString += this.CurrentShield + ";"; // Character shield.
            retString += this.TypeOfBullet + ";"; // Type of bullet.
            retString += (this as Player).NumOfWeaponUnlocked + ";"; // Character num of weaponst unlocked.
            return retString;
        }

        /// <summary>
        /// Add new key to the pressed list.
        /// </summary>
        /// <param name="key">Pressed key.</param>
        public void AddKey(string key)
        {
            if (!this.keys.Contains(key))
            {
                this.keys.Add(key);
            }
        }

        /// <summary>
        /// Remove key from the preesed list.
        /// </summary>
        /// <param name="key">Released key.</param>
        public void RemoveKey(string key)
        {
            this.keys.Remove(key);
        }

        /// <summary>
        /// Check key in the list.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <returns>True if contains.</returns>
        public bool ContainKey(string key)
        {
            return this.keys.Contains(key);
        }
    }
}
