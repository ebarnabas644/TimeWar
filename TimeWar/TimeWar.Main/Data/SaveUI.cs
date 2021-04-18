// <copyright file="SaveUI.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.Data
{
    using System.Linq;
    using GalaSoft.MvvmLight;
    using TimeWar.Data.Models;

    /// <summary>
    /// Save ui data class.
    /// </summary>
    public class SaveUI : ObservableObject
    {
        private int id;
        private int point;
        private int checkpoint;
        private int playerId;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveUI"/> class.
        /// </summary>
        public SaveUI()
        {
        }

        /// <summary>
        /// Gets or sets save id.
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.Set(ref this.id, value); }
        }

        /// <summary>
        /// Gets or sets point.
        /// </summary>
        public int Point
        {
            get { return this.point; }
            set { this.Set(ref this.point, value); }
        }

        /// <summary>
        /// Gets or sets checkpoint.
        /// </summary>
        public int Checkpoint
        {
            get { return this.checkpoint; }
            set { this.Set(ref this.checkpoint, value); }
        }

        /// <summary>
        /// Gets or sets playerid.
        /// </summary>
        public int PlayerId
        {
            get { return this.playerId; }
            set { this.Set(ref this.playerId, value); }
        }

        /// <summary>
        /// Convert saveui entity to database entity.
        /// </summary>
        /// <param name="saveui">Save ui entity.</param>
        /// <returns>Save entity.</returns>
        public static Save ConvertToSaveEntity(SaveUI saveui)
        {
            Save save = new Save();
            if (saveui != null)
            {
                save.Id = saveui.Id;
                save.PlayerId = saveui.PlayerId;
                save.Point = saveui.Point;
                save.Checkpoint = saveui.Checkpoint;
            }

            return save;
        }

        /// <summary>
        /// Convert save entity to ui entity.
        /// </summary>
        /// <param name="save">Save entity.</param>
        /// <returns>Save ui entity.</returns>
        public static SaveUI ConvertToSaveUiEntity(Save save)
        {
            SaveUI saveui = new SaveUI();
            if (save != null)
            {
                saveui.Id = save.Id;
                saveui.PlayerId = save.PlayerId;
                saveui.Point = save.Point;
                saveui.Checkpoint = save.Checkpoint;
            }

            return saveui;
        }

        /// <summary>
        /// Copy data from another Save element.
        /// </summary>
        /// <param name="other">Data source.</param>
        public void CopyFrom(SaveUI other)
        {
            this.GetType().GetProperties().ToList().ForEach(x => x.SetValue(this, x.GetValue(other)));
        }
    }
}
