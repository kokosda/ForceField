using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;

using ForceField.Domain.Renderer;

namespace ForceField.Domain.GameLogic
{
    public class Unit : Entity
    {

        public Unit()
        {
            LocationArea = 0;
        }

        #region Properties

        [XmlIgnore]
        public Sprite Sprite { get; set; }

        [XmlIgnore]
        public bool IsSelected 
        {
            get
            {
                return isSelected;
            }
        }

        [XmlIgnore]
        public bool IsAnimated
        {
            get
            {
                return Sprite.Animations.Count > 0;
            }
        }

        public Vector2 Position { get { return Sprite.Location; } }


        [XmlIgnore]
        public Rectangle BoundingRectangle
        {
            get
            {
                return Sprite.BoundingRectangle;
            }
        }

        [XmlIgnore]
        public HexagonBound BoundingBox
        {
            get;
            set;
        }


        public int LocationArea { get; set; }

        #endregion

        public virtual void SetBound(TileMap tileMap){}

        public IList<Tile> TilesArea { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", GetType().Name, Name);
        }

        public float Layer
        {
            get
            {
                return Sprite.Layer;
            }
            set
            {
                Sprite.Layer = value;
            }
        }

        public void SetSelect(bool value)
        {
            isSelected = value;
        }

        public void SetLayer(TileMap tileMap)
        {
            //Layer = Tile.X * tileMap.layerX + Tile.Y * tileMap.layerY;
            Layer = (float)(1) * tileMap.LayerX + (float)(1) * tileMap.LayerY;
            Layer = tileMap.LayerX + tileMap.LayerY;
        }

        public void SetLayer(float layer)
        {
            Layer = layer;
        }

        #region private
        //private float layer;
        private bool isSelected;
        #endregion
    }
}
