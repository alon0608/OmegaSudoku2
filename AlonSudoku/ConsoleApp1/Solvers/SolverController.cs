using AlonSudoku.Board;

namespace AlonSudoku.Solvers
{
    /// <summary>
    /// Controls the Sudoku solving process by managing the solver instance.
    /// This class acts as a bridge between the Sudoku board and the solver.
    /// </summary>
    public class SolverController
    {
        /// <summary>
        /// The Sudoku solver instance used to solve the board.
        /// </summary>
        private readonly SudokuSolver _solver;

        /// <summary>
        /// Initializes a new instance of the SolverController class.
        /// </summary>
        /// <param name="board">The Sudoku board to be solved.</param>
        public SolverController(SudokuBoard board)
        {
            _solver = new SudokuSolver(board);
        }

        /// <summary>
        /// Attempts to solve the Sudoku puzzle using a backtracking.
        /// </summary>
        /// <returns>True if the puzzle is successfully solved, otherwise false.</returns>
        public bool Solve()
        {
            return _solver.SolveWithBacktracking();
        }
    }
}
