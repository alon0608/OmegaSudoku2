using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    public class SudokuSolver
    {
        private readonly SudokuBoard _board;
        private readonly int _size;
        private readonly int _boxSize;

        private int[] rowUsed;
        private int[] colUsed;
        private int[] boxUsed;

        private List<(int row, int col)> emptyCells;

        public SudokuSolver(SudokuBoard board)
        {
            _board = board;
            _size = board.GetSize();
            _boxSize = board.GetBoxSize();

            rowUsed = new int[_size];
            colUsed = new int[_size];
            boxUsed = new int[_size];
            emptyCells = new List<(int row, int col)>();

            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < _size; i++)
            {
                rowUsed[i] = 0;
                colUsed[i] = 0;
                boxUsed[i] = 0;
            }

            emptyCells.Clear();

            for (int r = 0; r < _size; r++)
            {
                for (int c = 0; c < _size; c++)
                {
                    int val = _board.GetCellValue(r, c);
                    if (val == 0)
                    {
                        emptyCells.Add((r, c));
                    }
                    else
                    {
                        int mask = 1 << (val - 1);
                        rowUsed[r] |= mask;
                        colUsed[c] |= mask;
                        boxUsed[GetBoxIndex(r, c)] |= mask;
                    }
                }
            }
        }

        public bool Solve()
        {
            return Backtrack(0);
        }

        private bool Backtrack(int index)
        {
            if (index == emptyCells.Count)
                return true;

            int minOptions = _size + 1;
            int selectedIndex = -1;

            for (int i = index; i < emptyCells.Count; i++)
            {
                var (r, c) = emptyCells[i];
                int options = CountOptions(r, c);
                if (options < minOptions)
                {
                    minOptions = options;
                    selectedIndex = i;
                    if (options == 1)
                        break;
                }
            }

            if (selectedIndex == -1)
                return false;

            Swap(emptyCells, index, selectedIndex);
            var (row, col) = emptyCells[index];
            int usedMask = rowUsed[row] | colUsed[col] | boxUsed[GetBoxIndex(row, col)];

            for (int val = 1; val <= _size; val++)
            {
                int mask = 1 << (val - 1);
                if ((usedMask & mask) == 0)
                {
                    _board.SetCellValue(row, col, val);
                    rowUsed[row] |= mask;
                    colUsed[col] |= mask;
                    boxUsed[GetBoxIndex(row, col)] |= mask;

                    if (Backtrack(index + 1))
                        return true;

                    _board.SetCellValue(row, col, 0);
                    rowUsed[row] &= ~mask;
                    colUsed[col] &= ~mask;
                    boxUsed[GetBoxIndex(row, col)] &= ~mask;
                }
            }

            return false;
        }

        private int CountOptions(int row, int col)
        {
            int used = rowUsed[row] | colUsed[col] | boxUsed[GetBoxIndex(row, col)];
            int count = 0;
            for (int val = 1; val <= _size; val++)
            {
                if ((used & (1 << (val - 1))) == 0)
                    count++;
            }
            return count;
        }

        private void Swap(List<(int row, int col)> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        private int GetBoxIndex(int row, int col)
        {
            return (row / _boxSize) * _boxSize + (col / _boxSize);
        }
    }
}

