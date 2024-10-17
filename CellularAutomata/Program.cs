using CellularAutomata.Life;
using System.Numerics;

namespace CellularAutomata
{
    internal class Program
    {
        const int MIN_FRAME_TIME = 100;

        //TODO:
        //- check if dynamic invoke is vorking
        //- add interactibility to allow to capture keys and interact with simulation (pause, etc...)
        //- modify TimeExecution to support all type of functions (not sure it's doable)
        //- implement infinite life
        //- implement hexagonal cellular automata
        //- implement basic game with networking

        static async Task Main()
        {
            Console.CursorVisible = false;

            //default theme
            LifeTheme theme = new();

            //patterns
            CellAutomata copperhead = new ConwaysLife(
                ".....#.##...\n" +
                "....#......#\n" +
                "...##...#..#\n" +
                "##.#.....##.\n" +
                "##.#.....##.\n" +
                "...##...#..#\n" +
                "....#......#\n" +
                ".....#.##...",
                0, 1, 56, 28, theme);

            CellAutomata glider = new ConwaysLife(
                "..#\n" +
                "#.#\n" +
                ".##",
                0, 0, 56, 28, theme);

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
                0, 0, 56, 28, theme);

            CellAutomata rngLife = new ConwaysLife(Console.BufferWidth / 2, Console.WindowHeight - 2, theme);

            //timers for timing function executions
            TimeSpan tickTime = TimeSpan.FromMicroseconds(0);
            //TimeSpan stringTime = TimeSpan.FromMicroseconds(0);
            TimeSpan printTime = TimeSpan.FromMicroseconds(0);

            CellAutomata ca = rngLife;
            while (true)
            {
                Task timer = Task.Delay(MIN_FRAME_TIME);

                string info = $"Tick time: {tickTime.TotalMilliseconds}ms, Print time: {printTime.TotalMilliseconds}ms";
                info = info + " ".Repeat(Console.BufferWidth - info.Length);
                string display = ca.SimulationAsFormattedString();
                int firstLine = display.IndexOf('\n');
                display = display.Insert(firstLine, "".Repeat(Console.BufferWidth - firstLine));
                
                Console.WriteLine(info);
                printTime = MyExtensions.TimeExecution(Console.WriteLine, display);
                Console.SetCursorPosition(0, 0);

                tickTime = MyExtensions.TimeExecution(ca.Tick);
                
                await timer;
            }
        }
    }
}
