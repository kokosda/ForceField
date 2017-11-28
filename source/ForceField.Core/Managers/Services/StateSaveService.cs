using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ForceField.Domain;
using System.Threading.Tasks;
using ForceField.Interfaces;

namespace ForceField.Core.Managers.Services
{
    public class StateSaveService : IStateSaveService
    {
        public BackgroundWorker Worker { get { return worker; } }
        public bool SaveGame { get; set; }

        public StateSaveService()
        {
            worker = new BackgroundWorker();
            list = new List<SaveTask>();
            SaveGame = false;
        }

        public void AddToSave(object data, string file)
        {
            list.Add(new SaveTask(data, file));
        }

        public void Save(object data, string file)
        {
            // todo
        }

        public void SaveAll()
        {
            worker.RunWorkerAsync(list);
        }


        private void AsyncSave(object sender, DoWorkEventArgs args)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            List<SaveTask> list = args.Argument as List<SaveTask>;

            if (worker.CancellationPending)
            {
                args.Cancel = true;
            }

            foreach(SaveTask save in list)
            {
                BinaryFormatter binary = new BinaryFormatter();

                using (Stream file = new FileStream(save.File, FileMode.OpenOrCreate))
                {
                    binary.Serialize(file, save.Data);
                }
            }
            list.Clear();
            worker.CancelAsync();
        }

        private List<SaveTask> list;
        private BackgroundWorker worker;
    }
}
