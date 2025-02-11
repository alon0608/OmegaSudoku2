using AlonSudoku.Core.SudokuBoardClass;

namespace AlonSudoku.Validation
{
    /// <summary>
    /// Provides validation methods for checking if a Sudoku board follows the rules.
    /// This includes verifying rows, columns, and boxes to ensure no duplicates.
    /// </summary>
    public static class SudokuValidation
    {
        /// <summary>
        /// Checks if a given Sudoku board is valid.
        /// A board is valid if all rows, columns, and boxes contain no duplicate numbers (excluding zeros).
        /// </summary>
        /// <param name="board">The Sudoku board to validate.</param>
        /// <returns>True if the board is valid, otherwise false.</returns>
        public static bool IsValid(SudokuBoard board)
        {
            int size = board.GetSize();

            for (int i = 0; i < size; i++)
            {
                if (!IsValidGroup(GetRow(board, i)) ||
                    !IsValidGroup(GetColumn(board, i)) ||
                    !IsValidGroup(GetBox(board, i)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a group of numbers row, column, or box is valid.
        /// A valid group contains no duplicate numbers excluding zeros.
        /// </summary>
        /// <param name="group">An array representing a row, column, or box.</param>
        /// <returns>True if the group is valid, otherwise false.</returns>
        private static bool IsValidGroup(int[] group)
        {
            HashSet<int> seen = new HashSet<int>();
            foreach (int val in group)
            {
                if (val == 0) continue; // Ignore empty cells
                if (seen.Contains(val)) return false; // Duplicate found
                seen.Add(val);
            }
            return true;
        }

        /// <summary>
        /// Retrieves all values from a specific row in the Sudoku board.
        /// </summary>
        /// <param name="board">The Sudoku board.</param>
        /// <param name="row">The row index .</param>
        /// <returns>An array containing the numbers from the row.</returns>
        private static int[] GetRow(SudokuBoard board, int row)
        {
            int size = board.GetSize();
            int[] result = new int[size];
            for (int col = 0; col < size; col++)
                result[col] = board.GetCellValue(row, col);
            return result;
        }

        /// <summary>
        /// Retrieves all values from a specific column in the Sudoku board.
        /// </summary>
        /// <param name="board">The Sudoku board.</param>
        /// <param name="col">The column index .</param>
        /// <returns>An array containing the numbers from the column.</returns>
        private static int[] GetColumn(SudokuBoard board, int col)
        {
            int size = board.GetSize();
            int[] result = new int[size];
            for (int row = 0; row < size; row++)
                result[row] = board.GetCellValue(row, col);
            return result;
        }

        /// <summary>
        /// Retrieves all values from a specific box in the Sudoku board.
        /// </summary>
        /// <param name="board">The Sudoku board.</param>
        /// <param name="boxIndex">The box index .</param>
        /// <returns>An array containing the numbers from the box.</returns>
        private static int[] GetBox(SudokuBoard board, int boxIndex)
        {
            int boxSize = board.GetBoxSize();
            int size = board.GetSize();
            int[] result = new int[size];
            int startRow = boxIndex / boxSize * boxSize;
            int startCol = boxIndex % boxSize * boxSize;
            int index = 0;

            for (int r = 0; r < boxSize; r++)
                for (int c = 0; c < boxSize; c++)
                    result[index++] = board.GetCellValue(startRow + r, startCol + c);

            return result;
        }

        /// <summary>
        /// Validates the Sudoku board and ensures no rule violations.
        /// Throws an exception if there are duplicate values in rows, columns, or boxes.
        /// </summary>
        /// <param name="board">A integer array representing the Sudoku grid.</param>
        /// <param name="size">The size of the board.</param>
        /// <param name="boxSize">The size of a single Sudoku box.</param>
        /// <exception cref="ArgumentException">Thrown if a duplicate value is found.</exception>
        public static void ValidateSudokuRules(int[,] board, int size, int boxSize)
        {
            HashSet<int>[] rows = new HashSet<int>[size];
            HashSet<int>[] cols = new HashSet<int>[size];
            HashSet<int>[] boxes = new HashSet<int>[size];

            for (int i = 0; i < size; i++)
            {
                rows[i] = new HashSet<int>();
                cols[i] = new HashSet<int>();
                boxes[i] = new HashSet<int>();
            }

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    int value = board[r, c];
                    if (value == 0) continue;

                    int boxIndex = r / boxSize * boxSize + c / boxSize;

                    if (!rows[r].Add(value))
                        throw new ArgumentException($"Duplicate value '{SudokuInputValidation.ValueToChar(value)}' in row {r + 1}.");
                    if (!cols[c].Add(value))
                        throw new ArgumentException($"Duplicate value '{SudokuInputValidation.ValueToChar(value)}' in column {c + 1}.");
                    if (!boxes[boxIndex].Add(value))
                        throw new ArgumentException($"Duplicate value '{SudokuInputValidation.ValueToChar(value)}' in box {boxIndex + 1}.");
                }
            }
        }
    }
}
