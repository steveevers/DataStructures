using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miscellaneous;

namespace Graphs
{
    public static class Utilities
    {
        public static int RandomNode<T>(this IGraph<T> graph)
        {
            return ThreadLocalRandom.Next(graph.Nodes.Count - 1);
        }

        public static void AddNodes<T>(this IGraph<T> graph, IEnumerable<T> items)
        {
            foreach (var i in items)
                graph.AddNode(i);
        }
    }
}
