using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain.GameLogic
{
    public class Battle : Entity
    {
        public Battle()
        {
            IsInitialized = false;

            PlayerCreatures = new List<Creature>();
            EnemyCreatures = new List<Creature>();
        }

        #region Properties

        public bool IsInitialized { get; set; }

        public TileMap Ground { get; set; }

        public Hero PlayerHero { get; set; }

        public Hero EnemyHero { get; set; }

        public List<Creature> PlayerCreatures { get; set; }

        public List<Creature> EnemyCreatures { get; set; }

        public int TurnNumber { get; set; }

        public bool PlayerHeroTurn { get; set; }

        public BattlePhase CurrentPhase { get; set; }

        public bool IsPlayerOffensive { get; set; }

        #endregion
    }
}
