using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForceField.Interfaces;
using ForceField.Domain.GameLogic;

namespace ForceField.Core.Managers.Services
{
    public class CreatureService : BattleUnitService, ICreatureService
    {
        public CreatureService(IList<BattleUnit> creatureList, UnitsManager manager) : base(creatureList,manager)
        {
            spriteBatchService = manager.Game.Services.GetService(typeof(ISpriteBatchService)) as ISpriteBatchService;
            spriteService = manager.Game.Services.GetService(typeof(ISpriteService)) as ISpriteService;
            animationService = manager.Game.Services.GetService(typeof(IAnimationService)) as IAnimationService;
            unitsManager = manager;
        }

        public void SetSpriteService(ISpriteService spriteService)
        {
            if (spriteBatchService != null)
            {
                this.spriteService = spriteService;
            }
        }

        public void CreateUnitsFromServiceByResourcePath(string templatePath)
        {
        }

        public Unit GetRandom()
        {
            throw new NotImplementedException();

            // todo
        }

        public Texture2D GetFirstTexture(Creature unit)
        {
            throw new NotImplementedException();

            // todo
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Creature creature in Units)
            {
                UpdateCreature(creature, gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void AddCreature(Creature creature)
        {
            Units.Add(creature);
        }

        public void DeleteCreature(Creature creature)
        {
            for (int i = 0; i < Units.Count; i++)
            {
                if (Units[i] == creature)
                {
                    Units.RemoveAt(i);
                }
            }
        }

        public void DeleteCreature(string creatureId)
        {
            // todo
        }

        public void DeleteAllCreatures()
        {
            Units.Clear();
        }

        public Creature GetCreature(string creatureId)
        {
            throw new NotImplementedException();

            // todo
        }

        #region private

        private void UpdateCreature(Creature creature, GameTime gameTime)
        {

        }
        #region fields

        ISpriteBatchService spriteBatchService;
        ISpriteService spriteService;
        IAnimationService animationService;
        UnitsManager unitsManager;

        #endregion

        #endregion
    }
}
