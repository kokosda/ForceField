using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ForceField.Core.Managers.Services;
using Microsoft.Xna.Framework;
using ForceField.Interfaces;
using System.IO;
using System.CodeDom.Compiler;

namespace ForceField.Core.Managers
{
    public class ScriptManager : IGameComponent
    {
        public ScriptManager(Game game,IList<Assembly> assemblies,string isCompile)
        {            
            this.game = game;
            game.Components.Add(this);
            scriptService = new ScriptService(game ,assemblies,new List<IScript>());
            game.Services.AddService(typeof(IScriptService), scriptService);
            this.isCompile = isCompile;
        }

        public void Initialize()
        {
            LoadScripts("Content//Scripts//");
        }

        private void LoadScripts(string from)
        {
            var files = new DirectoryInfo(from).GetFiles();
            IList<Assembly> asm = scriptService.Assemblies;

            bool compile = false;

            bool.TryParse(isCompile, out compile);

            if (compile == false)
            {
                foreach (FileInfo file in files)
                {
                    if (file.Extension == ".dll")
                    {
                        Assembly assembly = Assembly.LoadFrom(file.FullName);
                        asm.Add(assembly);
                    }
                }
            }
            else
            {
                foreach (FileInfo file in files)
                {
                    if (file.Extension == ".cs")
                    {
                        Assembly assembly = null;
                        CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

                        string name = file.Name.Replace(".cs", "");

                        CompilerParameters cp = new CompilerParameters
                        {
                            GenerateExecutable = false,
                            GenerateInMemory = false,
                            TreatWarningsAsErrors = false,
                            OutputAssembly = from + name + ".dll"
                        };

                        Assembly executingAssembly = Assembly.GetExecutingAssembly();
                        cp.ReferencedAssemblies.Add(executingAssembly.Location);
                        foreach (AssemblyName assemblyName in executingAssembly.GetReferencedAssemblies())
                        {
                            cp.ReferencedAssemblies.Add(Assembly.Load(assemblyName).Location);
                        }

                        using (Stream stream = File.Open(from + file.Name, FileMode.Open))
                        {
                            using (TextReader reader = new StreamReader(stream))
                            {
                                CompilerResults cr = provider.CompileAssemblyFromSource(cp, reader.ReadToEnd());

                                foreach (CompilerError e in cr.Errors)
                                {
                                    //Временный дебаг
                                    Console.WriteLine(e.ErrorText);
                                }

                                assembly = cr.CompiledAssembly;
                            }
                        }

                        asm.Add(assembly);
                    }
                }
            }
        }

        string isCompile;
        Game game;
        ScriptService scriptService;
    }
}
