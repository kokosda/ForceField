using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ForceField.WorkflowDomain.Interfaces;
using ForceField.Domain.Renderer;
using ForceField.Domain.Renderer.Base;
using ForceField.WorkflowInfrastructure.Presenters;
using System.IO;

namespace ForceField.WorkflowControls
{
    public partial class ImageStickerPage : ServicePage, IImageStickerView
    {
        public ImageStickerPage()
            : base()
        {
        }

        public override void OnFormLoad(object sender, EventArgs e)
        {
            presenter = new ImageStickerPresenter(this, FindForm() as IWorkflowServiceView);

            GetChildControl<Button>("FrameSourceButton").Click += new EventHandler(FrameSourceButton_Clicked);
            GetChildControl<Button>("ImageStickButton").Click += new EventHandler(ImageStickButton_Clicked);
            GetChildControl<Button>("ImageStickerBackgroundColorButton").Click += new EventHandler(ImageStickerBackgroundColorButton_Clicked);

            base.OnFormLoad(sender, e);
        }

        public string SelectedFilePreview
        {
            set
            {
                var pb = GetChildControl<PictureBox>("ImageStickSelectedFilePreviewPictureBox");

                try
                {
                    pb.Image = new Bitmap(value);
                }
                catch
                {
                    if (pb.Image != null)
                    {
                        pb.Image.Dispose();
                        pb.Image = null;
                    }
                }
            }
        }

        public string GeneratedSequence
        {
            set
            {
                var pb = GetChildControl<PictureBox>("ImageStickerPictureBox2");

                try
                {
                    if (pb.Image != null)
                    {
                        pb.Image.Dispose();
                        pb.Image = null;
                    }

                    pb.Image = new Bitmap(value);
                }
                catch
                {
                }
            }
        }

        public bool ResultPanelHasImage
        {
            get
            {
                return GetChildControl<PictureBox>("ImageStickerPictureBox2").Image == null;
            }
        }

        public void SaveStickResult(string destinationPath)
        {
            presenter.OnSaveStickResult(destinationPath);
        }

        public void EmptyTempDirecories()
        {
            var pb = GetChildControl<PictureBox>("ImageStickerPictureBox2");
            if (pb.Image != null)
            {
                pb.Image.Dispose();
                pb.Image = null;
            }
            presenter.OnEmptyTempDirectories();
        }

        #region private

        private ImageStickerPresenter presenter;

        private void FrameSourceButton_Clicked(object sender, EventArgs e)
        {
            presenter.OnFrameSourceButtonClicked();
        }

        private void ImageStickButton_Clicked(object sender, EventArgs e)
        {
            presenter.OnImageStickButtonClicked();
        }

        private void ImageStickerBackgroundColorButton_Clicked(object sender, EventArgs e)
        {
            presenter.OnImageStickerBackgroundColorButtonClicked();
        }

        #endregion

    }
}
