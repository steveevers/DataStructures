using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Miscellaneous;

namespace SE.Graphs
{
	public abstract class Graph<T> : IGraph<T>
	{
		private Maybe<bool> isConnected = Maybe<bool>.None();
		private Maybe<bool> isComplete = Maybe<bool>.None();
		private Maybe<bool> isAcyclic = Maybe<bool>.None();

		public IList<T> Nodes { get; protected set; }
		public bool IsDirected { get; protected set; }
		public bool IsWeighted { get; protected set; }
		public bool IsConnected
		{
			get
			{
				return this.isConnected.Match(
					some: (v) => v,
					none: () =>
					{
						var start = this.RandomNode();
						var open = new Queue<int>();
						var closed = new HashSet<int>();

						open.Enqueue(start);
						closed.Add(start);

						while (open.Any())
						{
							var i = open.Dequeue();
							foreach (var n in this.Neighbors(i))
							{
								if (closed.Add(n))
									open.Enqueue(n);
							}
						}

						this.isConnected = Maybe<bool>.Some(closed.Count == this.Nodes.Count);
						return this.isConnected.Value;
					});
			}
		}
		public bool IsComplete
		{
			get
			{
				return this.isComplete.Match(
					some: (v) => v,
					none: () =>
					{
						for (int i = 0; i < this.Nodes.Count; i++)
						{
							// number of connections for each node must be greater or equal to the number of nodes in the graph (- 1 to account for self-connected nodes)
							var neighbourCount = this.Neighbors(i).Count();
							if (neighbourCount < this.Nodes.Count - 1)
							{
								this.isComplete = Maybe<bool>.Some(false);
								return this.isComplete.Value;
							}
						}

						this.isComplete = Maybe<bool>.Some(true);
						return this.isComplete.Value;
					});
			}
		}
		public bool IsAcyclic
		{
			get
			{
				return this.isAcyclic.Match(
					some: (v) => v,
					none: () =>
					{
						if (!this.IsDirected)
						{
							this.isAcyclic = Maybe<bool>.Some(false);
							return this.isAcyclic.Value;
						}

						for (int i = 0; i < this.Nodes.Count; i++)
						{
							if (this.Search(i, i, Searching.SearchType.DFS))
							{
								this.isAcyclic = Maybe<bool>.Some(false);
								return this.isAcyclic.Value;
							}
						}

						this.isAcyclic = Maybe<bool>.Some(true);
						return this.isAcyclic.Value;
					});
			}
		}

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

		public void AddEdge(int from, int to, float weight = -1)
		{
			this.ResetProperties();
			this.AddEdgeByIndex(from, to, weight);
		}
		public void AddEdge(T from, T to, float weight = -1)
		{
			this.ResetProperties();
			this.AddEdgeByType(from, to, weight);
		}

		protected abstract void AddEdgeByIndex(int from, int to, float weight);
		protected abstract void AddEdgeByType(T from, T to, float weight);

		public void RemoveEdge(int from, int to)
		{
			this.ResetProperties();
			this.RemoveEdgeByIndex(from, to);
		}
		public void RemoveEdge(T from, T to)
		{
			this.ResetProperties();
			this.RemoveEdgeByType(from, to);
		}

		protected abstract void RemoveEdgeByIndex(int from, int to);
		protected abstract void RemoveEdgeByType(T from, T to);

		public void AddNode(T n)
		{
			this.ResetProperties();
			this.Nodes.Add(n);
			this.AddNodeByType(n);
		}

		public void RemoveNode(int n)
		{
			this.ResetProperties();
			this.RemoveNodeByType(this.Nodes[n]);
			this.Nodes.RemoveAt(n);
		}

		protected abstract void AddNodeByType(T n);
		protected abstract void RemoveNodeByType(T n);

		public virtual void RemoveNode(T n)
		{
			this.ResetProperties();
			this.Nodes.Remove(n);
		}

		public abstract IEnumerable<int> Neighbors(int n);
		public abstract IEnumerable<int> Neighbors(T n);

		/// <summary>
		/// Resets flags so that graph properties are recalculated
		/// </summary>
		protected void ResetProperties()
		{
			this.isConnected = Maybe<bool>.None();
			this.isComplete = Maybe<bool>.None();
			this.isAcyclic = Maybe<bool>.None();
		}
	}
}
