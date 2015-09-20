using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Graphs
{
    public class MarkovChain<T>
    {
        private IGraph<T> graph;

        public MarkovChain(int numStates)
        {
            this.graph = GraphBuilder
                .Sparse()
                .Directed(true)
                .Weighted(true)
                .WithKnownMaxSize(numStates)
                .Build<T>();
        }

        public MarkovChain(IList<T> states) 
            : this(states.Count)
        {
            this.AddStates(states);
        }

        #region Graph Wrapper Methods

        public IList<T> States { get { return this.graph.Nodes; } }

        public void AddState(T state)
        {
            this.graph.AddNode(state);
        }

        public void AddStates(IEnumerable<T> states)
        {
            this.graph.AddNodes(states);
        }

        public void AddTransition(int from, int to, float probability)
        {
            this.graph.AddEdge(from, to, probability);
        }

        #endregion

        public int Next(int currentIndex)
        {
            int maxNeighbour = -1;
            float maxWeight = float.MinValue;
            var neighbours = graph.Neighbors(currentIndex).ToList();

            for (int i = 0; i < neighbours.Count; i++)
            {
                var w = graph.EdgeWeight(currentIndex, i);
                if (w > maxWeight)
                {
                    maxWeight = w;
                    maxNeighbour = i;
                }
            }

            return maxNeighbour;
        }

        public IEnumerable<int> MarkovPath(int startIndex, int length)
        {
            if (length < 1)
                throw new ArgumentOutOfRangeException("length of markov path must be greater than or equal to 1");

            int nextIndex = this.Next(startIndex);
            yield return nextIndex;

            for (int i = 1; i < length; i++)
            {
                nextIndex = this.Next(nextIndex);
                yield return nextIndex;
            }
        }
    }
}
