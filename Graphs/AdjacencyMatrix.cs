using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Graphs
{
    public class AdjacencyMatrix<T> : Graph<T>
    {
        private float[,] edges;

        private Action<int, int, float> addEdge;
        private Action<int, int> removeEdge;

        public AdjacencyMatrix(bool isDirected, bool isWeighted, int size)
        {
            this.Nodes = new List<T>(size);
            this.edges = new float[size, size];
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                for (int j = 0; j < this.Nodes.Count; j++)
                {
                    this.edges[i, j] = -1;
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
            return this.edges[from, to] >= 0;
        }

        public override bool EdgeExists(T from, T to)
        {
            var pair = this.GetNodePair(from, to);
            return EdgeExists(pair.Item1, pair.Item2);
        }

        public override float EdgeWeight(int from, int to)
        {
            return this.edges[from, to];
        }

        public override float EdgeWeight(T from, T to)
        {
            var pair = this.GetNodePair(from, to);
            return EdgeWeight(pair.Item1, pair.Item2);
        }

	protected override void AddNodeByType(T n) {
		var rows = this.edges.GetLength(0);
		var cols = this.edges.GetLength(1);

		var nEdges = new float[this.edges.GetLength(0) + 1, this.edges.GetLength(1) + 1];
		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < cols; c++) {
				if (r == rows - 1 || c == cols - 1)
					nEdges[r, c] = 0;
				else
					nEdges[r, c] = this.edges[r, c];
			}
		}
	}

	protected override void RemoveNodeByType(T n) {
		throw new NotImplementedException();
	}

	#region AddEdge

	protected override void AddEdgeByIndex(int from, int to, float weight)
        {
            this.addEdge(from, to, weight);
        }

        protected override void AddEdgeByType(T from, T to, float weight = -1)
        {
            var pair = this.GetNodePair(from, to);
            this.addEdge(pair.Item1, pair.Item2, weight);
        }

        private void addEdgeDirected(int from, int to, float weight)
        {
            this.edges[from, to] = weight;
        }

        private void addEdgeUndirected(int from, int to, float weight)
        {
            this.edges[from, to] = weight;
            this.edges[to, from] = weight;
        }

        #endregion

        #region RemoveEdge

        protected override void RemoveEdgeByIndex(int from, int to)
        {
            this.removeEdge(from, to);
        }

	protected override void RemoveEdgeByType(T from, T to)
        {
            var pair = this.GetNodePair(from, to);
            this.removeEdge(pair.Item1, pair.Item2);
        }

        private void removeEdgeDirected(int from, int to)
        {
            this.edges[from, to] = -1;
        }

        private void removeEdgeUndirected(int from, int to)
        {
            this.edges[from, to] = -1;
            this.edges[to, from] = -1;
        }

        #endregion

        #region Neighbors

        public override IEnumerable<int> Neighbors(int n)
        {
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                if (this.edges[n, i] >= 0)
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
