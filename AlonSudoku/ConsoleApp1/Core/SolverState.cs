using AlonSudoku.Core.SudokuBoardClass;
using AlonSudoku.Core.BoardStateManagerClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlonSudoku.Core.SolverState
{
    public class SolverState
    {
        public int Size { get; private set; }
        public int BoxSize { get; private set; }
        public int[] RowConstraints { get; private set; }
        public int[] ColConstraints { get; private set; }
        public int[] BoxConstraints { get; private set; }
        public List<int>[] EmptyCellsInRow { get; private set; }
        public List<int>[] EmptyCellsInCol { get; private set; }
        public List<int>[] EmptyCellsInBox { get; private set; }

        public SolverState(SudokuBoard board)
        {
            Size = board.GetSize();
            BoxSize = board.GetBoxSize();
            RowConstraints = new int[Size];
            ColConstraints = new int[Size];
            BoxConstraints = new int[Size];

            EmptyCellsInRow = new List<int>[Size];
            EmptyCellsInCol = new List<int>[Size];
            EmptyCellsInBox = new List<int>[Size];

            for (int i = 0; i < Size; i++)
            {
                EmptyCellsInRow[i] = new List<int>();
                EmptyCellsInCol[i] = new List<int>();
                EmptyCellsInBox[i] = new List<int>();
            }

            Initialize(board);
        }

        private void Initialize(SudokuBoard board)
        {
            for (int r = 0; r < Size; r++)
            {
                for (int c = 0; c < Size; c++)
                {
                    int val = board.GetCellValue(r, c);
                    int boxIndex = BoardStateManager.GetBoxIndex(r, c, BoxSize);

                    if (val == 0)
                    {
                        EmptyCellsInRow[r].Add(c);
                        EmptyCellsInCol[c].Add(r);
                        EmptyCellsInBox[boxIndex].Add(r * Size + c);
                    }
                    else
                    {
                        int mask = 1 << val - 1;
                        RowConstraints[r] |= mask;
                        ColConstraints[c] |= mask;
                        BoxConstraints[boxIndex] |= mask;
                    }
                }
            }
        }
    }
}
