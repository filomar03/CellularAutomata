using System.Diagnostics;
using System.Text;

namespace CellularAutomata
{
    static class MyExtensions
    {
        public static string Repeat(this string str, int n)
        {
            StringBuilder sb = new();
            for (int i = 0; i < n; i++)
            {
                sb.Append(str);
            }
            return sb.ToString();
        }

        public static T Choice<T>(this Random rng, IEnumerable<T> collection) => collection.ElementAt(rng.Next(collection.Count()));

        public static TimeSpan TimeExecution(Action action)
        {
            Stopwatch sw = Stopwatch.StartNew();

            action();

            sw.Stop();
            return sw.Elapsed;
        }
    }
}
