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
            if (graph is IUndirectedGraph<T>)
                return ((IUndirectedGraph<T>)graph).IsConnected();
            else
                throw new NotSupportedException();
        }

        public static bool IsConnected<T>(this IUndirectedGraph<T> graph)
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

        public static bool IsComplete<T>(this IUndirectedGraph<T> graph)
        {
            for(int i = 0; i < graph.Nodes.Count; i++)
            {
                // number of connections for each node must be greater or equal to the number of nodes in the graph - 1 to account for self-connected nodes
                if (graph.Neighbors(i).Count() >= graph.Nodes.Count - 1)
                    return false;
            }

            return true;
        }
    }
}
