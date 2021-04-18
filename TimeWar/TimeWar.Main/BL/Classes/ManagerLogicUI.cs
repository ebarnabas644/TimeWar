// <copyright file="ManagerLogicUI.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.BL.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight.Messaging;
    using TimeWar.Main.BL.Interfaces;
    using TimeWar.Main.Data;

    /// <summary>
    /// Manager logic ui class.
    /// </summary>
    internal class ManagerLogicUI : IManagerLogicUI
    {
        private Factory factory;
        private IMessenger messengerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerLogicUI"/> class.
        /// </summary>
        /// <param name="factory">Factory instance.</param>
        /// <param name="messenger">Messenger instance.</param>
        public ManagerLogicUI(Factory factory, IMessenger messenger)
        {
            this.factory = factory;
            this.messengerService = messenger;
        }

        /// <inheritdoc/>
        public void CreateMap(MapRecordUI newMap)
        {
            try
            {
                this.factory.ManagerLogic.CreateMap(MapRecordUI.ConvertToMapEntity(newMap));
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                this.messengerService.Send("Add failed, bad id or input", "LogicResult");
                return;
            }

            this.messengerService.Send("Add successfull", "LogicResult");
        }

        /// <inheritdoc/>
        public void CreateProfile(IList<PlayerProfileUI> profileUIs, PlayerProfileUI newProfile)
        {
            try
            {
                this.factory.ManagerLogic.CreateProfile(PlayerProfileUI.ConvertToProfileEntity(newProfile));
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                this.messengerService.Send("Add failed, bad id or input", "LogicResult");
                return;
            }

            profileUIs.Add(newProfile);
            this.messengerService.Send("Add successfull", "LogicResult");
        }

        /// <inheritdoc/>
        public void CreateSave(SaveUI newSave)
        {
            try
            {
                this.factory.ManagerLogic.CreateSave(SaveUI.ConvertToSaveEntity(newSave));
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                this.messengerService.Send("Add failed, bad id or input", "LogicResult");
                return;
            }

            this.messengerService.Send("Add successfull", "LogicResult");
        }

        /// <inheritdoc/>
        public void DeleteMap(IList<MapRecordUI> mapUIs, MapRecordUI map)
        {
            if (map != null && mapUIs.Remove(map))
            {
                this.factory.ManagerLogic.DeleteMap(MapRecordUI.ConvertToMapEntity(map));
                this.messengerService.Send("Delete successfull", "LogicResult");
            }
            else
            {
                this.messengerService.Send("Delete failed", "LogicResult");
            }
        }

        /// <inheritdoc/>
        public void DeleteProfile(IList<PlayerProfileUI> profileUIs, PlayerProfileUI profile)
        {
            if (profile != null && profileUIs.Remove(profile))
            {
                this.factory.ManagerLogic.DeleteProfile(PlayerProfileUI.ConvertToProfileEntity(profile));
                this.messengerService.Send("Delete successfull", "LogicResult");
            }
            else
            {
                this.messengerService.Send("Delete failed", "LogicResult");
            }
        }

        /// <inheritdoc/>
        public void DeleteSave(IList<SaveUI> saveUIs, SaveUI save)
        {
            if (save != null && saveUIs.Remove(save))
            {
                this.factory.ManagerLogic.DeleteSave(SaveUI.ConvertToSaveEntity(save));
                this.messengerService.Send("Delete successfull", "LogicResult");
            }
            else
            {
                this.messengerService.Send("Delete failed", "LogicResult");
            }
        }

        /// <inheritdoc/>
        public void ModifyMap(MapRecordUI newMap)
        {
            if (newMap == null)
            {
                this.messengerService.Send("Modify failed", "LogicResult");
                return;
            }
            else
            {
                try
                {
                    this.factory.ManagerLogic.ModifyMap(MapRecordUI.ConvertToMapEntity(newMap));
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                {
                    this.messengerService.Send("Modify failed, bad id or input", "LogicResult");
                    return;
                }

                this.messengerService.Send("Modify successfull", "LogicResult");
                }
            }

        /// <inheritdoc/>
        public void ModifyProfile(PlayerProfileUI newProfile)
        {
            if (newProfile == null)
            {
                this.messengerService.Send("Modify failed", "LogicResult");
                return;
            }
            else
            {
                PlayerProfileUI copy = new PlayerProfileUI();
                copy.CopyFrom(newProfile);
                try
                {
                    this.factory.ManagerLogic.ModifyProfile(PlayerProfileUI.ConvertToProfileEntity(copy));
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                {
                    this.messengerService.Send("Modify failed, bad id or input", "LogicResult");
                    return;
                }

                newProfile.CopyFrom(copy);
                this.messengerService.Send("Modify successfull", "LogicResult");
            }
        }

        /// <inheritdoc/>
        public void ModifySave(SaveUI newSave)
        {
            if (newSave == null)
            {
                this.messengerService.Send("Modify failed", "LogicResult");
                return;
            }
            else
            {
                try
                {
                    this.factory.ManagerLogic.ModifySave(SaveUI.ConvertToSaveEntity(newSave));
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                {
                    this.messengerService.Send("Modify failed, bad id or input", "LogicResult");
                    return;
                }
            }
        }
    }
}
