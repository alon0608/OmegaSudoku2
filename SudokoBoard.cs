using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SudokuOmega7
{
    public class SudokuBoard : IBoardSize
    {
        private readonly int[,] _board;
        private readonly int _size;
        private readonly int _boxSize;

        public SudokuBoard(string input)
        {
            _size = (int)Math.Sqrt(input.Length);
            _boxSize = (int)Math.Sqrt(_size);

            if (_size * _size != input.Length)
                throw new ArgumentException("Wrong input length");

            _board = new int[_size, _size];
            InitializeBoard(input);
        }

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

        private int ConvertToNumber(char c)
        {
            if (char.IsDigit(c))
                return c - '0';
            if (c >= ':' && c <= 'Z')
                return c - '0';

            throw new ArgumentException($"תווים לא חוקיים בקלט: '{c}'.");
        }

        private char ConvertToChar(int value)
        {
            return (char)('0' + value);
        }

        public int GetSize()
        {
            return _size;
        }

        public int GetBoxSize()
        {
            return _boxSize;
        }

        public int GetCellValue(int row, int col)
        {
            return _board[row, col];
        }

        public void SetCellValue(int row, int col, int value)
        {
            _board[row, col] = value;
        }

        public void PrintBoard()
        {
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    Console.Write(_board[i, j] == 0 ? '.' : ConvertToChar(_board[i, j]));
                    if ((j + 1) % _boxSize == 0 && j + 1 != _size)
                        Console.Write(" | ");
                }
                Console.WriteLine();
                if ((i + 1) % _boxSize == 0 && i + 1 != _size)
                    Console.WriteLine(new string('-', _size * 2));
            }
        }
    }
}

