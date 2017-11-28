using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ForceField.WorkflowInfrastructure.Presenters
{
    public interface IWorkflowServiceView
    {
        void OpenFrameSourceDialog();

        void OpenColorPickerDialog();

        bool ImageStickIsFrameSourceFileSelected { get; }

        string ImageStickFrameSourceFilePath { get; }

        string ImageStickFrameFileSelectedStatus { set; }

        /// <summary>
        /// 2-элемента: Ш и В
        /// </summary>
        int?[] ImageStickFrameSize { get; }

        string ImageStickExtension { get; }

        int ImageStickQuality { get; }

        Color ImageStickBackgroundColor { get; }

        void Log(string format, params object[] values);
    }
}
