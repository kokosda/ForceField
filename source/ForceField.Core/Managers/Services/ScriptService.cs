using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Globalization;

using Microsoft.Xna.Framework;

using ForceField.Interfaces;
using ForceField.Domain.Content;

namespace ForceField.Core.Managers.Services
{
    public class ScriptService : IScriptService
    {
        public IList<Assembly> Assemblies 
        {
            get
            {
                return assemblies;
            }
        }

        public ScriptService(Game game,IList<Assembly> assemblies,IList<IScript> activeScripts)
        {
            this.assemblies = assemblies;
            this.activeScripts = activeScripts;
            this.game = game;
        }

        public void AddActiveScript(IScript script)
        {
            script.Activate(game);
            activeScripts.Add(script);
        }

        public void DeleteActiveScript(IScript script)
        {
            activeScripts.Remove(script);
        }

        public void ClearActiveScripts()
        {
            activeScripts.Clear();
        }

        public void Compile(string code, out string[] result)
        {
            result = new string[] { "Complete" };
            //Заглушка
        }

        public IScript GetScript(string name)
        {
            Assembly asm = assemblies.First(p => p.GetName().Name == name);

            Type[] types = asm.GetTypes();

            Type type = types.First(p => p.Name == name);

            if (type != null)
                return Activator.CreateInstance(type) as IScript;
            else
                return null;
        }

        public IScript GetScript(string name, params object[] args)
        {
            Assembly assembly = assemblies.First(p => p.GetName().Name == name);
            return assembly.CreateInstance(name, true, BindingFlags.ExactBinding, null, args, null, null) as IScript;
        }

        public void Update(GameTime gameTime)
        {
            foreach (IScript script in activeScripts)
            {
                script.Update(gameTime);
            }
        }

        private Game game;
        private IList<IScript> activeScripts;
        private IList<Assembly> assemblies;
    }
}
