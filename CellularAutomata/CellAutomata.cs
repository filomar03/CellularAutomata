using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata
{
    internal abstract class CellAutomata : ICellAutomata
    {
        private uint generation;

        public uint Generation { get => generation; }

        public void Tick()
        {
            PerformTick();
            generation++;
        }

        protected abstract void PerformTick();

        public abstract string SimulationAsFormattedString();

        public override string ToString() => SimulationAsFormattedString();
    }
}
