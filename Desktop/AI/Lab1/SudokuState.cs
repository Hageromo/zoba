using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Laboratory1;

namespace Lab1
{
    class SudokuState : State
    {
        public int[,] plansza = new int[9, 9];
        private List<int> zerox = new List<int>();
        private List<int> zeroy = new List<int>();
        private List<int> mozliwosci = new List<int>();
        private int minid;
        public SudokuState(String pattern)
            : base()
        {
            CharEnumerator ce = pattern.GetEnumerator();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ce.MoveNext();
                    plansza[i, j] = int.Parse(ce.Current.ToString());
                }
            }
            this.id = pattern;
            Inicjalizuj();
        }
        public SudokuState(int[,] plansza, SudokuState parent)
            : base(parent)
        {
            Array.Copy(plansza, this.plansza, 9 * 9);
            StringBuilder sb = new StringBuilder();
            foreach (int i in this.plansza)
            {
                sb.Append(i);
            }
            this.id = sb.ToString();
            Inicjalizuj();
        }
        private void Inicjalizuj()
        {
            ZnajdzPuste();
            ZnajdzMozliwosci();
            if (mozliwosci.Count != 0)
                ZnajdzMinMoz();
        }
        private void ZnajdzPuste()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (plansza[i, j] == 0)
                    {
                        zerox.Add(i);
                        zeroy.Add(j);
                    }
                }
            }
        }
        private void ZnajdzMozliwosci()
        {
            bool[] d = new bool[9];
            int c;
            for (int i = 0; i < zerox.Count; i++)
            {
                //Wszystko jest dozwolone
                for (int j = 0; j < 9; j++)
                    d[j] = true;
                //Chyba że już występuje w kolumnie
                for (int w = 0; w < 9; w++)
                {
                    c = plansza[w, zeroy.ElementAt(i)];
                    if (c != 0)
                    {
                        d[c - 1] = false;
                    }
                }
                //lub wierszu
                for (int k = 0; k < 9; k++)
                {
                    c = plansza[zerox.ElementAt(i), k];
                    if (c != 0)
                    {
                        d[c - 1] = false;
                    }
                }
                //lub kwadracie
                int kwadrat_x = zerox.ElementAt(i) - zerox.ElementAt(i) % 3;
                int kwadrat_y = zeroy.ElementAt(i) - zeroy.ElementAt(i) % 3;
                for (int w = kwadrat_x; w < kwadrat_x + 3; w++)
                {
                    for (int k = kwadrat_y; k < kwadrat_y + 3; k++)
                    {
                        c = plansza[w, k];
                        if (c != 0)
                        {
                            d[c - 1] = false;
                        }
                    }
                }
                int m = 0;
                for (int x = 0; x < 9; x++)
                {
                    if (d[x] == true)
                    {
                        m += 1;
                    }
                }
                mozliwosci.Add(m);
            }
        }
        private void ZnajdzMinMoz()
        {
            minid = mozliwosci.IndexOf(mozliwosci.Min());
        }
        public void DzieciNaiwnie()
        {
            int a = zerox.ElementAt(0);
            int b = zeroy.ElementAt(0);
            int[,] copy = new int[9, 9];
            Array.Copy(plansza, copy, 9 * 9);
            for (int x = 1; x < 10; x++)
            {
                copy[a, b] = x;
                SudokuState s = new SudokuState(copy, this);
                s.G = this.G + 1;
                s.ComputeHeuristicGrade();
                children.Add(s);
            }
        }
        public void DzieciMinimum()
        {
            int a = zerox.ElementAt(minid);
            int b = zeroy.ElementAt(minid);
            int[,] copy = new int[9, 9];
            Array.Copy(plansza, copy, 9 * 9);
            for (int x = 1; x < 10; x++)
            {
                copy[a, b] = x;
                SudokuState s = new SudokuState(copy, this);
                s.G = this.G + 1;
                s.ComputeHeuristicGrade();
                children.Add(s);
            }
        }
        public void DzieciMinPopr()
        {
            int a = zerox.ElementAt(minid);
            int b = zeroy.ElementAt(minid);
            int[,] copy = new int[9, 9];
            Array.Copy(plansza, copy, 9 * 9);
            children.Clear();
            for (int x = 1; x < 10; x++)
            {
                copy[a, b] = x;
                SudokuState s = new SudokuState(copy, this);
                if (s.Poprawna())
                {
                    s.G = this.G + 1;
                    s.ComputeHeuristicGrade();
                    children.Add(s);
                }
            }
        }
        private void Heurystyka()
        {
            if (!Poprawna())
            {
                this.h = double.PositiveInfinity;
                isAdmissible = false;
            }
            else
            {

                //this.H = Naiwna();
                //this.H = Minimum();
                this.H = Suma();
                //throw new NotImplementedException();
            }
        }
        public override double ComputeHeuristicGrade()
        {
            Heurystyka();
            return this.H;
        }
        public bool Rozwiazanie()
        {
            foreach (int i in plansza)
            {
                if (i == 0) return false;
            }
            return true;
        }
        public bool Poprawna()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!SprawdzKwadrat(i, j))
                        return false;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (!SprawdzKolumne(i))
                    return false;
                if (!SprawdzWiersz(i))
                    return false;
            }
            return true;
        }
        private bool SprawdzKwadrat(int x, int y)
        {
            bool[] bylo = new bool[9] { false, false, false, false, false, false, false, false, false };
            for (int i = x * 3; i < x * 3 + 3; i++)
            {
                for (int j = y * 3; j < y * 3 + 3; j++)
                {
                    int c = plansza[i, j];
                    if (c != 0)
                    {
                        if (bylo[c - 1])
                            return false;
                        bylo[c - 1] = true;
                    }
                }
            }
            return true;
        }
        private bool SprawdzKolumne(int x)
        {
            bool[] bylo = new bool[9] { false, false, false, false, false, false, false, false, false };
            for (int i = 0; i < 9; i++)
            {
                int c = plansza[i, x];
                if (c != 0)
                {
                    if (bylo[c - 1])
                        return false;
                    bylo[c - 1] = true;
                }
            }
            return true;
        }
        private bool SprawdzWiersz(int x)
        {
            bool[] bylo = new bool[9] { false, false, false, false, false, false, false, false, false };
            for (int i = 0; i < 9; i++)
            {
                int c = plansza[x, i];
                if (c != 0)
                {
                    if (bylo[c - 1])
                        return false;
                    bylo[c - 1] = true;
                }
            }
            return true;
        }
        private double Naiwna()
        {
            return zerox.Count;
        }
        private double Minimum()
        {
            if (mozliwosci.Count > 0)
                return mozliwosci.ElementAt(minid);
            return 0;
        }
        private double Suma()
        {
            double s = 0;
            foreach (int i in mozliwosci)
            {
                s += i;
            }
            return s;
        }
    }
}
