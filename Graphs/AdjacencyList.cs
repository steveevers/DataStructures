using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class AdjacencyList<T> : Graph<T>
    {
        private Dictionary<int, HashSet<int>> edges = new Dictionary<int, HashSet<int>>();

        public AdjacencyList(bool isDirected)
        {
            this.Nodes = new List<T>();

            this.IsDirected = IsDirected;
            this.IsWeighted = false;
        }

        public override bool EdgeExists(int from, int to)
        {
            return this.edges[from].Contains(to);
        }

        public override bool EdgeExists(T from, T to)
        {
            var pair = this.GetNodePair(from, to);
            return this.EdgeExists(pair.Item1, pair.Item2);
        }

        public override float EdgeWeight(int from, int to)
        {
            return 0;
        }

        public override float EdgeWeight(T from, T to)
        {
            return 0;
        }

        public override void AddNode(T n)
        {
            this.Nodes.Add(n);
            this.edges.Add(this.Nodes.Count - 1, new HashSet<int>());
        }

        public override void AddEdge(int from, int to, float weight = -1)
        {
            this.edges[from].Add(to);
            if (!this.IsDirected)
                this.edges[to].Add(from);
        }

        public override void AddEdge(T from, T to, float weight = -1)
        {
            var pair = this.GetNodePair(from, to);
            this.AddEdge(pair.Item1, pair.Item2, weight);
        }

        public override void RemoveEdge(int from, int to)
        {
            this.edges[from].Remove(to);
            if (!this.IsDirected)
                this.edges[to].Remove(from);
        }

        public override void RemoveEdge(T from, T to)
        {
            var pair = this.GetNodePair(from, to);
            this.RemoveEdge(pair.Item1, pair.Item2);
        }

        public override IEnumerable<int> Neighbors(int n)
        {
            return this.edges[n];
        }

        public override IEnumerable<int> Neighbors(T n)
        {
            return this.Neighbors(this.Nodes.IndexOf(n));
        }
    }
}
