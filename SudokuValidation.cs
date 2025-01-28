using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SudokuOmega7
{
    public static class SudokuValidation
    {
        public static bool IsValid(SudokuBoard board)
        {
            int size = board.GetSize();

            for (int i = 0; i < size; i++)
            {
                if (!IsValidGroup(GetRow(board, i)) ||
                    !IsValidGroup(GetColumn(board, i)) ||
                    !IsValidGroup(GetBox(board, i)))
                    return false;
            }
            return true;
        }

        private static bool IsValidGroup(int[] group)
        {
            var seen = new HashSet<int>();
            foreach (var val in group)
            {
                if (val == 0) continue;
                if (seen.Contains(val)) return false;
                seen.Add(val);
            }
            return true;
        }

        private static int[] GetRow(SudokuBoard board, int row)
        {
            int size = board.GetSize();
            int[] result = new int[size];
            for (int col = 0; col < size; col++)
                result[col] = board.GetCellValue(row, col);
            return result;
        }

        private static int[] GetColumn(SudokuBoard board, int col)
        {
            int size = board.GetSize();
            int[] result = new int[size];
            for (int row = 0; row < size; row++)
                result[row] = board.GetCellValue(row, col);
            return result;
        }

        private static int[] GetBox(SudokuBoard board, int boxIndex)
        {
            int boxSize = board.GetBoxSize();
            int size = board.GetSize();
            int[] result = new int[size];
            int startRow = (boxIndex / boxSize) * boxSize;
            int startCol = (boxIndex % boxSize) * boxSize;
            int index = 0;

            for (int r = 0; r < boxSize; r++)
                for (int c = 0; c < boxSize; c++)
                    result[index++] = board.GetCellValue(startRow + r, startCol + c);

            return result;
        }
    }
}

