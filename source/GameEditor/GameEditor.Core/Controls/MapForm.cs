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
    public partial class MapForm : Form
    {
        public MapForm(Game game)
        {
            tileMapService = game.Services.GetService(typeof(ITileMapService)) as ITileMapService;
            spriteService = game.Services.GetService(typeof(ISpriteService)) as ISpriteService;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int tileHeight,tileWidth,mapWidth,mapHeight;

            if (int.TryParse(MapWidth.Text, out mapWidth) == false)
            {
                MessageBox.Show("Карта не может быть создана! Элемент 'Размер' указан неверно!", "Предупреждение", MessageBoxButtons.OK);
                return;
            }

            if (int.TryParse(MapHeight.Text, out mapHeight) == false)
            {
                MessageBox.Show("Карта не может быть создана! Элемент 'Размер' указан неверно!", "Предупреждение", MessageBoxButtons.OK);
                return;
            }
            
            if (int.TryParse(TileWidth.Text, out tileWidth) == false)
            {
                MessageBox.Show("Карта не может быть создана! Элемент 'Ширина клетки' указан неверно!", "Предупреждение", MessageBoxButtons.OK);
                return;
            }


            if (int.TryParse(TileHeight.Text, out tileHeight) == false)
            {
                MessageBox.Show("Карта не может быть создана! Элемент 'Высота клекти' указан неверно!", "Предупреждение", MessageBoxButtons.OK);
                return;
            }


            if (tileMapService != null)
            {
                if (tileMapService.CurrentTileMap == null)
                {
                    tileMapService.CurrentTileMap = tileMapService.CreateTileMap(new Microsoft.Xna.Framework.Point(tileWidth, tileHeight),
                                                     new Microsoft.Xna.Framework.Vector2(0, 0),
                                                     new Microsoft.Xna.Framework.Point(mapWidth, mapHeight),
                                                     spriteService.GetByName("Tile"),
                                                     MapName.Text);
                }
            }
            else
            {
               // log4net.LogManager.GetLogger(GetType()).Error("tileMapService = null !");
            }
        }


        private ITileMapService tileMapService;
        private ISpriteService spriteService;

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void MapForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
