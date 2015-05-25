using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using System.Linq;

namespace GraphTests
{
    [TestClass]
    public class IsCompleteTests
    {
        [TestMethod, TestCategory("Graph Properties")]
        public void IsCompleteSimple()
        {
            var graph = GraphBuilder
                .Sparse()
                .Directed(false)
                .Weighted(false)
                .Build<char>();

            graph.AddNodes(Enumerable.Range('a', 'j' - 'a' + 1).Select(c => (char)c));

            foreach (var a in graph.Nodes)
            {
                foreach (var b in graph.Nodes)
                {
                    if (a != b)
                        graph.AddEdge(a, b);
                }
            }

            Assert.IsTrue(graph.IsComplete(), "Graph is not connected");
        }
    }
}
