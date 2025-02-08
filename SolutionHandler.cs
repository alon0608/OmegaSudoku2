using ConsoleApp5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    public static class SolutionHandler
    {
        public static int GetBoxIndex(int row, int col, int boxSize)
        {
            return (row / boxSize) * boxSize + (col / boxSize);
        }

        public static bool IsSolved(SudokuBoard board)
        {
            int size = board.GetSize();
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (board.GetCellValue(r, c) == 0)
                        return false;
                }
            }
            return true;
        }

        public static void AssignValue(
            SudokuBoard board,
            int[] rowUsed,
            int[] colUsed,
            int[] boxUsed,
            int row,
            int col,
            int val,
            int boxSize)
        {
            board.SetCellValue(row, col, val);
            int mask = 1 << (val - 1);
            rowUsed[row] |= mask;
            colUsed[col] |= mask;
            boxUsed[GetBoxIndex(row, col, boxSize)] |= mask;
        }

        public static void UnassignValue(
            SudokuBoard board,
            int[] rowUsed,
            int[] colUsed,
            int[] boxUsed,
            int row,
            int col,
            int val,
            int boxSize)
        {
            board.SetCellValue(row, col, 0);
            int mask = ~(1 << (val - 1));
            rowUsed[row] &= mask;
            colUsed[col] &= mask;
            boxUsed[GetBoxIndex(row, col, boxSize)] &= mask;
        }

        public static int PopCount(int value)
        {
            int count = 0;
            while (value != 0)
            {
                count += (value & 1);
                value >>= 1;
            }
            return count;
        }

        public static int Log2(int value)
        {
            int log = 0;
            while ((value >>= 1) > 0)
            {
                log++;
            }
            return log;
        }

        public static void RestoreState(
            SudokuBoard board,
            SudokuBoard clonedBoard,
            int[] rowUsed,
            int[] clonedRowUsed,
            int[] colUsed,
            int[] clonedColUsed,
            int[] boxUsed,
            int[] clonedBoxUsed)
        {
            int size = board.GetSize();
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    int clonedVal = clonedBoard.GetCellValue(r, c);
                    board.SetCellValue(r, c, clonedVal);
                }
            }
            for (int i = 0; i < size; i++)
            {
                rowUsed[i] = clonedRowUsed[i];
                colUsed[i] = clonedColUsed[i];
                boxUsed[i] = clonedBoxUsed[i];
            }
        }
    }
}
