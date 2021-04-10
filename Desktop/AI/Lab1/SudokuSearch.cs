using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Laboratory1;

//http://mapleta.maths.uwa.edu.au/~gordon/sudokupat.php
namespace Lab1
{
    class SudokuSearch : AStarSearch
    {
        public SudokuSearch(SudokuState state)
            : base(state, true, true)
        {
            //TODO ?
        }
        protected override void buildChildren(IState parent)
        {
            SudokuState s = (SudokuState)parent;
            s.DzieciMinPopr();
        }

        protected override bool isSolution(IState state)
        {
            SudokuState s = (SudokuState)state;
            return s.Rozwiazanie();
        }
    }
}
