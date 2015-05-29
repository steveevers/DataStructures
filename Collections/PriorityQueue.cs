using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    public class PriorityQueue<P, V>
    {
        private IDictionary<P, Queue<V>> list = new SortedDictionary<P, Queue<V>>();

        public bool IsEmpty { get { return !this.list.Any(); } }

        public void Enqueue(P priority, V value)
        {
            Queue<V> q;
            if (!list.TryGetValue(priority, out q))
            {
                q = new Queue<V>();
                list.Add(priority, q);
            }

            q.Enqueue(value);
        }

        public V Dequeue()
        {
            var pair = list.First();
            var v = pair.Value.Dequeue();

            if (!pair.Value.Any())
                list.Remove(pair.Key);

            return v;
        }
    }
}
