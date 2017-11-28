using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Domain.Content;

namespace ForceField.Interfaces
{
    public interface IResourceService<T>
    {
        IList<ContentResource<T>> Resources { get; }

        ContentResourceType CurrentResourceType { get; }

        ContentResource<T> GetByName(string name);

        ContentResource<T> Get(int index);

        void LoadAllResources();
    }
}
