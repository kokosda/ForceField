using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForceField.Domain.GameLogic;
using ForceField.Interfaces;
using ForceField.Core.Services;

namespace ForceField.Core.Managers.Services
{
    public class BattleUnitService : UnitsService<BattleUnit>, IBattleUnitService
    {
        public BattleUnitService(IList<BattleUnit> units, UnitsManager unitManager) : base(units, unitManager)
        {
        }

        #region IUnitsService<BattleUnit>

        public void CreateUnitsFromServiceByResourcePath(string templatePath)
        {
            throw new NotImplementedException();
        }

        public Unit GetRandom()
        {
            throw new NotImplementedException();
        }

        public Texture2D GetFirstTexture(BattleUnit unit)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime time)
        {
            base.Update(time);
        }

        public void Draw(GameTime time)
        {
            base.Draw(time);
        }

        public void SetSpriteService(ISpriteService spriteService)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBattleUnitService

        public BattleUnit GetBattleUnitById(string creatureId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
