using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata.Life
{
    internal class ConwaysLife : CellAutomata
    {
        private bool[] world;
        private readonly int width, height;

        private const string brightSymbols = "M@#&%$*";
        private const string darkSymbols = "_. ";
        private readonly char liveChar, deadChar;
        private readonly bool spaced;

        public ConwaysLife(int width, int height, bool spaced = true)
        {
            this.width = width;
            this.height = height;
            world = new bool[width * height];

            Random rng = new();

            for (int i = 0; i < world.Length; i++) //initialize randomly
            {
                world[i] = rng.Next(0, 2) == 1;
            }

            liveChar = brightSymbols[rng.Next(0, brightSymbols.Length)]; //chose random "colors"
            deadChar = darkSymbols[rng.Next(0, darkSymbols.Length)];
            this.spaced = spaced;
        }

        public ConwaysLife(string startConfig, int left, int top, int width, int height, bool spaced = true) : this(width, height, spaced)
        {
            if (!IsConfigValid(startConfig)) //check if startConfig contains some illegal characters
            {
                throw new ArgumentException("Illegal starting configuration");
            }

            IList<string> lines = startConfig.Split('\n');
            if (top + lines.Count > height) //check id startConfig lines exceed world height
            {
                throw new ArgumentException("Illegal starting configuration");
            }

            world = new bool[width * height];
            for (int i = 0; i < world.Length; ++i)
            {
                int x = i % this.width;
                int y = i / this.width;

                if (y >= top && y < top + lines.Count) //check if at the height of one of the startConfig lines (y coord)
                { 
                    if (x == 0 && left + lines[y - top].Length > width) //check if each line exceeds world width (short-circuit to check once every line)
                    {
                        throw new ArgumentException("Illegal starting configuration");
                    }

                    if (x >= left && x < left + lines[y - top].Length) //check if inside one of the startConfig lines (x coord)
                    {
                        world[i] = brightSymbols.Contains(lines[y - top][x - left]); //set cell state according to startConfig
                    }
                }
            }
        }

        public (int, int) Size { get => (width, height); }

        private static bool IsConfigValid(string config) => config.All((brightSymbols + darkSymbols + "\n").Distinct().Contains);

        protected override void PerformTick()
        {
            bool[] newWorld = new bool[width * height];

            (int, int)[] nbrRel = [(-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1),];
            for (int i = 0; i < world.Length; i++)
            {
                int x = i % width;
                int y = i / width;

                int nbrsCount = 0;
                foreach (var (nbrRelX, nbrRelY) in nbrRel)
                {
                    int nbrX = nbrRelX + x;
                    int nbrY = nbrRelY + y;

                    //prevents from considering neighborn horizontal-wrapped cells and also prevents from under/over flowing world array vertically
                    if (nbrX >= 0 && nbrX < width && nbrY >= 0 && nbrY < height) 
                    {
                        nbrsCount += world[nbrY * width + nbrX] ? 1 : 0;
                    }
                }

                if (world[i] && nbrsCount is not (2 or 3)) //if living and neighborns is not 2 or 3, then die
                {
                    newWorld[i] = false;
                }
                else if (!world[i] && nbrsCount is 3) //if dead and neighborns is 3, then generate
                {
                    newWorld[i] = true;
                } else
                {
                    newWorld[i] = world[i]; //otherwise mantain state
                }
            }

            world = newWorld;
        }

        public override string SimulationAsFormattedString()
        {
            StringBuilder sb = new();

            sb.Append($"[Generation:{Generation} Size:{Size.Item1}x{Size.Item2} Population:{world.Count(e => e)}]");

            for (int i = 0; i < world.Length; i++)
            {
                int x = i % width;
                if (x == 0)
                {
                    sb.Append('\n');
                }

                sb.Append(world[i] ? liveChar : deadChar);
                if (spaced)
                {
                    sb.Append(' ');
                }
            }
            return sb.ToString();
        }
    }

    internal class WrappedLife
    {
        //Conways game of life implementation, but calculating neighborns with %, so it has a wrapping behaviour
    }
}
