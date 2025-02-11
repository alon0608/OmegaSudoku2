using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlonSudoku
{
    /// <summary>
    /// Manages a Sudoku game, including loading the board and validating it,
    /// and attempting to solve it using a Sudoku solver.
    /// </summary>
    public class SudokuGame
    {
        private readonly SudokuBoard _board;
        private readonly SolverController _solver;

        /// <summary>
        /// Initializes a new Sudoku game using an input source.
        /// The input source provides the initial board state.
        /// </summary>
        /// <param name="inputSource">The source that provides the Sudoku board as a string.</param>
        public SudokuGame(IInput inputSource)
        {
            string input = inputSource.GetInput();
            _board = new SudokuBoard(input);
            _solver = new SolverController(_board);
        }

        /// <summary>
        /// Runs the game logic:
        /// - Prints the initial board.
        /// - Validates if the board follows Sudoku rules.
        /// - Attempts to solve the puzzle.
        /// - Prints the solution if one is found.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("First board:");
            _board.PrintBoard();

            // Validate if the board is a proper Sudoku configuration
            if (!SudokuValidation.IsValid(_board))
            {
                Console.WriteLine("Invalid board.");
                return;
            }

            // Try solving the board
            if (_solver.Solve())
            {
                Console.WriteLine("Solution:");
                _board.PrintBoard();
            }
            else
            {
                Console.WriteLine("No solution for this board.");
            }
        }
    }
}
