using Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public static class AStar
    {
        public static IEnumerable<int> FindPath<T>(this IGraph<T> graph, int from, int to, Func<int, int, double> distance, Func<int, int, double> heuristic)
        {
            var closed = new HashSet<int>();
            var open = new PriorityQueue<double, Path>();
            open.Enqueue(0, new Path(from));

            while (!open.IsEmpty)
            {
                var path = open.Dequeue();
                if (closed.Contains(path.Last))
                    continue;

                if (path.Last.Equals(to))
                    return path;

                closed.Add(path.Last);
                foreach (var n in graph.Neighbors(path.Last))
                {
                    double d = distance(path.Last, n);
                    var p = path.Add(n, d);
                    open.Enqueue(p.Cost + heuristic(n, to), p);
                }
            }

            return new List<int>();
        }
    }

    public class Path : IEnumerable<int>
    {
        public int Last { get; private set; }
        public Path Previous { get; private set; }
        public double Cost { get; private set; }

        private Path(int last, Path previous, double cost)
        {
            this.Last = last;
            this.Previous = previous;
            this.Cost = cost;
        }

        public Path(int start) : this(start, null, 0) { }

        public Path Add(int step, double cost)
        {
            return new Path(step, this, this.Cost + cost);
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (Path p = this; p != null; p = p.Previous)
                yield return p.Last;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
