using ForceField.Domain.Renderer;

namespace ForceField.UI.UIElements
{
    public class UIComponent
    {
        public UIComponent()
        {
        }

        public delegate void ComponentEventHandler(UIComponent sender);

        public event ComponentEventHandler ClickedEvent;

        public event ComponentEventHandler CursorEnteredEvent;

        #region Properties

        public string Id { get; set; }

        public Sprite Sprite { get; set; }

        public SpriteText SpriteText { get; set; }

        public bool Clicked { get; set; }

        public bool Clickable { get; set; }

        #endregion

        public void DoClicked()
        {
            if (Clickable && ClickedEvent != null)
            {
                ClickedEvent(this);
            }
        }

        public void DoCursorEntered()
        {
            if (CursorEnteredEvent != null)
            {
                CursorEnteredEvent(this);
            }
        }
    }
}
