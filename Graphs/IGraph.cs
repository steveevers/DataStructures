using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public interface IGraph<T>
    {
        IList<T> Nodes { get; }
        bool IsDirected { get; }
        bool IsWeighted { get; }

        bool EdgeExists(int from, int to);
        bool EdgeExists(T from, T to);
        float EdgeWeight(int from, int to);
        float EdgeWeight(T from, T to);

        void AddEdge(int from, int to, float weight = -1);
        void AddEdge(T from, T to, float weight = -1);
        void RemoveEdge(int from, int to);
        void RemoveEdge(T from, T to);

        void AddNode(T n);
        void RemoveNode(int n);
        void RemoveNode(T n);

        IEnumerable<int> Neighbors(int n);
        IEnumerable<int> Neighbors(T n);
    }
}
