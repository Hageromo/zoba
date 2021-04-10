using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratory1 {
    public abstract class AStarSearch {

        #region Protected Fields

        /// <summary>
        /// Zbiór Closed - sortowany słownik hashów stanów i samych stanów.
        /// </summary>
        protected SortedDictionary<string, IState> closed = null;

        /// <summary>
        /// Stan początkowy.
        /// </summary>
        protected IState initialState = null;

        /// <summary>
        /// Decyduje czy zatrzymać algorytm po odnalezieniu pierwszego rozwiązania.
        /// </summary>
        protected bool isStopAfterFirstSolution = true;

        /// <summary>
        /// Decyduje czy zatrzymać algorytm po odnalezieniu drugiego rozwiązania.
        /// </summary>
        protected bool isStopAfterSecondSolution = true;

        /// <summary>
        /// Zbiór Open - stany które mają zostać odwiedzone.
        /// W zasadzie powinna być to kolejka priorytetowa ale na potrzeby zadania akademickiego taka konstrukcja w zupełności wystarczy.
        /// </summary>
        protected SortedList<IState, object> open = null;

        /// <summary>
        /// Lista odnalezionych rozwiązań.
        /// </summary>
        protected List<IState> solutions = null;

        #endregion //end Protected Fields

        #region Properties

        /// <summary>
        /// Zbiór Closed - stanów odwiedzonych.
        /// </summary>
        public SortedDictionary<string, IState> Closed {
            get {
                return this.closed;
            }
        }

        /// <summary>
        /// Lista odnalezionych rozwiązań.
        /// </summary>
        public List<IState> Solutions {
            get {
                return this.solutions;
            }
        }

        #endregion //end Properties

        #region Constructors

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="initialState">Stan początkowy.</param>
        /// <param name="isStopAfterFirstSolution">Decyduje czy zatrzymać algorytm po odnalezieniu pierwszego rozwiązania.</param>
        /// <param name="isStopAfterSecondSolution">Decyduje czy zatrzymać algorytm po odnalezieniu drugiego rozwiązania.</param>
        public AStarSearch(IState initialState, bool isStopAfterFirstSolution, bool isStopAfterSecondSolution) {
            this.closed = new SortedDictionary<string, IState>();
            this.initialState = initialState;
            this.isStopAfterFirstSolution = isStopAfterFirstSolution;
            this.isStopAfterSecondSolution = isStopAfterSecondSolution;
            this.solutions = new List<IState>();
            this.open = new SortedList<IState, object>();
        }

        #endregion //end Constructors

        #region Protected Methods

        /// <summary>
        /// Metoda powinna zawierać wszelkie niezbędne operacje do zbudowania stanów potomnych.
        /// </summary>
        /// <param name="parent">Stan rodzica.</param>
        protected abstract void buildChildren(IState parent);

        /// <summary>
        /// Zwraca wartość bool mówiąco czy stan podany w parametrze jest rozwiązaniem.
        /// </summary>
        /// <param name="state">Stan do sprawdzenia.</param>
        /// <returns>Wartość bool czy stan jest rozwiązaniem.</returns>
        protected abstract bool isSolution(IState state);

        /// <summary>
        /// Zarejestrowanie rozwiązania. Poprzez przeciązenie metody można do niej dodać więcej akcji.
        /// </summary>
        /// <param name="solutionState">Stan rozwiązania.</param>
        protected void registerSolution(IState solutionState) {
            solutions.Add(solutionState);
        }

        #endregion //end Private Methods

        #region Public Methods

        /// <summary>
        /// Wykonanie algorytmu A*.
        /// </summary>
        public void DoSearch() {
            IState currentState = this.initialState;

            while (true) {
                bool isSol = isSolution(currentState);
                if (isSol) {
                    registerSolution(currentState);

                    if (this.isStopAfterFirstSolution) {
                        break;
                    }

                    if ((solutions.Count == 2) && (this.isStopAfterSecondSolution)) {
                        break;
                    }
                }
                else {
                    buildChildren(currentState);

                    foreach (IState child in currentState.Children) {

                        if (this.closed.ContainsKey(child.ID)) {
                            continue;
                        }

                        bool found = false;
                        for (int i = 0; i < this.open.Keys.Count; ++i) {
                            if (this.open.Keys[i].ID == child.ID) {
                                if (this.open.Keys[i].F > child.F) {
                                    this.open.RemoveAt(i);
                                    this.open.Add(child, null);
                                }
                                found = true;
                                break;
                            }
                        }

                        if (!found) {
                            this.open.Add(child, null);
                        }
                    }
                }

                this.closed.Add(currentState.ID, currentState);

                if (this.open.Count == 0) {
                    break;
                }
                else {
                    currentState = this.open.Keys[0];
                    this.open.RemoveAt(0);
                }
            }
        }

        #endregion //end Public Methods
    }
}