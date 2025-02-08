using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    public static class NakedSinglesSolver
    {
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
                    if (board.GetCellValue(r, c) != 0)
                        continue;
                    int usedMask = rowUsed[r] | colUsed[c] | boxUsed[SolutionHandler.GetBoxIndex(r, c, boxSize)];
                    int options = ~usedMask & ((1 << size) - 1);
                    if (SolutionHandler.PopCount(options) == 1)
                    {
                        int val = SolutionHandler.Log2(options) + 1;
                        SolutionHandler.AssignValue(board, rowUsed, colUsed, boxUsed, r, c, val, boxSize);
                        progress = true;
                    }
                }
            }
            return progress;
        }
    }
}
