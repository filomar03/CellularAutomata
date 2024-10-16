using CellularAutomata.Life;
using System.Numerics;

namespace CellularAutomata
{
    internal class Program
    {
        const int MIN_FRAME_TIME = 50;
        static async Task Main()
        {
            Console.CursorVisible = false;

            CellAutomata copperhead = new ConwaysLife(
                ".....#.##...\n" +
                "....#......#\n" +
                "...##...#..#\n" +
                "##.#.....##.\n" +
                "##.#.....##.\n" +
                "...##...#..#\n" +
                "....#......#\n" +
                ".....#.##...",
                0, 1, 56, 28);

            CellAutomata glider = new ConwaysLife(
                "..#\n" +
                "#.#\n" +
                ".##",
                0, 0, 56, 28);

            CellAutomata gospelGliderGun = new ConwaysLife(
                ".......................@@\n" +
                ".......................@.@\n" +
                "..........@.@.............@.......@@\n" +
                ".........@..@..@@......@..@.......@@\n" +
                "@@......@@.....@.@........@\n" +
                "@@....@@.......@...@...@.@\n" +
                "........@@.....@@@.@@..@@\n" +
                ".........@..@...@@\n" +
                "..........@.@\n",
                0, 0, 56, 28);

            CellAutomata rngLife = new ConwaysLife(56, 28);

            CellAutomata ca = gospelGliderGun;

            while (true)
            {
                Task timer = Task.Delay(MIN_FRAME_TIME);

                string dipslay = ca .SimulationAsFormattedString();
                Console.WriteLine(dipslay);
                Console.SetCursorPosition(0, 0);
                
                ca.Tick();
                
                await timer;
            }
        }
    }
}
