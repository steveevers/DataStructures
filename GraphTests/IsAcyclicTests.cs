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
                .Build<int>();

            graph.AddNodes(Enumerable.Range(1, 10));

            Assert.IsFalse(graph.IsAcyclic(), "Undirected graphs cannot be acyclic");
        }
    }
}
