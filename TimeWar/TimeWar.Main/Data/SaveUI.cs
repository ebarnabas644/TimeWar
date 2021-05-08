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
        private string playerdata;
        private string enemydata;
        private string poidata;

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
        public string Playerdata
        {
            get { return this.playerdata; }
            set { this.Set(ref this.playerdata, value); }
        }

        /// <summary>
        /// Gets or sets checkpoint.
        /// </summary>
        public string Enemydata
        {
            get { return this.enemydata; }
            set { this.Set(ref this.enemydata, value); }
        }

        /// <summary>
        /// Gets or sets playerid.
        /// </summary>
        public string Poidata
        {
            get { return this.poidata; }
            set { this.Set(ref this.poidata, value); }
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
                save.Enemydata = saveui.Enemydata;
                save.Playerdata = saveui.Playerdata;
                save.Poidata = saveui.Poidata;
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
                saveui.Enemydata = save.Enemydata;
                saveui.Playerdata = save.Playerdata;
                saveui.Poidata = save.Poidata;
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
