using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Graphs
{
    public class GraphBuilder
    {
        private bool isDirected = true;
        private bool isWeighted = false;
        private bool isSparse = true;
        private int knownSize = 0;

        private GraphBuilder(bool isSparse)
        {
            this.isSparse = isSparse;
        }

        public static GraphBuilder Dense() { return new GraphBuilder(false); }
        public static GraphBuilder Sparse() { return new GraphBuilder(true); }

        public GraphBuilder Weighted(bool value)
        {
            this.isWeighted = value;
            return this;
        }

        public GraphBuilder Directed(bool value)
        {
            this.isDirected = value;
            return this;
        }

        public GraphBuilder WithKnownMaxSize(int size)
        {
            this.knownSize = size;
            return this;
        }

        public IGraph<T> Build<T>() 
        {
            if (this.isWeighted && this.knownSize > 0)
                return new AdjacencyMatrix<T>(this.isDirected, true, this.knownSize);
            else if (this.isWeighted && this.knownSize <= 0)
                throw new NotSupportedException("Weighted graphs currently need a known size");

            if (this.isSparse && !this.isWeighted)
                return new AdjacencyList<T>(this.isDirected);
            else
                throw new NotSupportedException("Cannot build a graph with the given properties {" + string.Join(",", "Weighted: " + this.isWeighted, "Directed: " + this.isDirected, "Size: " + this.knownSize) + "}");
        }
    }
}
