using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace ForceField.Domain.Content
{
    public class ContentResource<T> : Entity
    {
        public ContentResource(string resourcePath, T data)
        {
            this.resourcePath = resourcePath;
            Name = System.IO.Path.GetFileNameWithoutExtension(resourcePath);
            this.data = data;
            contentType = ContentResourceType.Undefined;
        }

        public ContentResource(string resourcePath, ContentResourceType contentType, T data)
        {
            this.resourcePath = resourcePath;
            Name = System.IO.Path.GetFileNameWithoutExtension(resourcePath);
            this.contentType = contentType;
            this.data = data;
        }

        #region Properties

        public string ResourcePath
        {
            get
            {
                return resourcePath;
            }
        }

        public ContentResourceType ContentResourceType
        {
            get
            {
                return contentType;
            }
        }

        public T Data
        {
            get
            {
                return data;
            }
        }

        #endregion

        #region public Methods

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region private

        private string resourcePath;

        private ContentResourceType contentType;

        private T data;

        #endregion
    }
}
