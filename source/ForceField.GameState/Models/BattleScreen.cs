using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForceField.GameState.Models.Base;
using ForceField.GameState.Services;
using ForceField.UI.UIElements;
using ForceField.UI.UIComponentServices;

namespace ForceField.GameState.Models
{
    public class BattleScreen : GameplayScreen
    {
        public BattleScreen(BattleScreenServiceImpl service) :
            base(service)
        {

        }

        #region Properties

        public UILabelService LabelService { get; set; }

        public UIButtonService ButtonService { get; set; }

        public Label TestLabel { get; set; }

        public Button TestButton { get; set; }

        public Button SpellButton { get; set; }

        #endregion
    }
}
