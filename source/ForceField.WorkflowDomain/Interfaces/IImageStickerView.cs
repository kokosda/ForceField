using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForceField.WorkflowDomain.Interfaces
{
    public interface IImageStickerView
    {
        string SelectedFilePreview { set; }

        string GeneratedSequence { set; }
    }
}
