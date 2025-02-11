using AlonSudoku.Board;
using AlonSudoku.Core.BoardStateManagerClass;

namespace AlonSudoku.Solvers
{
    /// <summary>
    /// Implements the Naked Singles solving technique for Sudoku.
    /// This strategy identifies cells where only one possible number can be placed.
    /// </summary>
    public static class NakedSinglesSolver
    {
        /// <summary>
        /// Applies the Naked Singles strategy to the Sudoku board.
        /// A Naked Single occurs when a cell has only one possible number that can be placed.
        /// </summary>
        /// <param name="board">The Sudoku board.</param>
        /// <param name="rowUsed">Bitmasks tracking used numbers in each row.</param>
        /// <param name="colUsed">Bitmasks tracking used numbers in each column.</param>
        /// <param name="boxUsed">Bitmasks tracking used numbers in each box.</param>
        /// <param name="size">The size of the Sudoku board.</param>
        /// <param name="boxSize">The size of a Sudoku box.</param>
        /// <returns>True if at least one Naked Single was found and placed, otherwise false.</returns>
        public static bool ApplyNakedSingles(
            SudokuBoard board,
            int[] rowUsed,
            int[] colUsed,
            int[] boxUsed,
            int size,
            int boxSize)
        {
            bool progress = false;

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    // Skip cells that are already filled
                    if (board.GetCellValue(r, c) != 0)
                        continue;

                    // Determine possible numbers for the cell using bitmasks
                    int usedMask = rowUsed[r] | colUsed[c] | boxUsed[BoardStateManager.GetBoxIndex(r, c, boxSize)];
                    int options = ~usedMask & (1 << size) - 1;

                    // If only one possible value remains, place it
                    if (BoardStateManager.PopCount(options) == 1)
                    {
                        int val = BoardStateManager.Log2(options) + 1;
                        BoardStateManager.PlaceNumber(board, rowUsed, colUsed, boxUsed, r, c, val, boxSize);
                        progress = true;
                    }
                }
            }
            return progress;
        }
    }
}

