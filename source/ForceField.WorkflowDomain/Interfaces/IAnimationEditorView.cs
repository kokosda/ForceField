using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Domain.Renderer;
using ForceField.Domain.Renderer.Base;


namespace ForceField.WorkflowDomain.Interfaces
{
    public interface IAnimationEditorView
    {
        string SelectedAnimation { get; }

        string SelectedAnimatedAction { get; }

        string[] CharacterList { set; }

        string[] AnimatedActionsList { set; }

        bool AnimationEditorTextVisibility { set; }

        string[] SpriteTextures { set; }
    }
}
