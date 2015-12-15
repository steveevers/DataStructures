using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Graphs
{
    public class AdjacencyList<T> : Graph<T>
    {
        private Dictionary<int, HashSet<int>> edges = new Dictionary<int, HashSet<int>>();

        public AdjacencyList(bool isDirected)
        {
            this.Nodes = new List<T>();

            this.IsDirected = isDirected;
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

        protected override void AddNodeByType(T n)
        {
            this.edges.Add(this.Nodes.Count - 1, new HashSet<int>());
        }

	protected override void RemoveNodeByType(T n) 
	{
		foreach (var kvp in this.edges) 
		{
			kvp.Value.RemoveWhere(i => i.Equals(n));
		}

		this.edges.Remove(this.Nodes.IndexOf(n));
	}

	protected override void AddEdgeByIndex(int from, int to, float weight)
        {
            this.edges[from].Add(to);
            if (!this.IsDirected)
                this.edges[to].Add(from);
        }

        protected override void AddEdgeByType(T from, T to, float weight)
        {
        	var pair = this.GetNodePair(from, to);
		this.AddEdgeByIndex(pair.Item1, pair.Item2, weight);
        }

        protected override void RemoveEdgeByIndex(int from, int to)
        {
            this.edges[from].Remove(to);
            if (!this.IsDirected)
                this.edges[to].Remove(from);
        }

	protected override void RemoveEdgeByType(T from, T to)
        {
            var pair = this.GetNodePair(from, to);
			this.RemoveEdgeByIndex(pair.Item1, pair.Item2);
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
