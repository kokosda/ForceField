using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Domain.GameLogic;

namespace ForceField.Interfaces
{
    public interface ILevelService
    {
        void Save(string path, string levelname);
        void Load(string path);
    }
}
