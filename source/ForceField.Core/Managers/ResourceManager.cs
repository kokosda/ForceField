using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

using ForceField.Domain.Content;
using ForceField.Core.Services;
using Microsoft.Xna.Framework.Content;
using ForceField.Interfaces;

namespace ForceField.Core.Managers
{
    public class ResourceManager : DrawableGameComponent, IServiceProvider
    {
        public ResourceManager(string rootDirectory, Game game) : base(game)
        {
            RootFolder = rootDirectory;
            Instance = this;
            this.contentManager = base.Game.Content;
            this.contentManager.RootDirectory = rootDirectory;
            gsc = base.Game.Services;

            base.Game.Components.Add(this);
        }

        #region Properties

        public ResourceService<Texture2D> Textures { get; set; }

        public ResourceService<Model> Models { get; set; }

        public ResourceService<Effect> Effects { get; set; }

        public ResourceService<SoundEffect> Sounds { get; set; }

        public ResourceService<SpriteFont> Fonts { get; set; }

        public GameServiceContainer Gsc
        {
            get
            {
                return gsc;
            }
        }

        #region private

        private string RootFolder { get; set; }

        private static ResourceManager Instance { get; set; }

        #endregion

        #endregion

        #region public Methods

        public override void Initialize()
        {
            Textures = GetService(typeof(Texture2D)) as ResourceService<Texture2D>;
            gsc.AddService(typeof(IResourceService<Texture2D>), Textures);

            Effects = GetService(typeof(Effect)) as ResourceService<Effect>;
            gsc.AddService(typeof(IResourceService<Effect>), Effects);

            Sounds = GetService(typeof(SoundEffect)) as ResourceService<SoundEffect>;
            gsc.AddService(typeof(IResourceService<SoundEffect>), Sounds);

            Models = GetService(typeof(Model)) as ResourceService<Model>;
            gsc.AddService(typeof(IResourceService<Model>), Models);

            Fonts = GetService(typeof(SpriteFont)) as ResourceService<SpriteFont>;
            gsc.AddService(typeof(IResourceService<SpriteFont>), Fonts);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Textures.LoadAllResources();
            Models.LoadAllResources();
            Effects.LoadAllResources();
            Sounds.LoadAllResources();
            Fonts.LoadAllResources();

            base.LoadContent();
        }

        public static ResourceManager GetInstance()
        {
            return Instance;
        }

        #endregion

        #region IServiceProvider

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(Texture2D))
            {
                return new ResourceService<Texture2D>(ContentResourceType.Texture, contentManager);
            }
            else if (serviceType == typeof(Model))
            {
                return new ResourceService<Model>(ContentResourceType.Model, contentManager);
            }
            else if (serviceType == typeof(SoundEffect))
            {
                return new ResourceService<SoundEffect>(ContentResourceType.Sound, contentManager);
            }
            else if (serviceType == typeof(Effect))
            {
                return new ResourceService<Effect>(ContentResourceType.Effect, contentManager);
            }
            else if (serviceType == typeof(SpriteFont))
            {
                return new ResourceService<SpriteFont>(ContentResourceType.Font, contentManager);
            }

            return null;
        }

        #endregion

        #region private

        ContentManager contentManager;

        GameServiceContainer gsc;

        #endregion
    }
}