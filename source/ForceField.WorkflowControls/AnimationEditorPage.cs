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

namespace ForceField.WorkflowControls
{
    public partial class AnimationEditorPage : ServicePage, IAnimationEditorView
    {
        public AnimationEditorPage() : base()
        {
            presenter = new AnimationEditorPresenter(this);
        }

        public string SelectedAnimation
        {
            get
            {
                var animationName = (Controls.Find("SelectCharacterComboBox", true).First() as ComboBox).Text;

                return animationName;
            }
        }

        public string SelectedAnimatedAction
        {
            get
            {
                var animatedAction = (Controls.Find("SelectAnimatedActionComboBox", true).First() as ComboBox).Text;

                return animatedAction;
            }
        }

        public string[] CharacterList
        {
            set
            {
                AssignDataSourceToComboBox(value, "SelectCharacterComboBox");
            }
        }

        public string[] AnimatedActionsList
        {
            set
            {
                AssignDataSourceToComboBox(value, "SelectAnimatedActionComboBox");
            }
        }

        public string[] SpriteTextures
        {
            set
            {
                spriteTextures = value;

                var pictureBox = GetChildControl<PictureBox>("AnimationEditorPictureBox");
                pictureBox.Image = new Bitmap(value[0]);

                if (value.Length > 1)
                {
                    GenerateAdditionalPictureBoxes(value);
                }
            }
        }

        public bool AnimationEditorTextVisibility
        {
            set
            {
                GetChildControl<Label>("PleaseClickToStartLabel").Visible = value;
            }
        }

        public override void OnFormLoad(object sender, EventArgs e)
        {
            GetChildControl<Button>("LoadAllAnimationsButton").Click += new EventHandler(LoadAllAnimationsButton_Click);
            GetChildControl<ComboBox>("SelectCharacterComboBox").SelectedIndexChanged += new EventHandler(SelectAnimationComboBox_SelectedIndexChanged);
            GetChildControl<ComboBox>("SelectAnimatedActionComboBox").SelectedIndexChanged += new EventHandler(SelectAnimatedActionComboBox_SelectedIndexChanged);

            base.OnFormLoad(sender, e);
        }

        #region private

        private AnimationEditorPresenter presenter;

        private string[] spriteTextures = new string[0];

        private void LoadAllAnimationsButton_Click(object sender, EventArgs e)
        {
            presenter.OnLoadAllAnimationsButton_Click();
        }

        private void SelectAnimationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearAnimationEditPanel();

            presenter.OnSelectAnimationComboBoxSelectedIndexChanged((sender as ComboBox).Text);
        }

        private void SelectAnimatedActionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            presenter.OnSelectAnimatedActionComboBoxSelectedIndexChanged(GetChildControl<ComboBox>("SelectCharacterComboBox").Text, (sender as ComboBox).Text);
        }

        private void GenerateAdditionalPictureBoxes(string[] imageSources)
        {
            var pictureBox = GetChildControl<PictureBox>("AnimationEditorPictureBox");
            pictureBox.Dock = DockStyle.Top;

            var editPanel = GetChildControl<Panel>("AnimationEditPanel");

            for (var i = 1; i < imageSources.Length; i++)
            {
                var newPictureBox = new PictureBox
                {
                    Dock = DockStyle.Top,
                    Size = pictureBox.Size,
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Name = string.Format("{0}{1}", pictureBox.Name, i + 1),
                    Image = new Bitmap(imageSources[i])
                };

                editPanel.Controls.Add(newPictureBox);
            }
        }

        private void ClearAnimationEditPanel()
        {
            var panel = GetChildControl<Panel>("AnimationEditPanel");
            var controls = panel.Controls;

            for (var i = 0; i < controls.Count; i++)
            {
                if (controls[i] is PictureBox)
                {
                    var pictureBox = controls[i] as PictureBox;

                    if (pictureBox.Image != null)
                    {
                        pictureBox.Image.Dispose();
                        pictureBox.Image = null;
                    }

                    if (controls[i].Name != "AnimationEditorPictureBox")
                    {
                        panel.Controls.Remove(controls[i]);
                    }
                }
            }
        }

        #endregion
    }
}
