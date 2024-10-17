using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellularAutomata.Life
{
    internal class ConwaysLife : CellAutomata
    {
        private readonly bool[] world;
        private readonly int[] nbrns;
        private readonly int width, height;

        private readonly LifeTheme theme;

        public ConwaysLife(int width, int height, LifeTheme theme)
        {
            this.width = width;
            this.height = height;
            world = new bool[width * height];
            nbrns = new int[world.Length];

            Random rng = new();

            for (int i = 0; i < world.Length; i++) //initialize randomly
            {
                world[i] = rng.Next(0, 2) == 1;
            }
            computeNeighborns();

            this.theme = theme;
        }

        public ConwaysLife(string startConfig, int left, int top, int width, int height, LifeTheme theme) : this(width, height, theme)
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
                        world[i] = (LifeTheme.defBrightSymbols + theme.Live).Contains(lines[y - top][x - left]); //set cell state according to startConfig
                    }
                }
            }
            computeNeighborns();
        }

        public (int, int) Size { get => (width, height); }

        private bool IsConfigValid(string config) => config.All(
            (LifeTheme.defBrightSymbols + theme.Live + LifeTheme.defDarkSymbols + theme.Dead + " \n").Contains);

        private void computeNeighborns()
        {
            (int, int)[] nbrnRel = [(-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1),];
            for (int i = 0; i < world.Length; i++)
            {
                int x = i % width;
                int y = i / width;

                nbrns[i] = 0;

                foreach (var (nbrRelX, nbrRelY) in nbrnRel)
                {
                    int nbrX = nbrRelX + x;
                    int nbrY = nbrRelY + y;

                    //prevents from considering neighborn horizontal-wrapped cells and also prevents from under/over flowing world array vertically
                    if (nbrX >= 0 && nbrX < width && nbrY >= 0 && nbrY < height)
                    {
                        nbrns[i] += world[nbrY * width + nbrX] ? 1 : 0;
                    }
                }
            }
        }

        protected override void PerformTick()
        {
            computeNeighborns();

            for (int i = 0; i < world.Length; i++)
            {
                if (world[i] && nbrns[i] is not (2 or 3)) //if living and neighborns is not 2 or 3, then die
                {
                    world[i] = false;
                }
                else if (!world[i] && nbrns[i] is 3) //if dead and neighborns is 3, then generate
                {
                    world[i] = true;
                }
            }
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

                sb.Append(world[i] ?  theme.Live : theme.Dead); 
                if (theme.Spaced)
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

    internal record struct LifeTheme
    {
        public const string defBrightSymbols = "M@#&%$*";
        public const string defDarkSymbols = "_. ";

        private readonly char live, dead;
        private readonly bool spaced = true;

        public LifeTheme ()
        {
            Random rng = new();

            live = rng.Choice(LifeTheme.defBrightSymbols);
            dead = rng.Choice(LifeTheme.defDarkSymbols);
        }
        public LifeTheme(bool spaced) : this()
        {
            this.spaced = spaced;
        }

        public LifeTheme(char live, char dead, bool spaced = true)
        { 
            this.live = live;
            this.dead = dead;
            this.spaced = spaced;
        }

        public readonly char Live => live;
        public readonly char Dead => dead;
        public readonly bool Spaced => spaced;
    }
}
