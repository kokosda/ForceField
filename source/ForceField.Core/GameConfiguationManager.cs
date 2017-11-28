using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ForceField.Domain.Renderer.Base;
using ForceField.Domain.Renderer;
using ForceField.Domain.GameLogic;
using ForceField.Domain.GameLogic.Comparers;
using ForceField.Core.Services;
using ForceField.Interfaces;
using System.Configuration;

namespace ForceField.Core
{
    public static class GameConfiguationManager
    {
        public static void Write(string index, string value)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration("App.config");
            config.AppSettings.Settings.Add(index, value);

            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static string Read(string index)
        {
            return ConfigurationManager.AppSettings.Get(index);
        }
    }
}