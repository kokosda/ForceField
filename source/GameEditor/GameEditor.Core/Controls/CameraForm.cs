using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using ForceField.Interfaces;

namespace GameEditor.Core.Controls
{
    public partial class CameraForm : Form
    {
        public CameraForm(Game game)
        {
            InitializeComponent();
            camera = game.Services.GetService(typeof(ICamera2DService)) as ICamera2DService;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float spdX, spdY, posX, posY;

            if (float.TryParse(speedX.Text, out spdX) == false)
            {
                MessageBox.Show("Не удается установить скорость камеры по X");
                return;
            }

            if (float.TryParse(speedY.Text, out spdY) == false)
            {
                MessageBox.Show("Не удается установить скорость камеры по Y");
                return;
            }

            if (float.TryParse(positionX.Text, out posX) == false)
            {
                MessageBox.Show("Не удается установить позицию камеры по X");
                return;
            }

            if (float.TryParse(positionY.Text, out posY) == false)
            {
                MessageBox.Show("Не удается установить позицию камеры по Y");
                return;
            }


            camera.Data.Offset = new Vector2(spdX,spdY);
            camera.Data.Translation = new Vector2(posX, posY);
        }

        private ICamera2DService camera;
    }
}
