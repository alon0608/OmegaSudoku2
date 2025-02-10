using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    public class SolverController
    {
        private readonly SudokuSolver _solver;

        public SolverController(SudokuBoard board)
        {
            _solver = new SudokuSolver(board);
        }

        public bool Solve()
        {
            return _solver.SolveWithBacktracking();
        }
    }
}
