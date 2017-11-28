using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.WorkflowDomain.Interfaces;
using ForceField.Core.Services;
using ForceField.Domain.Renderer;
using ForceField.Domain.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text.RegularExpressions;
using ForceField.Domain.Renderer.Base;

namespace ForceField.WorkflowInfrastructure.Presenters
{
    public class AnimationEditorPresenter
    {
        public AnimationEditorPresenter(IAnimationEditorView view)
        {
            this.view = view;

            AnimationTypes.Initialize();

            spriteService = new SpriteService(new List<Sprite>(), new List<SpriteText>());
            animationService = new AnimationService(new List<Animation>());

            animationService.SetSpriteService(spriteService);
        }

        public bool IsAllAnimationsLoaded
        {
            get
            {
                return isAllAnimationsLoaded;
            }
        }

        public void OnLoadAllAnimationsButton_Click()
        {
            animationService.Animations.Clear();
            spriteService.Sprites.Clear();

            ConstructSpriteCollection(ref spriteService, WorkflowConfiguration.SpritePaths);

            foreach (var animationPath in WorkflowConfiguration.AnimationPaths)
            {
                animationService.LoadAllAnimations(animationPath);
            }

            isAllAnimationsLoaded = true;

            var animationsList = spriteService.Sprites
                                                .Select(s => s.Name)
                                                .OrderBy(n => n)
                                                .ToArray();

            view.CharacterList = animationsList;

            var spriteName = animationsList.FirstOrDefault();

            view.AnimationEditorTextVisibility = false;
        }

        public void OnSelectAnimationComboBoxSelectedIndexChanged(string spriteName)
        {
            if (!string.IsNullOrEmpty(spriteName))
            {
                view.AnimatedActionsList = GetAnimatedActionList(spriteName);
                view.SpriteTextures = GetSpriteTexturesPaths(spriteName);
            }
        }

        public void OnSelectAnimatedActionComboBoxSelectedIndexChanged(string spriteName, string animatedAction)
        {
            if (!string.IsNullOrEmpty(spriteName) && !string.IsNullOrEmpty(animatedAction))
            {

            }
        }

        #region private

        private IAnimationEditorView view;

        private bool isAllAnimationsLoaded = false;

        private SpriteService spriteService;

        private AnimationService animationService;

        private void ConstructSpriteCollection(ref SpriteService spriteService, string[] spritePaths)
        {
            for (var i = 0; i < spritePaths.Count(); i++)
            {
                var spriteFiles = System.IO.Directory.GetFiles(spritePaths[i]);

                for (var j = 0; j < spriteFiles.Count(); j++)
                {
                    var spriteFile = System.IO.Path.GetFileNameWithoutExtension(spriteFiles[j]);
                    var name = Regex.Replace(spriteFile, @"[\d]+", string.Empty);

                    if (spriteService.GetByName(name) == null)
                    {
                        var allSpriteFiles = System.IO.Directory.GetFiles(spritePaths[i], string.Format("{0}??.*", name));
                        var textureResources = new List<ContentResource<Texture2D>>();

                        foreach (var f in allSpriteFiles)
                        {
                            textureResources.Add(new ContentResource<Texture2D>(f, null));
                        }

                        var sprite = new Sprite
                        {
                            TextureResources = textureResources,
                            Name = name
                        };

                        spriteService.Sprites.Add(sprite);
                    }
                }
            }
        }

        private string[] GetAnimatedActionList(string spriteName)
        {
            return spriteService.GetByName(spriteName).Animations
                                                        .Select(a => a.AnimatedAction.ToString())
                                                        .OrderBy(aa => aa)
                                                        .ToArray();
        }

        private string[] GetSpriteTexturesPaths(string spriteName)
        {
            return spriteService.GetByName(spriteName).TextureResources
                                                        .Select(tr => tr.ResourcePath)
                                                        .ToArray();
        }

        #endregion
    }
}
