using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ForceField.Core.Services;
using ForceField.UI.UIComponentServices;
using ForceField.Interfaces;
using ForceField.Core.Service;

namespace ForceField.Core.Managers
{
    public class GUIManager : IGameComponent
    {
        public GUIManager(Game game)
        {
            this.game = game;
            game.Components.Add(this);

            uiService = new GUIService(new List<IUIComponentService>());
            game.Services.AddService(typeof(IGUIService), uiService);
        }

        public void Initialize()
        {

        }

        public void AddComponent(UIComponentService component)
        {
            //component.inputService = game.Services.GetService(typeof(IUserInputService)) as IUserInputService;
            //uiService.UIComponents.Add(component);
        }

        #region private

        private Game game;
        private GUIService uiService;

        #endregion
    }
}
