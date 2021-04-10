using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Laboratory1;

namespace Lab1
{
    class PuzzleState : State
    {
        public int[,] plansza;
        //n^2
        public int n;
        //x i y pola "pustego"
        private int ex, ey;
        //x i y możliwych przesunięć
        private List<int> mx=new List<int>(4);
        private List<int> my=new List<int>(4);
        private void ZnajdzE()
        {
            bool found = false;
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    if (plansza[x, y] == n * n)
                    {
                        found = true;
                        ex = x;
                        ey = y;
                        break;
                    }
                }
                if (found)
                    break;
            }
        }
        //Znajdź możliwe przesunięcia
        private void ZnajdzM()
        {
            mx.Clear();
            my.Clear();
            if (ex + 1 < n)
            {
                mx.Add(1);
                my.Add(0);
            }
            if (ex - 1 >= 0)
            {
                mx.Add(-1);
                my.Add(0);
            }
            if (ey + 1 < n)
            {
                mx.Add(0);
                my.Add(1);
            }
            if (ey - 1 >= 0)
            {
                mx.Add(0);
                my.Add(-1);
            }
        }
        public override double ComputeHeuristicGrade()
        {
            H = 0;
            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    H += Math.Abs(x - (plansza[x, y] - 1) % n);
                    H += Math.Abs(y - (plansza[x, y] - 1) / n);
                }
            }
            H /= 2;
            return H;
        }
        public string GetHash()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in plansza)
            {
                sb.Append(i.ToString());
                sb.Append('d');
            }
            return sb.ToString();
        }
        //-1,0,1
        public void Ruch(int dx,int dy)
        {
            int temp = plansza[ex + dx, ey + dy];
            plansza[ex + dx, ey + dy] = plansza[ex, ey];
            plansza[ex, ey] = temp;
            ex += dx;
            ey += dy;
        }
        public void Dzieci()
        {
            ZnajdzM();
            for (int i = 0; i < mx.Count; i++)
            {
                PuzzleState s = new PuzzleState(n, this);
                s.Ruch(mx[i], my[i]);
                s.ComputeHeuristicGrade();
                s.ID = s.GetHash();
                this.Children.Add(s);
            }
        }
        public PuzzleState(int n, int shuffle)
            : base()
        {
            this.n = n;
            this.plansza = new int[n, n];

            int temp = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    plansza[i, j] = ++temp;
                }
            }
            Random rgen = new Random();
            ex = ey = n - 1;
            int dir;
            for (int i = 0; i < shuffle; i++)
            {
                ZnajdzM();
                dir = rgen.Next(mx.Count);
                Ruch(mx[dir], my[dir]);
            }
            ComputeHeuristicGrade();
            this.ID = GetHash();
        }
        public PuzzleState(int n, PuzzleState parent)
            : base(parent)
        {
            this.n = n;
            this.G = parent.G + 1;
            this.plansza = new int[n, n];
            Array.Copy(parent.plansza, this.plansza, n * n);
            ZnajdzE();
        }
    }
}
