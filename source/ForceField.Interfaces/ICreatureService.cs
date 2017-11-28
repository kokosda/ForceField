using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.Domain.GameLogic;

namespace ForceField.Interfaces
{
    public interface ICreatureService : IBattleUnitService
    {
        void AddCreature(Creature creature);

        void DeleteCreature(Creature creature);
        void DeleteCreature(string creatureId);

        void DeleteAllCreatures();

        Creature GetCreature(string creatureId);
    }
}
