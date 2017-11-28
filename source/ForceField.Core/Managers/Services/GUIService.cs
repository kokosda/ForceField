using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ForceField.Interfaces;
using ForceField.UI.UIComponentServices;
using ForceField.UI.UIElements;

namespace ForceField.Core.Services
{
    public class GUIService : IGUIService
    {
        public GUIService(IList<IUIComponentService> UIComponents)
        {
            this.uiComponents = UIComponents;
        }

        #region Properties

        public IList<IUIComponentService> UIComponents 
        { 
            get 
            { 
                return uiComponents; 
            } 
        }

        #endregion

        public void AddComponentService(IUIComponentService componentService)
        {
            uiComponents.Add(componentService);
        }

        public void DeleteComponentService(IUIComponentService componentService)
        {
            // todo
        }

        public void ClearComponentServices()
        {
            uiComponents.Clear();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IUIComponentService component in uiComponents)
            {
                component.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (IUIComponentService component in uiComponents)
            {
                component.Draw(gameTime);
            }
        }

        public void Initialize(Game game)
        {
            spriteBatchService = game.Services.GetService(typeof(ISpriteBatchService)) as ISpriteBatchService;
        }

        #region private

        private ISpriteBatchService spriteBatchService;
        private IList<IUIComponentService> uiComponents;

        #endregion
    }
}
