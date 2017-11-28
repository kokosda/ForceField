using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ForceField.Interfaces;
using ForceField.Domain.GameLogic;

namespace ForceField.Core.Managers.Services
{
    public class BattleService : IBattleService
    {
        public BattleService(Game game)
        {
            tileMapService = game.Services.GetService(typeof(ITileMapService)) as ITileMapService;
            heroService = game.Services.GetService(typeof(IHeroService)) as IHeroService;

            currentBattle = null;
        }

        #region Properties

        public Battle CurrentBattle
        {
            get
            {
                return currentBattle;
            }
        }

        #endregion

        public void StartBattle(Hero offensiveHero, Hero defendingHero)
        {
            
        }

        public void StartBattle(Battle battle)
        {
            currentBattle = battle;
        }

        public Battle CreateTestBattle()
        {
            Battle battle = new Battle();

            battle.PlayerHero = heroService.CreateTestBattlePlayerHero();
            battle.EnemyHero = heroService.CreateTestBattleEnemyHero();
            battle.IsPlayerOffensive = true;
            battle.TurnNumber = 0;
            battle.CurrentPhase = BattlePhase.Initialize;
            battle.Ground = tileMapService.Units.First(p => p.Name == "Default_battle_tilemap");
            battle.IsInitialized = true;

            return battle;
        }

        public void Update()
        {
            // todo
        }

        #region private

        private ITileMapService tileMapService;
        private IHeroService heroService;

        private Battle currentBattle;

        #endregion
    }
}
