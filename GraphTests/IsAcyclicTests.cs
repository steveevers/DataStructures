using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
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

            graph.AddNodes(Enumerable.Range('a', 'j' - 'a' + 1).Select(c => (char)c));

            Assert.IsFalse(graph.IsAcyclic(), "Undirected graphs cannot be acyclic");
        }

        [TestMethod, TestCategory("Graph Properties")]
        public void IsAcyclicSimple()
        {
            var graph = GraphBuilder
                .Sparse()
                .Directed(true)
                .Weighted(false)
                .Build<char>();

            char a = 'a';
            char b = 'b';
            char c = 'c';
            char d = 'd';
            char e = 'e';

            graph.AddNodes(new List<char> { a, b, c, d, e });
            graph.AddEdge(a, b);
            graph.AddEdge(a, c);
            graph.AddEdge(b, c);
            graph.AddEdge(b, d);
            graph.AddEdge(b, e);
            graph.AddEdge(c, d);
            graph.AddEdge(d, e);
            
            Assert.IsTrue(graph.IsAcyclic(), "Graph is not acyclic");
        }
    }
}
