using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

using ForceField.Domain.Content;
using ForceField.Interfaces;
using System.Reflection;

namespace ForceField.Core.Services
{
    public class ResourceService<T> : IResourceService<T>
    {
        public ResourceService(ContentResourceType contentType, ContentManager contentManager)
        {
            currentResourceType = contentType;
            this.contentManager = contentManager;
        }

        #region Properties

        public IList<ContentResource<T>> Resources
        {
            get
            {
                return resources;
            }
        }

        public ContentResourceType CurrentResourceType
        {
            get
            {
                return currentResourceType;
            }
        }

        #endregion

        #region public Methods

        public ContentResource<T> GetByName(string name)
        {
            return Resources.First(p => p.Name == name);
        }

        public ContentResource<T> Get(int index)
        {
            return resources[index];
        }

        public void LoadAllResources()
        {
            if (ContentFolders.Length > 0 && !ContentFolders.Contains(string.Empty))
            {
                LoadAllResources(contentManager.RootDirectory);
            }
        }

        public override string ToString()
        {
            return string.Format("Resources<{0}> = {1}", typeof(T).Name, Resources != null ? Resources.Count : 0);
        }

        #endregion

        #region private

        private void LoadAllResources(string rootDirectory, bool isRelativeRoot = true)
        {
            string[] contentFolders = null;

            if (isRelativeRoot)
            {
                contentFolders = ContentFolders;
            }
            else
            {
                contentFolders = new DirectoryInfo(rootDirectory).GetDirectories().Select(p => p.Name).ToArray();
            }

            if (contentFolders.Count() > 0)
            {
                foreach (var contentFolder in contentFolders)
                {
                    var path = Path.Combine(rootDirectory, contentFolder);

                    LoadAllResources(path, false);
                }
            }

            var files = new DirectoryInfo(rootDirectory).GetFiles();
            string[] relativeFilePathes = new string[files.Count()];
            for(var i = 0; i < files.Length; i++)
            {
                var path = rootDirectory.Replace(string.Format(@"{0}\", contentManager.RootDirectory), string.Empty);
                relativeFilePathes[i] = Path.Combine(path, Path.GetFileNameWithoutExtension(files[i].Name));
            }

            AssignContentResourceList(relativeFilePathes);
        }

        private void AssignContentResourceList(string[] relativeFilePathes)
        {
            foreach (var filePath in relativeFilePathes)
            {
                resources.Add(new ContentResource<T>(filePath, CurrentResourceType, contentManager.Load<T>(filePath)));
            }
        }

        private string[] ContentFolders
        {
            get
            {
                if (typeof(T) == typeof(Texture2D))
                {
                    return Depository.TexturesFolders;
                }
                else if (typeof(T) == typeof(SoundEffect))
                {
                    return Depository.SoundsFolders;
                }
                else if (typeof(T) == typeof(Model))
                {
                    return Depository.ModelsFolders;
                }
                else if (typeof(T) == typeof(Effect))
                {
                    return Depository.EffectsFolders;
                }
                else if (typeof(T) == typeof(SpriteFont))
                {
                    return Depository.FontsFolders;
                }
                else
                {
                    return Depository.BinariesFolders;
                }
            }
        }
        
        #region private members

        private List<ContentResource<T>> resources = new List<ContentResource<T>>();

        private ContentResourceType currentResourceType = ContentResourceType.Undefined;

        private ContentManager contentManager;

        #endregion

        #endregion
    }
}
