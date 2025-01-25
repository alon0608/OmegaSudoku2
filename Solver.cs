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

        private int[] rowUsed = new int[9];
        private int[] colUsed = new int[9];
        private int[] boxUsed = new int[9];

        private List<(int row, int col)> emptyCells = new List<(int row, int col)>();

        public SudokuSolver(SudokuBoard board)
        {
            _board = board;
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < 9; i++)
            {
                rowUsed[i] = 0;
                colUsed[i] = 0;
                boxUsed[i] = 0;
            }

            emptyCells.Clear();

            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
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

        private int CountOptions(int row, int col)
        {
            int used = rowUsed[row] | colUsed[col] | boxUsed[GetBoxIndex(row, col)];
            int count = 0;
            for (int val = 1; val <= 9; val++)
            {
                if ((used & (1 << (val - 1))) == 0)
                    count++;
            }
            return count;
        }
    }
}
