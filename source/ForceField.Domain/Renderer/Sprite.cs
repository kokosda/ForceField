using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForceField.Domain.Renderer.Base;
using ForceField.Domain.Content;
using ForceField.Domain.GameLogic;

namespace ForceField.Domain.Renderer
{
    public class Sprite : RenderEntity
    {
        public Sprite()
        {
            Animations = new List<Animation>();
            CurrentAction = AnimatedAction.Idle;
            Color = new Color(1.0f, 1.0f, 1.0f);
            Scale = 1.0f;
        }

        public Sprite(Vector2 location) : base(location)
        {
            Animations = new List<Animation>();
            CurrentAction = AnimatedAction.Idle;
            Color = new Color(1.0f, 1.0f, 1.0f);
            Scale = 1.0f;
        }

        public static Sprite Clone(Sprite val, Unit newUnit)
        {
            Sprite clone = new Sprite();

            foreach (Animation anim in val.Animations)
            {
                clone.Animations.Add(Animation.Clone(anim, clone));
            }
            clone.Location = val.Location;
            clone.Angle = val.Angle;
            clone.Color = val.Color;
            clone.Effect = val.Effect;
            clone.CurrentAction = val.CurrentAction;
            clone.TextureResources = new List<ContentResource<Texture2D>>(val.TextureResources.ToList());
            clone.Unit = newUnit;
            
            return clone;
        }

        public static Sprite Clone(Sprite sprite)
        {
            Sprite clone = new Sprite();
            clone.Angle = sprite.Angle;
            clone.Animations = sprite.Animations;
            clone.CanDraw = sprite.CanDraw;
            clone.Color = sprite.Color;
            clone.CurrentAction = sprite.CurrentAction;
            clone.Effect = sprite.Effect;
            clone.Location = sprite.Location;
            clone.Name = sprite.Name;
            clone.RenderData = sprite.RenderData;
            clone.Scale = sprite.Scale;
            clone.TextureResources = sprite.TextureResources;
            clone.Unit = sprite.Unit;
            return clone;
        }

        public override string ToString()
        {
            return Name;
        }

        #region Properties

        [XmlIgnore]
        public IList<Animation> Animations { get; set; }

        public AnimatedAction CurrentAction { get; set; }

        [XmlIgnore]
        public Rectangle ClientRectangle
        {
            get { return CurrentAnimation.ToRectangle; }
        }

        [XmlIgnore]
        public virtual Vector2 Origin 
        { 
            get 
            { 
                return new Vector2((int)(CurrentAnimation.ToRectangle.Width * Scale) >> 1, (int)(CurrentAnimation.ToRectangle.Height * Scale) >> 1); 
            } 
        }

        public Vector2 TranslationPosition
        {
            get;
            set;
        }

        public float Layer
        {
            //get
            //{
            //    return Unit.Layer;
            //}
            get;
            set;
        }

        public Color Color { get; set; }

        public SpriteEffects Effect { get; set; }

        [XmlIgnore]
        public RenderData RenderData { get; set; }

        [XmlIgnore]
        public IList<ContentResource<Texture2D>> TextureResources { get; set; }

        [XmlIgnore]
        public bool CanDraw
        {
            get;
            set;
        }

        [XmlIgnore]
        public Animation CurrentAnimation { get { return Animations.First(p => p.AnimatedAction == CurrentAction); } }

        public Rectangle BoundingRectangle
        {
            get
            {
                Point frameSize = CurrentAnimation.FrameSize;
                return new Rectangle((int)TranslationPosition.X, (int)TranslationPosition.Y, (int)(frameSize.X * Scale), (int)(frameSize.Y * Scale));
            }
        }

        /// <summary>
        /// Указывает на масштабирование юнита
        /// меняется [0; float.MaxValue), 1 - оригинальный размер
        /// </summary>
        public float Scale { get; set; }

        [XmlIgnore]
        public Unit Unit { get; set; }

        #endregion

    }
}
