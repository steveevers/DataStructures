using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public static class Searching
    {
        public enum SearchType { BFS, DFS }

        public static bool Search<T>(this IGraph<T> graph, int a, int b, SearchType method)
        {
            switch (method)
            {
                case SearchType.BFS:
                    return graph.BreadthFirstSearch(a, b);
                case SearchType.DFS:
                    return graph.DepthFirstSearch(a, b);
                default:
                    throw new NotSupportedException();
            }
        }

        public static bool DepthFirstSearch<T>(this IGraph<T> graph, int a, int b)
        {
            var visited = new HashSet<int>();
            var queue = new Queue<int>();

            visited.Add(a);
            queue.Enqueue(a);

            while (queue.Any())
            {
                var i = queue.Dequeue();
                if (i == b)
                    return true;

                foreach (var n in graph.Neighbors(i))
                {
                    if (visited.Add(n))
                        queue.Enqueue(n);
                }
            }

            return false;
        }

        public static bool BreadthFirstSearch<T>(this IGraph<T> graph, int a, int b)
        {
            var visited = new HashSet<int>();
            var stack = new Stack<int>();

            visited.Add(a);
            stack.Push(a);

            while (stack.Any())
            {
                var i = stack.Pop();
                if (i == b)
                    return true;

                foreach (var n in graph.Neighbors(i))
                {
                    if (visited.Add(n))
                        stack.Push(n);
                }
            }

            return false;
        }
    }
}
