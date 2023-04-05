using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bombermen
{
     class Step
     {
        public int moves;
        public Element el;
        private Step prev;
        private Player pl;

        public Step(int _moves, Element m, Step prev, Player pl)
        {
            moves = _moves;
            el = m;
            this.prev = prev;
            this.pl = pl;
        }

        public int Priority()
        { return moves + el.manhattan(pl); }

        public int Moves()
        { return moves; }

        public Step Pstep()
        { return prev; }

        internal bool IsGoal(Element el, Player pl)
        {
            if (el.X == pl.X && el.Y == pl.Y)
                return true;

            return false;
        }
     }

     class MinPQ<Key> where Key : Step
     {
        private Key[] pq;
        private int N;
        Player pl;

        public MinPQ(int capacity)
        { pq = new Key[capacity + 1]; }

        public bool isEmpty()
        { return N == 0; }

        public void insert(Key x)
        {
            pq[++N] = x;
            swim(N);
        }

        public Key delMin()
        {
            Key max = pq[1];
            exch(1, N--);
            sink(1);
            pq[N + 1] = default(Key);
            return max;
        }

        private void swim(int k)
        {
            while (k > 1 && greater(k / 2, k))
            {
                exch(k, k / 2);
                k = k / 2;
            }
        }

        private void sink(int k)
        {
            while (2 * k <= N)
            {
                int j = 2 * k;
                if (j < N && greater(j, j + 1)) j++;
                if (!greater(k, j)) break;
                exch(k, j);
                k = j;
            }
        }

        private bool greater(int i, int j)
        {
            return pq[i].Priority() - pq[j].Priority() > 0;

        }

        private void exch(int i, int j)
        { Key t = pq[i]; pq[i] = pq[j]; pq[j] = t; }
     }
}
