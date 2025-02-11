using AlonSudoku.Core.SudokuBoardClass;
using AlonSudoku.Solvers;
using AlonSudoku.Core.SolverState;
using AlonSudoku.Core.BoardStateManagerClass;
using System;

namespace AlonSudoku.Solvers
{
    /// <summary>
    /// Implements a Sudoku solver that uses backtracking and logical solving strategies
    /// to efficiently solve Sudoku puzzles.
    /// </summary>
    public class SudokuSolver
    {
        private readonly SudokuBoard _board;
        private readonly SolverState _state;

        /// <summary>
        /// Initializes a new Sudoku solver instance with a given board.
        /// </summary>
        /// <param name="board">The Sudoku board to solve.</param>
        public SudokuSolver(SudokuBoard board)
        {
            _board = board;
            _state = new SolverState(board);
        }

        /// <summary>
        /// Attempts to solve the Sudoku puzzle using backtracking combined with logical solving techniques.
        /// </summary>
        /// <returns>True if the puzzle is successfully solved, otherwise false.</returns>
        public bool SolveWithBacktracking()
        {
            return Backtrack();
        }

        /// <summary>
        /// Recursive backtracking algorithm to find the correct solution.
        /// It first applies logical solving strategies, then selects the best candidate cell to guess.
        /// </summary>
        /// <returns>True if a solution is found, otherwise false.</returns>
        private bool Backtrack()
        {
            // Apply logical solving strategies first
            while (ApplySolvingStrategies()) ;

            // If the board is fully solved, return true
            if (BoardStateManager.IsSolved(_board))
                return true;

            // Find the best cell to make a guess
            (int bestCandidateRow, int bestCandidateCol) = FindBestCandidateCell();
            if (bestCandidateRow == -1 && bestCandidateCol == -1)
                return false;

            int boxIndex = BoardStateManager.GetBoxIndex(bestCandidateRow, bestCandidateCol, _state.BoxSize);
            int availableNumbersMask = ~(_state.RowConstraints[bestCandidateRow] |
                                         _state.ColConstraints[bestCandidateCol] |
                                         _state.BoxConstraints[boxIndex]) & (1 << _state.Size) - 1;

            // Try each available number in the selected cell
            for (int val = 1; val <= _state.Size; val++)
            {
                int mask = 1 << val - 1;
                if ((availableNumbersMask & mask) != 0)
                {
                    // Clone board state before making a guess
                    SudokuBoard clonedBoard = _board.Clone();
                    int[] clonedRowConstraints = (int[])_state.RowConstraints.Clone();
                    int[] clonedColConstraints = (int[])_state.ColConstraints.Clone();
                    int[] clonedBoxConstraints = (int[])_state.BoxConstraints.Clone();

                    // Place the number and continue backtracking
                    BoardStateManager.PlaceNumber(_board, _state.RowConstraints, _state.ColConstraints, _state.BoxConstraints,
                                                  bestCandidateRow, bestCandidateCol, val, _state.BoxSize);

                    bool result = Backtrack();
                    if (result)
                        return true;

                    // Undo move if the guess was incorrect
                    BoardStateManager.UndoMove(_board, clonedBoard, _state.RowConstraints, clonedRowConstraints,
                                               _state.ColConstraints, clonedColConstraints, _state.BoxConstraints, clonedBoxConstraints);
                }
            }

            return false;
        }

        /// <summary>
        /// Applies logical solving strategies such as Naked Singles and Hidden Singles before resorting to backtracking.
        /// </summary>
        /// <returns>True if at least one logical deduction was made, otherwise false.</returns>
        private bool ApplySolvingStrategies()
        {
            bool progress = false;
            progress |= NakedSinglesSolver.ApplyNakedSingles(_board, _state.RowConstraints, _state.ColConstraints, _state.BoxConstraints, _state.Size, _state.BoxSize);

            // Apply Hidden Singles only for large boards (size >= 16)
            if (_state.Size >= 16)
            {
                progress |= HiddenSinglesSolver.ApplyHiddenSingles(_board, _state.RowConstraints, _state.ColConstraints, _state.BoxConstraints, _state.Size, _state.BoxSize);
            }
            return progress;
        }

        /// <summary>
        /// Finds the best candidate cell to make a guess.
        /// The best candidate is the cell with the fewest possible numbers, prioritizing cells that impact more constraints.
        /// </summary>
        /// <returns>A tuple (row, col) representing the best candidate cell.</returns>
        private (int row, int col) FindBestCandidateCell()
        {
            int minOptions = _state.Size + 1;
            int bestCandidateRow = -1;
            int bestCandidateCol = -1;
            int highestDegree = -1;
            bool foundSingleOption = false;

            for (int r = 0; r < _state.Size; r++)
            {
                foreach (int c in _state.EmptyCellsInRow[r])
                {
                    if (_board.GetCellValue(r, c) != 0)
                        continue;

                    int usedMask = _state.RowConstraints[r] | _state.ColConstraints[c] | _state.BoxConstraints[BoardStateManager.GetBoxIndex(r, c, _state.BoxSize)];
                    int options = ~usedMask & (1 << _state.Size) - 1;
                    int optionCount = BoardStateManager.PopCount(options);

                    // Degree heuristic: prioritize cells affecting more empty neighbors
                    int degree = _state.EmptyCellsInRow[r].Count + _state.EmptyCellsInCol[c].Count + _state.EmptyCellsInBox[BoardStateManager.GetBoxIndex(r, c, _state.BoxSize)].Count;

                    if (optionCount < minOptions || optionCount == minOptions && degree > highestDegree)
                    {
                        minOptions = optionCount;
                        bestCandidateRow = r;
                        bestCandidateCol = c;
                        highestDegree = degree;
                        if (optionCount == 1)
                            foundSingleOption = true;
                    }
                }
            }

            return (bestCandidateRow, bestCandidateCol);
        }
    }
}
