using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    public static class BoardStateManager
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

        public static void PlaceNumber(SudokuBoard board, int[] rowConstraints, int[] colConstraints, int[] boxConstraints, int row, int col, int val, int boxSize)
        {
            board.SetCellValue(row, col, val);
            int mask = 1 << (val - 1);
            rowConstraints[row] |= mask;
            colConstraints[col] |= mask;
            boxConstraints[GetBoxIndex(row, col, boxSize)] |= mask;
        }

        public static void UndoMove(SudokuBoard board, SudokuBoard clonedBoard, int[] rowConstraints, int[] clonedRowConstraints, int[] colConstraints, int[] clonedColConstraints, int[] boxConstraints, int[] clonedBoxConstraints)
        {
            board.CopyFrom(clonedBoard);
            for (int i = 0; i < rowConstraints.Length; i++)
            {
                rowConstraints[i] = clonedRowConstraints[i];
                colConstraints[i] = clonedColConstraints[i];
                boxConstraints[i] = clonedBoxConstraints[i];
            }
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


    }
}
