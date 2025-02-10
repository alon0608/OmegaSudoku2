using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    /// <summary>
    /// Represents a Sudoku board that can store and manipulate the puzzle's state.
    /// The board is initialized from a string, where '0' represents an empty cell.
    /// </summary>
    public class SudokuBoard
    {
        private readonly int[,] _board;
        private readonly int _size;
        private readonly int _boxSize;

        /// <summary>
        /// Creates a Sudoku board from an input string.
        /// The string must have a length that forms a perfect square (81 for a 9x9 board).
        /// </summary>
        /// <param name="input">A string representation of the board, where '0' represents empty cells.</param>
        public SudokuBoard(string input)
        {
            _size = (int)Math.Sqrt(input.Length);
            _boxSize = (int)Math.Sqrt(_size);

            if (_size * _size != input.Length)
                throw new ArgumentException("Wrong input length");

            _board = new int[_size, _size];
            InitializeBoard(input);
        }

        /// <summary>
        /// Populates the board using the input string.
        /// </summary>
        /// <param name="input">A string containing the Sudoku numbers in row order.</param>
        private void InitializeBoard(string input)
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    char c = input[i * _size + j];
                    _board[i, j] = c == '0' ? 0 : ConvertToNumber(c);
                }
            }
        }

        /// <summary>
        /// Converts a character to a number (1-9).
        /// </summary>
        /// <param name="c">A character representing a digit.</param>
        /// <returns>The integer value of the character.</returns>
        /// <exception cref="ArgumentException">Thrown if the character is invalid.</exception>
        public int ConvertToNumber(char c)
        {
            if (char.IsDigit(c))
                return c - '0';
            if (c >= ':' && c <= 'Z')
                return c - '0';

            throw new ArgumentException($"Wrong character: '{c}'.");
        }

        /// <summary>
        /// Converts an integer value to a character.
        /// </summary>
        /// <param name="value">A number from 1 to 9.</param>
        /// <returns>A character representing the number.</returns>
        private char ConvertToChar(int value)
        {
            return (char)('0' + value);
        }

        /// <summary>
        /// Gets the size of the Sudoku board (25 for a 25x25 board).
        /// </summary>
        /// <returns>The board size.</returns>
        public int GetSize()
        {
            return _size;
        }

        /// <summary>
        /// Gets the box size (4 for a 16x16 board, since boxes are 4x4).
        /// </summary>
        /// <returns>The size of a single Sudoku sub-box.</returns>
        public int GetBoxSize()
        {
            return _boxSize;
        }

        /// <summary>
        /// Gets the value of a specific cell.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="col">Column index.</param>
        /// <returns>The number in the cell, or 0 if empty.</returns>
        public int GetCellValue(int row, int col)
        {
            return _board[row, col];
        }

        /// <summary>
        /// Sets the value of a specific cell.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="col">Column index.</param>
        /// <param name="value">The number to place in the cell.</param>
        public void SetCellValue(int row, int col, int value)
        {
            _board[row, col] = value;
        }

        /// <summary>
        /// Prints the Sudoku board to the console in a formatted way.
        /// Empty cells are displayed as '.' and boxes are separated.
        /// </summary>
        public void PrintBoard()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Console.Write(" ");
                    Console.Write(_board[i, j] == 0 ? '.' : ConvertToChar(_board[i, j]));
                    if ((j + 1) % _boxSize == 0 && j + 1 != _size)
                        Console.Write(" | ");
                }
                Console.WriteLine();
                if ((i + 1) % _boxSize == 0 && i + 1 != _size)
                    Console.WriteLine(new string('-', _size * 3));
            }
        }

        /// <summary>
        /// Creates a deep copy of the Sudoku board.
        /// </summary>
        /// <returns>A new SudokuBoard object with the same values.</returns>
        public SudokuBoard Clone()
        {
            SudokuBoard clone = new SudokuBoard(new string('0', _size * _size));

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    clone.SetCellValue(i, j, _board[i, j]);
                }
            }

            return clone;
        }

        /// <summary>
        /// Copies all values from another Sudoku board.
        /// </summary>
        /// <param name="other">The board to copy from.</param>
        /// <exception cref="ArgumentException">Thrown if board sizes do not match.</exception>
        public void CopyFrom(SudokuBoard other)
        {
            if (other.GetSize() != this._size)
                throw new ArgumentException("Board sizes do not match.");

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    this._board[i, j] = other.GetCellValue(i, j);
                }
            }
        }

        /// <summary>
        /// Resets the board by clearing all values (sets all cells to 0).
        /// </summary>
        public void ResetBoard()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _board[i, j] = 0;
                }
            }
        }
    }
}
