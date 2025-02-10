using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    public static class SudokuInputValidation
    {
        public static void ValidateInput(string input, int maxSize = 25)
        {
            int length = input.Length;
            int sqrt = (int)Math.Sqrt(length);

            if (sqrt * sqrt != length)
                throw new ArgumentException($"The input length is invalid. The number of characters must be a perfect square (1X1,4X4,16X16,25X25)");
            if (length > maxSize * maxSize)
                throw new ArgumentException($"Invalid Sudoku size: you entered {sqrt} X {sqrt} sudoku board but the maximum is  {maxSize} X {maxSize} sudoku.");

            int boxSize = (int)Math.Sqrt(sqrt);

            foreach (char c in input)
            {
                if (!IsValidSudokuChar(c, sqrt))
                    throw new ArgumentException($"Invalid character detected: '{c}' is not within the expected ASCII range.");
            }

            int[,] board = ConvertToBoard(input, sqrt);
            SudokuValidation.ValidateSudokuRules(board, sqrt, boxSize);
        }

        private static bool IsValidSudokuChar(char c, int size)
        {
            if (c == '0') return true;

            if (size <= 9)
            {
                return c >= '1' && c <= (char)('0' + size);
            }
            else
            {
                return (c >= '1' && c <= '9') || (c >= ':' && c < (char)(':' + (size - 9)));
            }
        }


        private static int ConvertCharToValue(char c)
        {
            if (c >= '1' && c <= '9') return c - '0';
            if (c >= ':' && c <= '~') return 10 + (c - ':');
            return 0;
        }

        private static int[,] ConvertToBoard(string input, int size)
        {
            int[,] board = new int[size, size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    board[i, j] = ConvertCharToValue(input[i * size + j]);

            return board;
        }

        public static char ValueToChar(int value)
        {
            if (value >= 1 && value <= 9) return (char)('0' + value);
            if (value >= 10) return (char)(':' + (value - 10));
            return '0';
        }
    }
}
