using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    public static class HiddenSinglesSolver
    {
        public static bool ApplyHiddenSingles(
            SudokuBoard board,
            int[] rowUsed,
            int[] colUsed,
            int[] boxUsed,
            int size,
            int boxSize)
        {
            bool progress = false;
            progress |= ApplyHiddenSinglesForUnit(board, rowUsed, colUsed, boxUsed, size, boxSize, UnitType.Row);
            progress |= ApplyHiddenSinglesForUnit(board, rowUsed, colUsed, boxUsed, size, boxSize, UnitType.Column);
            progress |= ApplyHiddenSinglesForUnit(board, rowUsed, colUsed, boxUsed, size, boxSize, UnitType.Box);
            return progress;
        }

        private static bool ApplyHiddenSinglesForUnit(
            SudokuBoard board,
            int[] rowUsed,
            int[] colUsed,
            int[] boxUsed,
            int size,
            int boxSize,
            UnitType unitType)
        {
            bool progress = false;
            for (int unit = 0; unit < size; unit++)
            {
                Dictionary<int, List<(int row, int col)>> valueToCells = new Dictionary<int, List<(int, int)>>();
                for (int i = 1; i <= size; i++)
                {
                    valueToCells[i] = new List<(int, int)>();
                }
                List<(int row, int col)> cells = GetUnitCells(unitType, unit, size, boxSize);
                foreach (var (r, c) in cells)
                {
                    if (board.GetCellValue(r, c) != 0)
                        continue;
                    int usedMask = rowUsed[r] | colUsed[c] | boxUsed[BoardStateManager.GetBoxIndex(r, c, boxSize)];
                    int options = ~usedMask & ((1 << size) - 1);
                    for (int val = 1; val <= size; val++)
                    {
                        if ((options & (1 << (val - 1))) != 0)
                        {
                            valueToCells[val].Add((r, c));
                        }
                    }
                }
                foreach (var kvp in valueToCells)
                {
                    int val = kvp.Key;
                    List<(int row, int col)> possibleCells = kvp.Value;
                    if (possibleCells.Count == 1)
                    {
                        var (r, c) = possibleCells[0];
                        if (board.GetCellValue(r, c) == 0)
                        {
                            BoardStateManager.PlaceNumber(board, rowUsed, colUsed, boxUsed, r, c, val, boxSize);
                            progress = true;
                        }
                    }
                }
            }
            return progress;
        }

        private static List<(int row, int col)> GetUnitCells(UnitType unitType, int unit, int size, int boxSize)
        {
            List<(int row, int col)> cells = new List<(int row, int col)>();
            switch (unitType)
            {
                case UnitType.Row:
                    for (int c = 0; c < size; c++)
                        cells.Add((unit, c));
                    break;
                case UnitType.Column:
                    for (int r = 0; r < size; r++)
                        cells.Add((r, unit));
                    break;
                case UnitType.Box:
                    int startRow = (unit / boxSize) * boxSize;
                    int startCol = (unit % boxSize) * boxSize;
                    for (int r = 0; r < boxSize; r++)
                    {
                        for (int c = 0; c < boxSize; c++)
                        {
                            cells.Add((startRow + r, startCol + c));
                        }
                    }
                    break;
            }
            return cells;
        }

        private enum UnitType
        {
            Row,
            Column,
            Box
        }
    }
}
