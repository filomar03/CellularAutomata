using CellularAutomata.Life;
using System.Numerics;

namespace CellularAutomata
{
    internal class Program
    {
        const int MIN_FRAME_TIME = 250;
        static async Task Main()
        {
            Console.CursorVisible = false;

            CellAutomata life = new ConwaysLife("MMM", 0, 1, 3, 3);
         
            while (true)
            {
                string dipslay = life.SimulationAsFormattedString();
                Console.WriteLine(dipslay);
                Console.SetCursorPosition(0, 0);

                Task timer = Task.Delay(MIN_FRAME_TIME);
                
                life.Tick();
                
                await timer;
            }
        }
    }
}
