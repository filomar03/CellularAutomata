using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata.Life
{
    //Conways game of life but with faster Tick() implementation
    //using sparse list to store only living cells
    internal class FastLife : CellAutomata
    {
        public override string SimulationAsFormattedString()
        {
            throw new NotImplementedException();
        }

        protected override void PerformTick()
        {
            throw new NotImplementedException();
        }
    }

    //Conways game of life but with "infinite" space implementation
    internal class InfiniteLife : CellAutomata
    {
        public override string SimulationAsFormattedString()
        {
            throw new NotImplementedException();
        }

        protected override void PerformTick()
        {
            throw new NotImplementedException();
        }
    }
}
