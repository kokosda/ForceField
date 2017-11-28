using ForceField.Interfaces;
using System.ComponentModel;
using ForceField.Domain.GameLogic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace ForceField.Core.Services
{
    public class LevelService : ILevelService
    {

        IList<Level> Levels { get; set; }
        private void LoadLevel(object sender, DoWorkEventArgs e)
        {
            Level Level = (Level)e.Argument;
            using (FileStream File = new FileStream(Level.Name, FileMode.Open))
            {
                File.Lock(0, File.Length);
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                Level level = (Level)BinaryFormatter.Deserialize(File);
                File.Unlock(0, File.Length);
            }
        }

        private void SaveLevel(object sender, DoWorkEventArgs e)
        {
            Level Level = (Level)e.Argument;

            using (FileStream File = new FileStream(Level.Name, FileMode.OpenOrCreate))
            {
                File.Lock(0, File.Length);
                BinaryFormatter BinaryFormatter = new BinaryFormatter();
                BinaryFormatter.Serialize(File, Level);
                File.Unlock(0, File.Length);
            }
        }

        public void Save(string path, string levelname)
        {
            BackgroundWorker BackgroundWorker = new BackgroundWorker();
            Level Level = Levels.First(lev => lev.Name == levelname);
            BackgroundWorker.DoWork += new DoWorkEventHandler(SaveLevel);
            BackgroundWorker.RunWorkerAsync(Level);
            BackgroundWorker.CancelAsync();
        }
        
        public void Load(string path)
        {
            BackgroundWorker BackgroundWorker = new BackgroundWorker();
            Level Level = new Level();
            Level.Name = path;
            BackgroundWorker.DoWork += new DoWorkEventHandler(LoadLevel);
            BackgroundWorker.RunWorkerAsync(Level);
            BackgroundWorker.CancelAsync();
            lock (Levels)
            {
                Levels.Add(Level);
            }
         }

        public LevelService(IList<Level> Levels)
        {
            this.Levels = Levels;
        }
    }
}
