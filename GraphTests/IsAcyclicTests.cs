using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SE.Graphs;
using System.Linq;
using System.Collections.Generic;

namespace GraphTests
{
    [TestClass]
    public class IsAcyclicTests
    {
        [TestMethod, TestCategory("Graph Properties")]
        public void IsAcyclicForUndirectedGraphs()
        {
            var graph = GraphBuilder
                .Sparse()
                .Directed(false)
                .Weighted(false)
                .Build<char>();

            graph.AddNodes(Letters.Alphabet(Letters.A, Letters.J));

            Assert.IsFalse(graph.IsAcyclic, "Undirected graphs cannot be acyclic");
        }

        [TestMethod, TestCategory("Graph Properties")]
        public void IsAcyclicSimple()
        {
            var graph = GraphBuilder
                .Sparse()
                .Directed(true)
                .Weighted(false)
                .Build<char>();

            graph.AddNodes(Letters.Alphabet(Letters.A, Letters.E));
            graph.AddEdge(Letters.A, Letters.B);
            graph.AddEdge(Letters.B, Letters.C);
            graph.AddEdge(Letters.B, Letters.C);
            graph.AddEdge(Letters.B, Letters.D);
            graph.AddEdge(Letters.B, Letters.E);
            graph.AddEdge(Letters.C, Letters.D);
            graph.AddEdge(Letters.D, Letters.E);
            
            Assert.IsTrue(graph.IsAcyclic, "Graph is not acyclic");
        }
    }
}
