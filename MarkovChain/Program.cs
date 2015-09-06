using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs;
using Miscellaneous;

namespace MarkovChain
{
    class Program
    {
        static void Main(string[] args)
        {
            var states = new List<char> { 'A', 'B', 'C', 'D' };
            var m = new MarkovChain<char>(states);
            
            for (int i = 0; i < states.Count; i++)
            {
                for (int j = 0; j < states.Count; j++)
                {
                    float probability = (float)Math.Abs(i - j) * .25f;

                    m.AddTransition(i, j, probability);
                }
            }

            int k = 0;
            List<char> sequence = new List<char>();

            do
            {
                Console.Clear();
                Console.WriteLine("History:\t" + string.Join(",", sequence));
                Console.WriteLine("Suggestions:\t" + string.Join(",", m.MarkovPath(k, 4)));

                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out k))
                {
                    continue;
                }

                if (k == 9 || k >= m.States.Count)
                    break;

                sequence.Add(m.States[k]);
            } while (true);

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Done");
            while (!Console.KeyAvailable) { }
        }
    }
}
