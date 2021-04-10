using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Laboratory1;

namespace Lab1
{
    class PuzzleSearch : AStarSearch
    {
        public PuzzleSearch(PuzzleState state)
            : base(state, true, true)
        {
        }
        protected override void buildChildren(IState parent)
        {
            PuzzleState s = (PuzzleState)parent;
            s.Dzieci();
        }

        protected override bool isSolution(IState state)
        {
            int x = 0;
            foreach (int i in ((PuzzleState)state).plansza)
            {
                if (i != ++x)
                    return false;
            }
            return true;
        }
    }
}
