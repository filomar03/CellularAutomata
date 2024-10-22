//#define LIMIT_FPS
//#define TIME_IT

using CellularAutomata.Life;
using System.Diagnostics;

namespace CellularAutomata
{
    internal class Program
    {
        //const declarations
#if LIMIT_FPS || TIME_IT
        const int MIN_FRAME_TIME = 100;
#endif
#if TIME_IT
        const double SMOOTHIN_TIME = 1000;
        const int SMOOTHING_FPS = (int) (SMOOTHIN_TIME / MIN_FRAME_TIME);
#endif

        //main
#if LIMIT_FPS
        static async Task Main()
#else
        static void Main()
#endif
        {
            Console.CursorVisible = false;

            //default theme
            LifeTheme theme = new();

            //known patterns
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

            //async wait task to limit max fps
#if LIMIT_FPS
            Task frameTimer;
#endif
#if TIME_IT
            //variables to time execution
            double _avgTickTime = 0, _avgFormatTime = 0, _avgPrintTime = 0;
            double avgTickTime = 0, avgFormatTime = 0, avgPrintTime = 0;
            ulong frameCounter = 0;
#endif
            CellAutomata ca = rngLife;

            Stopwatch clock = Stopwatch.StartNew();

            while (true)
            {
#if LIMIT_FPS
                frameTimer = Task.Delay(MIN_FRAME_TIME);
#endif
#if TIME_IT
                Console.Write($"Clock:{(int)clock.Elapsed.TotalSeconds}s; ");
           
                //format to string
                string info = $"Time:{(int) clock.Elapsed.TotalSeconds}s; " +
                    $"Tick:{avgTickTime:F3}ms; " +
                    $"Format:{avgFormatTime:F3}ms; " +
                    $"Print:{avgPrintTime:F3}ms" +
                    " ".Repeat(5); //trailing spaces are added to override the console buffer, since no cleaning
                string display = "";
                _avgFormatTime += MyExtensions.TimeExecution(() =>
                {
                    display = ca.SimulationAsFormattedString();
                }).TotalMilliseconds;              

                //print to string
                Console.WriteLine(info);
                _avgPrintTime += MyExtensions.TimeExecution(() => Console.WriteLine(display)).TotalMilliseconds;
                Console.SetCursorPosition(0, 0);

                //tick
                _avgTickTime += MyExtensions.TimeExecution(ca.Tick).TotalMilliseconds;

                //calculating execution times
                if (frameCounter % SMOOTHING_FPS == 0)
                {
                    avgTickTime = _avgTickTime / SMOOTHING_FPS;
                    _avgTickTime = 0;
                    avgFormatTime = _avgFormatTime / SMOOTHING_FPS;
                    _avgFormatTime = 0;
                    avgPrintTime = _avgPrintTime / SMOOTHING_FPS;
                    _avgPrintTime = 0;
                }
                frameCounter++;
#else
                string display = ca.SimulationAsFormattedString();
                
                Console.WriteLine(display);
                Console.SetCursorPosition(0, 0);

                ca.Tick();
#endif
#if LIMIT_FPS
                //wait for timer so every frame takes at least MIN_FRAME_TIME ms to comeplete
                await frameTimer;
#endif
            }
        }
    }
}
