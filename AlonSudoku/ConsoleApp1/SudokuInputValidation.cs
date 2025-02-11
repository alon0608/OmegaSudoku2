using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlonSudoku
{
    /// <summary>
    /// Handles validation and conversion of Sudoku input strings.
    /// Ensures the input Sudoku size constraints and contains only valid characters.
    /// </summary>
    public static class SudokuInputValidation
    {
        /// <summary>
        /// Validates the input string to ensure it represents a valid Sudoku board.
        /// Checks that the length is a perfect square and that all characters are valid.
        /// </summary>
        /// <param name="input">The Sudoku board represented as a single string.</param>
        /// <param name="maxSize">The maximum allowed Sudoku board size (default is 25x25).</param>
        /// <exception cref="ArgumentException">Thrown if the input is not a valid Sudoku format.</exception>
        public static void ValidateInput(string input, int maxSize = 25)
        {
            int length = input.Length;
            int sqrt = (int)Math.Sqrt(length);

            // Check if the length is a perfect square (valid Sudoku board dimensions)
            if (sqrt * sqrt != length)
                throw new ArgumentException($"The input length is invalid. The number of characters must be a perfect square (1x1, 4x4, 16x16, 25x25).");

            // Ensure the board is within the allowed max size
            if (length > maxSize * maxSize)
                throw new ArgumentException($"Invalid Sudoku size: you entered a {sqrt}x{sqrt} board, but the maximum allowed size is {maxSize}x{maxSize}.");

            int boxSize = (int)Math.Sqrt(sqrt);

            // Check for invalid characters in the input string
            foreach (char c in input)
            {
                if (!IsValidSudokuChar(c, sqrt))
                    throw new ArgumentException($"Invalid character detected: '{c}' is not within the expected ASCII range.");
            }

            // Convert to a board representation and validate Sudoku rules
            int[,] board = ConvertToBoard(input, sqrt);
            SudokuValidation.ValidateSudokuRules(board, sqrt, boxSize);
        }

        /// <summary>
        /// Checks if a character is a valid Sudoku character based on board size.
        /// Supports boards larger than 9x9 by allowing extended characters.
        /// </summary>
        /// <param name="c">The character to validate.</param>
        /// <param name="size">The size of the Sudoku board.</param>
        /// <returns>True if the character is valid, otherwise false.</returns>
        private static bool IsValidSudokuChar(char c, int size)
        {
            if (c == '0') return true; // '0' represents an empty cell

            // For small Sudoku boards (size <= 9), only digits 1-9 are valid
            if (size <= 9)
            {
                return c >= '1' && c <= (char)('0' + size);
            }
            // For larger boards, additional ASCII characters are used
            else
            {
                return (c >= '1' && c <= '9') || (c >= ':' && c < (char)(':' + (size - 9)));
            }
        }

        /// <summary>
        /// Converts a Sudoku character to its integer representation.
        /// Supports both numeric digits (1-9) and extended characters for larger boards.
        /// </summary>
        /// <param name="c">The character to convert.</param>
        /// <returns>The corresponding integer value.</returns>
        private static int ConvertCharToValue(char c)
        {
            if (c >= '1' && c <= '9') return c - '0';
            if (c >= ':' && c <= '~') return 10 + (c - ':'); // Extended range for larger boards
            return 0;
        }

        /// <summary>
        /// Converts a Sudoku board from a string format into a integer array.
        /// </summary>
        /// <param name="input">The input string representing the Sudoku board.</param>
        /// <param name="size">The size of the board.</param>
        /// <returns>A integer array representing the board.</returns>
        private static int[,] ConvertToBoard(string input, int size)
        {
            int[,] board = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    board[i, j] = ConvertCharToValue(input[i * size + j]);

            return board;
        }

        /// <summary>
        /// Converts an integer Sudoku value into its corresponding character representation.
        /// Used when displaying the board.
        /// </summary>
        /// <param name="value">The Sudoku value to convert.</param>
        /// <returns>The corresponding character representation.</returns>
        public static char ValueToChar(int value)
        {
            if (value >= 1 && value <= 9) return (char)('0' + value);
            if (value >= 10) return (char)(':' + (value - 10)); // Extended characters for large boards
            return '0'; // Default for empty cells
        }
    }
}
