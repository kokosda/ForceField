using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using log4net;
using ForceField.Interfaces;
using ForceField.Domain.Renderer;
using ForceField.Domain.Renderer.Base;

namespace ForceField.Core.Services
{
    public class AnimationService : IAnimationService
    {
        #region Properties

        public IList<Animation> Animations
        {
            get;
            set;
        }

        protected ISpriteService SpriteService
        {
            get;
            set;
        }

        #endregion

        public AnimationService(IList<Animation> animations)
        {
            Animations = animations;
        }

        #region InGame functionality

        public void Update(Animation anim, int delta)
        {
            anim.TimeFromLastFrame += delta;

            if (anim.TimeFromLastFrame > anim.FrameDelay)
            {
                if (IsTimeToSlideFrame(anim))
                {
                    anim.CurrentKeyFrame = NextKeyFrame(anim);
                    anim.TimeFromLastFrame -= anim.Speed;
                }
            }
        }

        public void Reset(Animation anim)
        {
            anim.TimeFromLastFrame = 0;
            anim.CurrentKeyFrame = anim.KeyFrames[0];
        }

        public KeyFrame NextKeyFrame(Animation anim)
        {
            var orderNum = anim.CurrentKeyFrame.OrderNum;

            if (anim.IsCycle)
            {
                return anim.CurrentKeyFrame = anim.FramesCount - 1 != anim.CurrentKeyFrame ? anim.KeyFrames[orderNum + 1] : anim.KeyFrames.First();
            }
            else
            {
                return anim.CurrentKeyFrame = anim.FramesCount - 1 != anim.CurrentKeyFrame ? anim.KeyFrames[orderNum + 1] : anim.KeyFrames.Last();
            }
        }

        public bool IsTimeToSlideFrame(Animation anim)
        {
            return anim.TimeFromLastFrame > anim.Speed;
        }

        public Texture2D GetCurrentTexture(Animation anim)
        {
            return anim.CurrentKeyFrame.Texture;
        }

        #endregion

        #region Internal

        public void SetSpriteService(ISpriteService spriteService)
        {
            SpriteService = spriteService;
        }

        public void LoadAllAnimations(string rootDirectory)
        {
            LoadAnimationsFromDir(rootDirectory);
        }

        /// <summary>
        /// Рекурсивно загружает анимации из заданного каталога
        /// </summary>
        /// <param name="dirPath">Путь к каталогу</param>
        public void LoadAnimationsFromDir(string dirPath)
        {
            string[] entities = Directory.GetFileSystemEntries(dirPath);
            string[] animFiles = Directory.GetFiles(dirPath, "*.anim");

            foreach (string animFilename in animFiles)
            {
                LoadAnimations(animFilename);
            }

            foreach (string entityName in entities)
            {
                if (Directory.Exists(entityName))
                {
                    LoadAnimationsFromDir(entityName);
                }
            }
        }

        /// <summary>
        /// Загружает все анимации из заданного файла в каталоге анимаций
        /// </summary>
        /// <param name="filename">Имя файла анимаций</param>
        public void LoadAnimations(string filepath)
        {
            XmlDocument animFile = new XmlDocument();
            animFile.Load(filepath);

            if (animFile.DocumentElement.Name != "animations")
            {
                // иключение
            }

            foreach (XmlNode animationNode in animFile.DocumentElement.ChildNodes)
            {
                if (animationNode.Name == "animation")
                {
                    LoadAnimation(animationNode);
                }
                else
                {
                    // исключение
                }
            }
        }

        #endregion

        #region private

        /// <summary>
        /// Закрытый метод. Загрузить анимацию из заданного xml-узла
        /// </summary>
        /// <param name="animNode">xml-узел, содержащий анимацию</param>
        private void LoadAnimation(XmlNode animNode)
        {
            Animation anim = new Animation();

            anim.SpriteName = animNode.Attributes["sprite"].Value;
            anim.Sprite = SpriteService.GetByName(anim.SpriteName);

            foreach (XmlNode node in animNode.ChildNodes)
            {
                if (node.Name == "name")
                {
                    anim.Name = node.InnerText.Replace(" ", string.Empty);
                    anim.AnimatedAction = AnimationTypes.Get(anim.Name);
                    continue;
                }

                if (node.Name == "speed")
                {
                    anim.Speed = int.Parse(node.InnerText);
                    continue;
                    // todo: Обработка ошибок
                }

                if (node.Name == "isCycle")
                {
                    anim.IsCycle = bool.Parse(node.InnerText);
                    continue;
                    // todo: обработка ошибок
                }

                if (node.Name == "frameSize")
                {
                    foreach (XmlNode frameSizeNode in node)
                    {
                        if (frameSizeNode.Name == "x")
                        {
                            Point frameSize = anim.FrameSize;
                            frameSize.X = int.Parse(frameSizeNode.InnerText);
                            anim.FrameSize = frameSize;                            
                        }

                        if (frameSizeNode.Name == "y")
                        {
                            Point frameSize = anim.FrameSize;
                            frameSize.Y = int.Parse(frameSizeNode.InnerText);
                            anim.FrameSize = frameSize;      
                        }
                    }

                    continue;
                }

                if (node.Name == "keyFrames")
                {
                    anim.KeyFrames = ParseKeyFrames(node);
                    continue;
                }
            }

            InitializeKeyFrameTextures(anim);

            // без этого не работает
            anim.CurrentKeyFrame = anim.KeyFrames[0];

            Animations.Add(anim);
            anim.Sprite.Animations.Add(anim);
        }

        /// <summary>
        /// Вспомогательный, закрытый метод. Пропарсить xml-элемент "frame" (последовательность кадров)
        /// </summary>
        /// <param name="parentNode">Элемент frame</param>
        /// <returns>Список кадров</returns>
        private List<KeyFrame> ParseKeyFrames(XmlNode parentNode)
        {
            List<KeyFrame> keyFrames = new List<KeyFrame>();

            foreach (XmlNode frameNode in parentNode.ChildNodes)
            {
                KeyFrame keyFrame = new KeyFrame();

                foreach (XmlNode node in frameNode.ChildNodes)
                {
                    if (node.Name == "x")
                    {
                        keyFrame.Location.X = int.Parse(node.InnerText);
                        continue;
                        // todo: проверка ошибок
                    }

                    if (node.Name == "y")
                    {
                        keyFrame.Location.Y = int.Parse(node.InnerText);
                        continue;
                        // todo: проверка ошибок
                    }

                    if (node.Name == "textureName")
                    {
                        keyFrame.TextureName = node.InnerText;
                        continue;
                        // todo: проверка ошибок
                    }

                    if (node.Name == "orderNum")
                    {
                        keyFrame.OrderNum = int.Parse(node.InnerText); 
                        continue;
                    }
                }

                keyFrames.Add(keyFrame);
            }

            return keyFrames;
        }

        private void InitializeKeyFrameTextures(Animation anim)
        {
            for (int i = 0; i < anim.KeyFrames.Count; i++)
            {
                var sprite = anim.Sprite;

                if (sprite != null)
                {
                    string textureName = anim.KeyFrames[i].TextureName;
                    var keyFrame = anim.KeyFrames[i];

                    keyFrame.Texture = SpriteService.GetTexture(sprite, textureName);

                    anim.KeyFrames[i] = keyFrame;
                }
                else
                {
                    //log4net.LogManager.GetLogger(GetType()).ErrorFormat("Requested sprite {0} was not found.", anim.SpriteName);
                }
            }
        }

        #endregion
    }
}