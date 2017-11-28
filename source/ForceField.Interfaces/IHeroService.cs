using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Domain.GameLogic;
using Microsoft.Xna.Framework;

namespace ForceField.Interfaces
{
    public interface IHeroService : IBattleUnitService
    {
        Hero PlayerHero
        {
            get;
        }

        Hero AddHero();
        void AddHero(Hero hero);

        Hero CreateDefaultHero(Point location, string name, string spriteName, Color color, TileMap map);
        Hero CreateHero();

        Hero CreateTestBattlePlayerHero();
        Hero CreateTestBattleEnemyHero();
        
        void AddCurrentHero(Hero hero);

        void ClearCurrentHeroes();
       
        void SetGameplayServices();

        void UpdateInGlobalMapMode(GameTime gameTime);
        void UpdateInBattleMode(GameTime gameTime);
    }
}
