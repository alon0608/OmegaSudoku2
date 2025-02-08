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

        public SudokuSolver(SudokuBoard board)
        {
            _board = board;
            _size = board.GetSize();
            _boxSize = board.GetBoxSize();
            rowUsed = new int[_size];
            colUsed = new int[_size];
            boxUsed = new int[_size];
            Initialize();
        }

        private void Initialize()
        {
            for (int r = 0; r < _size; r++)
            {
                for (int c = 0; c < _size; c++)
                {
                    int val = _board.GetCellValue(r, c);
                    if (val != 0)
                    {
                        int mask = 1 << (val - 1);
                        rowUsed[r] |= mask;
                        colUsed[c] |= mask;
                        boxUsed[SolutionHandler.GetBoxIndex(r, c, _boxSize)] |= mask;
                    }
                }
            }
        }

        public bool Solve()
        {
            return Backtrack();
        }

        private bool Backtrack()
        {
            while (ApplyHeuristics()) ;
            if (SolutionHandler.IsSolved(_board))
                return true;
            var (selectedRow, selectedCol) = SelectNextCell();
            if (selectedRow == -1 && selectedCol == -1)
                return false;
            int boxIndexSelected = SolutionHandler.GetBoxIndex(selectedRow, selectedCol, _boxSize);
            int optionsMaskSelected = ~(rowUsed[selectedRow] | colUsed[selectedCol] | boxUsed[boxIndexSelected]) & ((1 << _size) - 1);
            for (int val = 1; val <= _size; val++)
            {
                int mask = 1 << (val - 1);
                if ((optionsMaskSelected & mask) != 0)
                {
                    SudokuBoard clonedBoard = _board.Clone();
                    int[] clonedRowUsed = (int[])rowUsed.Clone();
                    int[] clonedColUsed = (int[])colUsed.Clone();
                    int[] clonedBoxUsed = (int[])boxUsed.Clone();
                    SolutionHandler.AssignValue(_board, rowUsed, colUsed, boxUsed, selectedRow, selectedCol, val, _boxSize);
                    bool result = Backtrack();
                    if (result)
                        return true;
                    SolutionHandler.RestoreState(_board, clonedBoard, rowUsed, clonedRowUsed, colUsed, clonedColUsed, boxUsed, clonedBoxUsed);
                }
            }
            return false;
        }

        private bool ApplyHeuristics()
        {
            bool progress = false;
            progress |= NakedSinglesSolver.ApplyNakedSingles(_board, rowUsed, colUsed, boxUsed, _size, _boxSize);
            if (_size >= 16)
                progress |= HiddenSinglesSolver.ApplyHiddenSingles(_board, rowUsed, colUsed, boxUsed, _size, _boxSize);
            return progress;
        }

        private (int row, int col) SelectNextCell()
        {
            int minOptions = _size + 1;
            int selectedRow = -1;
            int selectedCol = -1;
            int highestDegree = -1;
            bool foundSingleOption = false;
            for (int r = 0; r < _size && !foundSingleOption; r++)
            {
                for (int c = 0; c < _size && !foundSingleOption; c++)
                {
                    if (_board.GetCellValue(r, c) != 0)
                        continue;
                    int usedMask = rowUsed[r] | colUsed[c] | boxUsed[SolutionHandler.GetBoxIndex(r, c, _boxSize)];
                    int options = ~usedMask & ((1 << _size) - 1);
                    int optionCount = SolutionHandler.PopCount(options);
                    if (optionCount < minOptions)
                    {
                        minOptions = optionCount;
                        selectedRow = r;
                        selectedCol = c;
                        highestDegree = CalculateDegree(r, c);
                        if (optionCount == 1)
                            foundSingleOption = true;
                    }
                    else if (optionCount == minOptions)
                    {
                        int degree = CalculateDegree(r, c);
                        if (degree > highestDegree)
                        {
                            selectedRow = r;
                            selectedCol = c;
                            highestDegree = degree;
                            if (optionCount == 1)
                                foundSingleOption = true;
                        }
                    }
                }
            }
            return (selectedRow, selectedCol);
        }

        private int CalculateDegree(int row, int col)
        {
            int degree = 0;
            for (int c = 0; c < _size; c++)
            {
                if (c != col && _board.GetCellValue(row, c) == 0)
                    degree++;
            }
            for (int r = 0; r < _size; r++)
            {
                if (r != row && _board.GetCellValue(r, col) == 0)
                    degree++;
            }
            int boxIndex = SolutionHandler.GetBoxIndex(row, col, _boxSize);
            int startRowBox = (boxIndex / _boxSize) * _boxSize;
            int startColBox = (boxIndex % _boxSize) * _boxSize;
            for (int rr = 0; rr < _boxSize; rr++)
            {
                for (int cc = 0; cc < _boxSize; cc++)
                {
                    int currentRow = startRowBox + rr;
                    int currentCol = startColBox + cc;
                    if ((currentRow != row || currentCol != col) && _board.GetCellValue(currentRow, currentCol) == 0)
                        degree++;
                }
            }
            return degree;
        }
    }
}

