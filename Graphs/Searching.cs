using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Graphs
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
            var queue = new Queue<int>(new List<int> { a });

            while (queue.Any())
            {
                var i = queue.Dequeue();
                
                foreach (var n in graph.Neighbors(i))
                {
                    if (n == b)
                        return true;

                    if (visited.Add(n))
                        queue.Enqueue(n);
                }
            }

            return false;
        }

        public static bool BreadthFirstSearch<T>(this IGraph<T> graph, int a, int b)
        {
            var visited = new HashSet<int>();
            var stack = new Stack<int>(new List<int> { a });

            while (stack.Any())
            {
                var i = stack.Pop();

                foreach (var n in graph.Neighbors(i))
                {
                    if (n == b)
                        return true;

                    if (visited.Add(n))
                        stack.Push(n);
                }
            }

            return false;
        }
    }
}
