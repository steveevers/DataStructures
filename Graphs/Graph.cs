using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public abstract class Graph<T> : IGraph<T>
    {
        public IList<T> Nodes { get; protected set; }
        public bool IsDirected { get; protected set; }
        public bool IsWeighted { get; protected set; }

        protected Tuple<int, int> GetNodePair(T from, T to)
        {
            int fromIndex = -1;
            int toIndex = -1;

            for (int i = 0; i < this.Nodes.Count; i++)
            {
                if (this.Nodes[i].Equals(from))
                    fromIndex = i;

                if (this.Nodes[i].Equals(to))
                    toIndex = i;

                if (fromIndex >= 0 && toIndex >= 0)
                    break;
            }

            return Tuple.Create(fromIndex, toIndex);
        }

        public abstract bool EdgeExists(int from, int to);
        public abstract bool EdgeExists(T from, T to);

        public abstract float EdgeWeight(int from, int to);
        public abstract float EdgeWeight(T from, T to);

        public abstract void AddEdge(int from, int to, float weight = -1);
        public abstract void AddEdge(T from, T to, float weight = -1);

        public abstract void RemoveEdge(int from, int to);
        public abstract void RemoveEdge(T from, T to);

        public virtual void AddNode(T n)
        {
            this.Nodes.Add(n);
        }

        public virtual void RemoveNode(int n)
        {
            this.Nodes.RemoveAt(n);
        }

        public virtual void RemoveNode(T n)
        {
            this.Nodes.Remove(n);
        }

        public abstract IEnumerable<int> Neighbors(int n);
        public abstract IEnumerable<int> Neighbors(T n);
    }
}
