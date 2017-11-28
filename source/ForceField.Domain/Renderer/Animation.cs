using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForceField.Domain.Renderer.Base;


namespace ForceField.Domain.Renderer
{
    public class Animation : Entity
    {
        public Animation()
        {
            KeyFrames = new List<KeyFrame>();
            currentKeyFrame.OrderNum = -1;
            FrameDelay = 0f;
        }

        public static Animation Clone(Animation anim, Sprite newSprite)
        {
            Animation clone = new Animation();

            clone.SpriteName = anim.SpriteName;
            clone.IsCycle = anim.IsCycle;
            clone.Speed = anim.Speed;
            clone.FrameSize = anim.FrameSize;
            clone.AnimatedAction = anim.AnimatedAction;
            clone.TimeFromLastFrame = anim.TimeFromLastFrame;
            clone.Sprite = newSprite;

            clone.KeyFrames = new List<KeyFrame>(anim.KeyFrames);
            clone.CurrentKeyFrame = anim.CurrentKeyFrame;

            return clone;
        }

        public override string ToString()
        {
            return string.Format("AnimatedAction: {0}, FrameSize: {1}, KeyFrames: {2}, Sprite: {3}, IsCycle: {4}", AnimatedAction.ToString(), FrameSize, KeyFrames.Count, SpriteName, IsCycle.ToString());
        }
 
        #region Properties

        public string SpriteName { get; set; }

        public bool IsCycle { get; set; }

        public int Speed { get; set; }

        public Point FrameSize { get; set; }

        public List<KeyFrame> KeyFrames { get; set; }

        public AnimatedAction AnimatedAction { get; set; }

        public float FrameDelay { get; set; }

        public Sprite Sprite { get; set; }

        public KeyFrame CurrentKeyFrame
        {
            get
            {
                return currentKeyFrame;
            }
            set
            {
                currentKeyFrame = value;
            }
        }

        public int FramesCount
        {
            get
            {
                return KeyFrames.Count;
            }
        }

        public Rectangle ToRectangle
        {
            get
            {
                return new Rectangle(CurrentKeyFrame.Location.X, CurrentKeyFrame.Location.Y, FrameSize.X, FrameSize.Y);
            }
        }

        public int TimeFromLastFrame { get; set; }

        #endregion

        #region private

        private KeyFrame currentKeyFrame;

        #endregion
    }
}