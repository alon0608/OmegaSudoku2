using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    public class SudokuValidation:IValidation
    {
        private int[] GetRow(SudokuBoard board, int row)
        {
            int size = board.GetSize();
            int[] result = new int[size];
            for (int col = 0; col < size; col++)
                result[col] = board.GetCellValue(row, col);
            return result;
        }

        private int[] GetColumn(SudokuBoard board, int col)
        {
            int size = board.GetSize();
            int[] result = new int[size];
            for (int row = 0; row < size; row++)
                result[row] = board.GetCellValue(row, col);
            return result;
        }

        private int[] GetBox(SudokuBoard board, int boxIndex)
        {
            int boxSize = board.GetBoxSize();
            int[] result = new int[boxSize * boxSize];
            int startRow = (boxIndex / boxSize) * boxSize;
            int startCol = (boxIndex % boxSize) * boxSize;
            int index = 0;

            for (int r = 0; r < boxSize; r++)
                for (int c = 0; c < boxSize; c++)
                    result[index++] = board.GetCellValue(startRow + r, startCol + c);

            return result;
        }
        private bool IsValidGroup(int[] group)
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
    }
}
