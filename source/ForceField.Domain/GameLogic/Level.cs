using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForceField.Domain.Renderer;

namespace ForceField.Domain.GameLogic
{
    [Serializable]
    public class Level : Entity
    {
        #region Properties

        public List<NonPlayerCharacter> NonPlayerCharacters
        {
            get;
            set;
        }

        public List<Block> Blocks
        {
            get;
            set;
        }

        public PlayerCharacter Player
        {
            get;
            set;
        }

        public List<Background> Backgrounds { get; set; }

        public List<Emitter> Emitters { get; set; }

        public List<Building> Buildings { get; set; }

        [NonSerialized]
        public IList<Level> NearLevels;

        #endregion
    }
}
