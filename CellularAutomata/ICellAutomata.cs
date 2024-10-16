using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    internal interface ICellAutomata
    {
        void Tick();
        string SimulationAsFormattedString();
    }
}
