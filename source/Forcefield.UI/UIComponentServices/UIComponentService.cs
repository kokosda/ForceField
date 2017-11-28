using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using ForceField.Interfaces;
using ForceField.Domain.Input;
using ForceField.UI.UIElements;

namespace ForceField.UI.UIComponentServices
{
    public class UIComponentService : IUIComponentService
    {
        public UIComponentService(ISpriteBatchService spriteBatchService, IUserInputService inputService)
        {
            this.Elements = new List<UIComponent>();

            Click = new Handler(DefaultHandle);
            CursorEntered = new Handler(DefaultHandle);

            SpriteBatchService = spriteBatchService;
            InputService = inputService;
        }

        public UIComponentService(Game game)
        {
            this.Elements = new List<UIComponent>();

            SpriteService = game.Services.GetService(typeof(ISpriteService)) as ISpriteService;
            SpriteBatchService = game.Services.GetService(typeof(ISpriteBatchService)) as ISpriteBatchService;
            InputService = game.Services.GetService(typeof(IUserInputService)) as IUserInputService;

            this.game = game;

            //Click = new Handler(DefaultHandle);
            //CursorEntered = new Handler(DefaultHandle);
        }

        public delegate void Handler(UIComponent sender);

        #region Properties

        public Handler Click { get; set; }

        public Handler CursorEntered { get; set; }

        public IList<UIComponent> Elements { get; protected set; }

        #endregion

        public virtual void Update(GameTime gameTime)
        {
            foreach (UIComponent component in Elements)
            {
                //Point mousePosition = InputService.IsMousePosition();

                if (CheckCursorEntered(component))
                {
                    //CursorEntered(component);
                    component.DoCursorEntered();

                    if (component.Clickable)
                    {
                        if (CheckClicked(component))
                        {
                            //Click(component);
                            component.DoClicked();
                        }
                    }
                }
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            for (int i = 0; i < Elements.Count; ++i)
            {
                UIComponent element = Elements[i];

                if (element.Sprite != null)
                {
                    SpriteBatchService.Draw(element.Sprite);
                }

                if (element.SpriteText != null)
                {
                    SpriteBatchService.Draw(element.SpriteText);
                }
            }
        }

        public void AddComponent(UIComponent component)
        {
            Elements.Add(component);
        }

        #region protected

        #region Properties

        protected ISpriteService SpriteService { get; private set; }

        protected ISpriteBatchService SpriteBatchService { get; private set; }

        protected IUserInputService InputService { get; private set; }

        #endregion

        protected virtual bool CheckCursorEntered(UIComponent component)
        {
            Point mousePosition = InputService.IsMousePosition();

            if (component.Sprite != null)
            {
                if (component.Sprite.BoundingRectangle.Contains(mousePosition))
                {
                    return true;
                }
            }

            if (component.SpriteText != null)
            {
                if (component.SpriteText.BoundingRectangle.Contains(mousePosition))
                {
                    return true;
                }
            }
            
            return false;
        }

        protected virtual bool CheckClicked(UIComponent component)
        {
            if (!CheckCursorEntered(component))
            {
                return false;
            }

            return (InputService.IsMouseButtonPress(MouseButton.LeftButton));
        }

        #endregion

        #region private

        private void DefaultHandle(UIComponent sender)
        {

        }

        #region field

        Game game;

        #endregion

        #endregion
    }
}
