using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEditor.Domain;
using System.Windows.Forms;

namespace GameEditor.Interfaces
{
    public interface IToolUnitService : IToolService
    {
        ToolEventCreate GetCreateFunction();
    }
}
