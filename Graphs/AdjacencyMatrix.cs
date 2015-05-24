using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class AdjacencyMatrix<T> : Graph<T>
    {
        private float[,] adjacency;

        private Action<int, int, float> addEdge;
        private Action<int, int> removeEdge;

        public AdjacencyMatrix(bool isDirected, bool isWeighted, int size)
        {
            this.Nodes = new List<T>(size);
            this.adjacency = new float[size, size];
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                for (int j = 0; j < this.Nodes.Count; j++)
                {
                    this.adjacency[i, j] = -1;
                }
            }

            this.IsDirected = IsDirected;
            this.IsWeighted = IsWeighted;

            if (this.IsDirected)
            {
                this.addEdge = this.addEdgeDirected;
                this.removeEdge = this.removeEdgeDirected;
            }
            else
            {
                this.addEdge = this.addEdgeUndirected;
                this.removeEdge = this.removeEdgeUndirected;
            }
        }

        public override bool EdgeExists(int from, int to)
        {
            return this.adjacency[from, to] >= 0;
        }

        public override bool EdgeExists(T from, T to)
        {
            var pair = this.GetNodePair(from, to);
            return EdgeExists(pair.Item1, pair.Item2);
        }

        public override float EdgeWeight(int from, int to)
        {
            return this.adjacency[from, to];
        }

        public override float EdgeWeight(T from, T to)
        {
            var pair = this.GetNodePair(from, to);
            return EdgeWeight(pair.Item1, pair.Item2);
        }

        #region AddEdge

        public override void AddEdge(int from, int to, float weight)
        {
            this.addEdge(from, to, weight);
        }

        public override void AddEdge(T from, T to, float weight = -1)
        {
            var pair = this.GetNodePair(from, to);
            this.addEdge(pair.Item1, pair.Item2, weight);
        }

        private void addEdgeDirected(int from, int to, float weight)
        {
            this.adjacency[from, to] = weight;
        }

        private void addEdgeUndirected(int from, int to, float weight)
        {
            this.adjacency[from, to] = weight;
            this.adjacency[to, from] = weight;
        }

        #endregion

        #region RemoveEdge

        public override void RemoveEdge(int from, int to)
        {
            this.removeEdge(from, to);
        }

        public override void RemoveEdge(T from, T to)
        {
            var pair = this.GetNodePair(from, to);
            this.removeEdge(pair.Item1, pair.Item2);
        }

        private void removeEdgeDirected(int from, int to)
        {
            this.adjacency[from, to] = -1;
        }

        private void removeEdgeUndirected(int from, int to)
        {
            this.adjacency[from, to] = -1;
            this.adjacency[to, from] = -1;
        }

        #endregion

        #region Neighbors

        public override IEnumerable<int> Neighbors(int n)
        {
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                if (this.adjacency[n, i] >= 0)
                    yield return i;
            }
        }

        public override IEnumerable<int> Neighbors(T n)
        {
            var index = this.Nodes.IndexOf(n);
            return this.Neighbors(index);
        }

        #endregion
    }
}
