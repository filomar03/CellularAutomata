using CellularAutomata.Life;
using System.Numerics;

namespace CellularAutomata
{
    internal class Program
    {
        const int MIN_FRAME_TIME = 100;

        //TODO:
        //- decouple text-based graphic (extract to an interface), add options to constructor to chose graphics settings (maybe create a struct to represent them)
        //- understand multithreading
        //- add interactibility to allow to capture keys and interact with simulation (pause, etc...)
        //- implement infinite life
        //- implement hexagonal cellular automata
        //- implement basic game with networking

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

            CellAutomata ca = rngLife;

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
