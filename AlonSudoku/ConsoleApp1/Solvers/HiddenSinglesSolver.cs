using AlonSudoku.Core.SudokuBoardClass;
using AlonSudoku.Core.BoardStateManagerClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlonSudoku.Solvers
{
    /// <summary>
    /// Implements the Hidden Singles solving technique for Sudoku.
    /// This strategy identifies cells where a number can only appear once in a row, column, or box.
    /// </summary>
    public static class HiddenSinglesSolver
    {
        /// <summary>
        /// Applies the Hidden Singles strategy to the entire Sudoku board.
        /// A Hidden Single occurs when a number has only one possible position in a given row, column, or box.
        /// </summary>
        /// <param name="board">The Sudoku board.</param>
        /// <param name="rowUsed">Bitmasks tracking used numbers in each row.</param>
        /// <param name="colUsed">Bitmasks tracking used numbers in each column.</param>
        /// <param name="boxUsed">Bitmasks tracking used numbers in each box.</param>
        /// <param name="size">The size of the Sudoku board.</param>
        /// <param name="boxSize">The size of a Sudoku box.</param>
        /// <returns>True if at least one Hidden Single was found and placed, otherwise false.</returns>
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

        /// <summary>
        /// Searches for Hidden Singles in a specific unit and places them if found.
        /// </summary>
        /// <param name="board">The Sudoku board.</param>
        /// <param name="rowUsed">Bitmasks for row constraints.</param>
        /// <param name="colUsed">Bitmasks for column constraints.</param>
        /// <param name="boxUsed">Bitmasks for box constraints.</param>
        /// <param name="size">The size of the board.</param>
        /// <param name="boxSize">The size of each box.</param>
        /// <param name="unitType">The type of unit to analyze (row, column, or box).</param>
        /// <returns>True if any numbers were placed, otherwise false.</returns>
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

                // Get all the cells in the current unit (row, column, or box)
                List<(int row, int col)> cells = GetUnitCells(unitType, unit, size, boxSize);
                foreach ((int r, int c) in cells)
                {
                    if (board.GetCellValue(r, c) != 0)
                        continue;

                    int usedMask = rowUsed[r] | colUsed[c] | boxUsed[BoardStateManager.GetBoxIndex(r, c, boxSize)];
                    int options = ~usedMask & (1 << size) - 1;

                    for (int val = 1; val <= size; val++)
                    {
                        if ((options & 1 << val - 1) != 0)
                        {
                            valueToCells[val].Add((r, c));
                        }
                    }
                }

                // Place numbers if a Hidden Single is found
                foreach (KeyValuePair<int, List<(int row, int col)>> kvp in valueToCells)
                {
                    int val = kvp.Key;
                    List<(int row, int col)> possibleCells = kvp.Value;
                    if (possibleCells.Count == 1)
                    {
                        (int r, int c) = possibleCells[0];
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

        /// <summary>
        /// Retrieves all cells in a given row, column, or box.
        /// </summary>
        /// <param name="unitType">The type of unit.</param>
        /// <param name="unit">The index of the unit.</param>
        /// <param name="size">The size of the Sudoku board.</param>
        /// <param name="boxSize">The size of each box.</param>
        /// <returns>A list of cell coordinates within the specified unit.</returns>
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
                    int startRow = unit / boxSize * boxSize;
                    int startCol = unit % boxSize * boxSize;
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

        /// <summary>
        /// Represents the different unit types in Sudoku (row, column, and box).
        /// </summary>
        private enum UnitType
        {
            Row,
            Column,
            Box
        }
    }
}
