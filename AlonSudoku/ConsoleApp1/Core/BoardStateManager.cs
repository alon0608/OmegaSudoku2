using AlonSudoku.Core.SudokuBoardClass;

namespace AlonSudoku.Core.BoardStateManagerClass
{
    /// <summary>
    /// This class manages the state of the Sudoku board.
    /// It handles solution verification, placing numbers, and board state tracking.
    /// </summary>
    public static class BoardStateManager
    {
        /// <summary>
        /// Computes the box index for a given row and column in a Sudoku grid.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="col">The column index.</param>
        /// <param name="boxSize">The size of a box.</param>
        /// <returns>The index of the  box.</returns>
        public static int GetBoxIndex(int row, int col, int boxSize)
        {
            return (row / boxSize) * boxSize + (col / boxSize);
        }

        /// <summary>
        /// Checks if the Sudoku board is completely solved.
        /// </summary>
        /// <param name="board">The Sudoku board object.</param>
        /// <returns>True if all cells are filled, otherwise false.</returns>
        public static bool IsSolved(SudokuBoard board)
        {
            int size = board.GetSize();
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (board.GetCellValue(r, c) == 0)
                        return false; // Found an empty cell, board is not solved
                }
            }
            return true; // No empty cells, board is solved
        }

        /// <summary>
        /// Places a number in a specific cell and updates the constraints 
        /// for rows, columns, and boxes.
        /// </summary>
        /// <param name="board">The Sudoku board object.</param>
        /// <param name="rowConstraints">Array representing row constraints.</param>
        /// <param name="colConstraints">Array representing column constraints.</param>
        /// <param name="boxConstraints">Array representing box constraints.</param>
        /// <param name="row">The row index.</param>
        /// <param name="col">The column index.</param>
        /// <param name="val">The value to be placed in the cell.</param>
        /// <param name="boxSize">The size of a box.</param>
        public static void PlaceNumber(SudokuBoard board, int[] rowConstraints, int[] colConstraints, int[] boxConstraints, int row, int col, int val, int boxSize)
        {
            board.SetCellValue(row, col, val); // Set the number in the cell
            int mask = 1 << (val - 1); // Create a bitmask for the given value
            rowConstraints[row] |= mask; // Mark the value as used in the row
            colConstraints[col] |= mask; // Mark the value as used in the column
            boxConstraints[GetBoxIndex(row, col, boxSize)] |= mask; // Mark the value as used in the box
        }

        /// <summary>
        /// Reverts the last move and restores the previous state of the board 
        /// and constraint arrays.
        /// </summary>
        /// <param name="board">The current board.</param>
        /// <param name="clonedBoard">A saved copy of the board before the move.</param>
        /// <param name="rowConstraints">The current row constraints.</param>
        /// <param name="clonedRowConstraints">The previous row constraints.</param>
        /// <param name="colConstraints">The current column constraints.</param>
        /// <param name="clonedColConstraints">The previous column constraints.</param>
        /// <param name="boxConstraints">The current box constraints.</param>
        /// <param name="clonedBoxConstraints">The previous box constraints.</param>
        public static void UndoMove(SudokuBoard board, SudokuBoard clonedBoard, int[] rowConstraints, int[] clonedRowConstraints, int[] colConstraints, int[] clonedColConstraints, int[] boxConstraints, int[] clonedBoxConstraints)
        {
            board.CopyFrom(clonedBoard); // Restore board state

            // Restore all constraint arrays
            for (int i = 0; i < rowConstraints.Length; i++)
            {
                rowConstraints[i] = clonedRowConstraints[i];
                colConstraints[i] = clonedColConstraints[i];
                boxConstraints[i] = clonedBoxConstraints[i];
            }
        }

        /// <summary>
        /// Computes the number of set bits (1s) in the binary representation of a given value.
        /// </summary>
        /// <param name="value">The integer to check.</param>
        /// <returns>The number of bits set to 1.</returns>
        public static int PopCount(int value)
        {
            int count = 0;
            while (value != 0)
            {
                count += (value & 1); // Count set bits
                value >>= 1; // Shift right
            }
            return count;
        }

        /// <summary>
        /// Computes the logarithm base 2 of an integer.
        /// </summary>
        /// <param name="value">The integer to evaluate.</param>
        /// <returns>The highest bit position that is set (equivalent to log2 of the number).</returns>
        public static int Log2(int value)
        {
            int log = 0;
            while ((value >>= 1) > 0)
            {
                log++; // Each shift represents division by 2
            }
            return log;
        }
    }
}
