using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ForceField.WorkflowControls;
using ForceField.WorkflowInfrastructure.Presenters;
using System.IO;

namespace ForceField.WorkflowServices
{
    public partial class WorkflowServices : Form, IWorkflowServiceView
    {
        public WorkflowServices()
        {
            InitializeComponent();
            ImageStickBackgroundColorDialog.Color = Color.Transparent;
        }

        #region Properties

        public bool ImageStickIsFrameSourceFileSelected
        {
            get 
            {
                return frameSourceFileSelected;
            }
        }

        public string ImageStickFrameFileSelectedStatus
        {
            set
            {
                FramesFileSelectedValueLabel.Text = value;
            }
        }
        
        public string ImageStickFrameSourceFilePath
        {
            get
            {
                return FrameSourceDialog.FileName;
            }
        }

        public int?[] ImageStickFrameSize
        {
            get
            {
                try
                {
                    imageStickFrameSize[0] = Convert.ToInt32(ImageStickFrameSizeWidthTextBox.Text);
                    imageStickFrameSize[1] = Convert.ToInt32(ImageStickFrameSizeHeightTextBox.Text);
                }
                catch 
                {
                    imageStickFrameSize[0] = null;
                    imageStickFrameSize[1] = null;

                    Log("Frame size is not set or cannot be converted to integer value.");
                }

                return imageStickFrameSize;
            }
        }

        public string ImageStickExtension
        {
            get 
            {
                var ext = string.Empty;

                if(!string.IsNullOrEmpty(ImageStickerExtensionsComboBox.Text))
                {
                    ext = ImageStickerExtensionsComboBox.Text;
                }
                else
                {
                    ext = ".png";
                }

                return ext;
            }
        }

        public int ImageStickQuality
        {
            get
            {
                return (int)ImageStickerQualityNumericUpDown.Value;
            }
        }

        public Color ImageStickBackgroundColor
        {
            get 
            {
                return ImageStickBackgroundColorDialog.Color;
            }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (ServicePage servicePage in ServicePageContainer.TabPages)
            {
                servicePage.OnFormLoad(this, e);
            }
        }

        public void OpenFrameSourceDialog()
        {
            FrameSourceDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            switch (FrameSourceDialog.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.OK:
                    frameSourceFileSelected = true;
                    break;
                default:
                    frameSourceFileSelected = false;
                    break;
            }
        }

        public void OpenColorPickerDialog()
        {
            switch (ImageStickBackgroundColorDialog.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.OK:
                    ImageStickerBackgroundColorLabelValue.BackColor = ImageStickBackgroundColorDialog.Color;
                    break;
                default:
                    break;
            }
        }

        public void Log(string format, params object[] values)
        {
            ImageStickerStatusBox.AppendText(string.Format(format, values));
            ImageStickerStatusBox.AppendText(Environment.NewLine);
            ImageStickerStatusBox.ScrollToCaret();
            ImageStickerStatusBox.Update();
        }

        #region private

        private bool frameSourceFileSelected;

        private int?[] imageStickFrameSize = { null, null };

        private void ImageStickResultPanelMenuStrip_Opened(object sender, EventArgs e)
        {
            ImageStickSaveAsMenuItem.Enabled = ImageStickerPictureBox2.Image != null;
        }

        private void ImageStickSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            ImageStickSaveResultDialog.FileName = string.Format("{0}{1}", Path.GetFileNameWithoutExtension(ImageStickFrameSourceFilePath), ImageStickExtension);

            switch (ImageStickSaveResultDialog.ShowDialog())
            {
                case System.Windows.Forms.DialogResult.OK:
                    ImageStickerPage.SaveStickResult(ImageStickSaveResultDialog.FileName);
                    break;
                default:
                    break;
            }
        }

        private void WorkflowServices_FormClosing(object sender, FormClosingEventArgs e)
        {
            ImageStickerPage.EmptyTempDirecories();
        }

        #endregion

    }
}
