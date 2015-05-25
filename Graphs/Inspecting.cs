using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public static class Inspecting
    {
        public static bool IsConnected<T>(this IGraph<T> graph)
        {
            var start = graph.RandomNode();
            var open = new Queue<int>();
            var closed = new HashSet<int>();

            open.Enqueue(start);
            closed.Add(start);

            while (open.Any())
            {
                var i = open.Dequeue();
                foreach (var n in graph.Neighbors(i))
                {
                    if (closed.Add(n))
                        open.Enqueue(n);
                }
            }

            return closed.Count == graph.Nodes.Count;
        }

        public static bool IsComplete<T>(this IGraph<T> graph)
        {
            for (int i = 0; i < graph.Nodes.Count; i++)
            {
                // number of connections for each node must be greater or equal to the number of nodes in the graph - 1 to account for self-connected nodes
                var neighbourCount = graph.Neighbors(i).Count();
                if (neighbourCount < graph.Nodes.Count - 1)
                    return false;
            }

            return true;
        }

        public static bool IsAcyclic<T>(this IGraph<T> graph)
        {
            if (!graph.IsDirected)
                return false;

            var visited = new HashSet<int>();
            var open = new Queue<int>();

            var start = graph.RandomNode();
            open.Enqueue(start);
            visited.Add(start);

            while (open.Any())
            {
                var i = open.Dequeue();

                foreach (var n in graph.Neighbors(i))
                {
                    if (!visited.Add(n))
                        return false;

                    open.Enqueue(n);
                }
            }

            return false;
        }
    }
}
