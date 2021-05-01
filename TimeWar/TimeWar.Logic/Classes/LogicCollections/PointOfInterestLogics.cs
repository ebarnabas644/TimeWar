// <copyright file="PointOfInterestLogics.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.LogicCollections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Logic.Classes.POIs;
    using TimeWar.Logic.Interfaces;
    using TimeWar.Model;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Collection of POIs.
    /// </summary>
    public class PointOfInterestLogics
    {
        private const int TickDistance = 8;
        private GameModel model;
        private List<PointOfInterestLogic> pois;
        private CharacterLogic character;
        private CommandManager commandManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointOfInterestLogics"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="character">Character.</param>
        /// <param name="commandManager">Command manager.</param>
        public PointOfInterestLogics(GameModel model, CharacterLogic character, CommandManager commandManager)
        {
            this.model = model;
            this.character = character;
            this.pois = new List<PointOfInterestLogic>();
            this.commandManager = commandManager;
            this.GetPOIs();
        }

        /// <summary>
        /// Tick Pois.
        /// </summary>
        public void TickPois()
        {
            if (this.commandManager.IsFinished)
            {
                for (int i = 0; i < this.pois.Count; i++)
                {
                    if (this.DistanceFromPlayer(this.pois[i]) < TickDistance)
                    {
                        this.pois[i].OneTick();
                    }

                    this.Despawn(this.pois[i]);
                }
            }
        }

        /// <summary>
        /// Gets pois.
        /// </summary>
        public void GetPOIs()
        {
            foreach (PointOfInterest poi in this.model.CurrentWorld.GetPois)
            {
                switch (poi.Type)
                {
                    case POIType.Checkpoint:
                        CheckpointLogic checkpoint = new CheckpointLogic(this.model, poi);
                        this.pois.Add(checkpoint);
                        break;
                    case POIType.Finish:
                        FinishLogic finish = new FinishLogic(this.model, poi);
                        this.pois.Add(finish);
                        break;
                    case POIType.HealthKit:
                        HealthKitLogic hp = new HealthKitLogic(this.model, poi);
                        this.pois.Add(hp);
                        break;
                    case POIType.HighJump:
                        HighJumpLogic jump = new HighJumpLogic(this.model, poi, this.character);
                        this.pois.Add(jump);
                        break;
                    case POIType.UnlockWeapon:
                        UnlockWeaponLogic weapon = new UnlockWeaponLogic(this.model, poi);
                        this.pois.Add(weapon);
                        break;
                    case POIType.Invincibility:
                        InvincibilityLogic inv = new InvincibilityLogic(this.model, poi, this.character);
                        this.pois.Add(inv);
                        break;
                    case POIType.RapidFire:
                        RapidFireLogic rapid = new RapidFireLogic(this.model, poi, this.character);
                        this.pois.Add(rapid);
                        break;
                    default:
                        break;
                }
            }
        }

        private void Despawn(PointOfInterestLogic poi)
        {
            if (poi.IsPlayerContacted && poi is not ITimedEvent)
            {
                this.model.CurrentWorld.RemovePOI(poi.Poi);
                this.pois.Remove(poi);
            }
            else
            {
                if (poi.IsPlayerContacted)
                {
                    this.model.CurrentWorld.RemovePOI(poi.Poi);
                }

                if (poi is TimedPOILogic && this.character.EffectStopwatch.IsRunning)
                {
                    if ((poi as TimedPOILogic).CheckTimer())
                    {
                        (poi as TimedPOILogic).ResetStats();
                        this.pois.Remove(poi);
                    }
                }
            }
        }

        private int DistanceFromPlayer(PointOfInterestLogic poi)
        {
            return (int)Math.Sqrt(Math.Pow(poi.Poi.Position.X - this.model.CurrentWorld.ConvertPixelToTile(this.model.Hero.Position.X), 2) + Math.Pow(poi.Poi.Position.Y - this.model.CurrentWorld.ConvertPixelToTile(this.model.Hero.Position.Y), 2));
        }
    }
}
