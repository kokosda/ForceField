using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Microsoft.Xna.Framework;

namespace ForceField.Interfaces
{
    public interface IScriptService
    {
        IList<Assembly> Assemblies { get; }

        void AddActiveScript(IScript script);

        void DeleteActiveScript(IScript script);

        void ClearActiveScripts();

        void Compile(string code, out string[] result);

        IScript GetScript(string name);

        IScript GetScript(string name, params object[] args);

        void Update(GameTime time);
    }
}
