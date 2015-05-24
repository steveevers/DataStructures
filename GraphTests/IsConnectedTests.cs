using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using System.Linq;

namespace GraphTests
{
    [TestClass]
    public class IsConnectedTests
    {
        [TestMethod, TestCategory("Graph Properties")]
        public void Simple()
        {
            var graph = GraphBuilder
                .Sparse()
                .Directed(false)
                .Weighted(false)
                .Build<int>();

            foreach (var i in Enumerable.Range(1, 10))
                graph.AddNode(i);

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    if (i != j)
                        graph.AddEdge(i, j);
                }
            }

            Assert.IsTrue(graph.IsConnected(), "Graph is not connected");
        }
    }
}
