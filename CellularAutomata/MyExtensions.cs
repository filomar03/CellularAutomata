using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static TimeSpan TimeExecution<T>(Delegate action, params T[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();

            action.DynamicInvoke(args);

            sw.Stop();
            return sw.Elapsed;
        }
    }
}
