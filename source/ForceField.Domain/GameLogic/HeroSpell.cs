using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.Domain.GameLogic
{
    public class HeroSpell : Perfomance
    {
        public HeroSpell()
        {
        }

        #region Properties

        public int ManaCost { get; set; }       // боевая мана-стоимость заклинания

        public int LearnCost { get; set; }      // стоимость изучения (помещения в слот)

        public ColorDescription Color { get; set; }  // цвет заклинания

        public int Tier { get; set; }           // ранг (круг) заклинания

        public int Level { get; set; }          // уровень заклинания

        public HeroSpellType Type { get; set; }     // тип заклинания

        #endregion
    }
}
