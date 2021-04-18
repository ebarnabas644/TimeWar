// <copyright file="PlayerProfileUI.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.Data
{
    using System.Linq;
    using GalaSoft.MvvmLight;
    using TimeWar.Data.Models;

    /// <summary>
    /// Profile ui data class.
    /// </summary>
    public class PlayerProfileUI : ObservableObject
    {
        private int playerId;
        private string playerName;
        private int totalKills;
        private int totalDeaths;
        private int completedRuns;
        private bool selected;
        private int saveId;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerProfileUI"/> class.
        /// </summary>
        public PlayerProfileUI()
        {
        }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        public int PlayerId
        {
            get { return this.playerId; }
            set { this.Set(ref this.playerId, value); }
        }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public string PlayerName
        {
            get { return this.playerName; }
            set { this.Set(ref this.playerName, value); }
        }

        /// <summary>
        /// Gets or sets the total number of kills.
        /// </summary>
        public int TotalKills
        {
            get { return this.totalKills; }
            set { this.Set(ref this.totalKills, value); }
        }

        /// <summary>
        /// Gets or sets the total number of deaths.
        /// </summary>
        public int TotalDeaths
        {
            get { return this.totalDeaths; }
            set { this.Set(ref this.totalDeaths, value); }
        }

        /// <summary>
        /// Gets or sets the number of completed runs.
        /// </summary>
        public int CompletedRuns
        {
            get { return this.completedRuns; }
            set { this.Set(ref this.completedRuns, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether selected.
        /// </summary>
        public bool Selected
        {
            get { return this.selected; }
            set { this.Set(ref this.selected, value); }
        }

        /// <summary>
        /// Gets or sets the save id.
        /// </summary>
        public int SaveId
        {
            get { return this.saveId; }
            set { this.Set(ref this.saveId, value); }
        }

        /// <summary>
        /// Convert profileui entity to database entity.
        /// </summary>
        /// <param name="profileui">Profile ui entity.</param>
        /// <returns>Profile entity.</returns>
        public static PlayerProfile ConvertToProfileEntity(PlayerProfileUI profileui)
        {
            PlayerProfile profile = null;
            if (profileui != null)
            {
                profile = new PlayerProfile();
                profile.PlayerId = profileui.PlayerId;
                profile.Selected = profileui.Selected;
                profile.PlayerName = profileui.PlayerName;
                profile.SaveId = profileui.SaveId;
                profile.TotalKills = profileui.TotalKills;
                profile.TotalDeaths = profileui.TotalDeaths;
                profile.CompletedRuns = profileui.CompletedRuns;
            }

            return profile;
        }

        /// <summary>
        /// Convert profile entity to ui entity.
        /// </summary>
        /// <param name="profile">Profile entity.</param>
        /// <returns>Profile ui entity.</returns>
        public static PlayerProfileUI ConvertToProfileUiEntity(PlayerProfile profile)
        {
            PlayerProfileUI profileui = null;
            if (profile != null)
            {
                profileui = new PlayerProfileUI();
                profileui.PlayerId = profile.PlayerId;
                profileui.Selected = profile.Selected;
                profileui.PlayerName = profile.PlayerName;
                profileui.SaveId = profile.SaveId;
                profileui.TotalKills = profile.TotalKills;
                profileui.TotalDeaths = profile.TotalDeaths;
                profileui.CompletedRuns = profile.CompletedRuns;
            }

            return profileui;
        }

        /// <summary>
        /// Copy data from another Profile element.
        /// </summary>
        /// <param name="other">Data source.</param>
        public void CopyFrom(PlayerProfileUI other)
        {
            this.GetType().GetProperties().ToList().ForEach(x => x.SetValue(this, x.GetValue(other)));
        }
    }
}
