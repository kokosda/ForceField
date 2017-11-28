using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.UI.UIElements;
using ForceField.Domain.Renderer;
using ForceField.UI.Enums;

namespace ForceField.UI.UIElements
{
    public class CheckBox : UIComponent
    {
        public Label Label { get; set; }
        public Button Button { get; set; }
        public Sprite SelectedSprite { get; set; }
        public Sprite NormalSprite { get; set; }
        public CheckBoxState State { get; set; }

        public CheckBox(Label label, Button button, Sprite Select)
        {
            SpriteText = null;
            Sprite = null;
            Clicked = false;
            Clickable = false;
            Label = label;
            Button = button;
            SelectedSprite = Select;
            NormalSprite = button.Sprite;
            State = CheckBoxState.Unknow;
        }
    }
}
