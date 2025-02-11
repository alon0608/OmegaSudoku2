using AlonSudoku.Core.BoardStateManagerClass;
using AlonSudoku.Core.SudokuBoardClass;

namespace AlonSudoku.Core.SolverState
{
    /// <summary>
    /// Represents the state of a Sudoku solver, including constraints 
    /// and tracking of empty cells.
    /// </summary>
    public class SolverState
    {
        /// <summary>
        /// The size of the Sudoku grid.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// The size of each box in the grid.
        /// </summary>
        public int BoxSize { get; private set; }

        /// <summary>
        /// Bitmask representing used numbers in each row.
        /// </summary>
        public int[] RowConstraints { get; private set; }

        /// <summary>
        /// Bitmask representing used numbers in each column.
        /// </summary>
        public int[] ColConstraints { get; private set; }

        /// <summary>
        /// Bitmask representing used numbers in each box.
        /// </summary>
        public int[] BoxConstraints { get; private set; }

        /// <summary>
        /// Tracks the empty cell positions for each row.
        /// </summary>
        public List<int>[] EmptyCellsInRow { get; private set; }

        /// <summary>
        /// Tracks the empty cell positions for each column.
        /// </summary>
        public List<int>[] EmptyCellsInCol { get; private set; }

        /// <summary>
        /// Tracks the empty cell positions for each box.
        /// </summary>
        public List<int>[] EmptyCellsInBox { get; private set; }

        /// <summary>
        /// Initializes the solver state based on the given Sudoku board.
        /// </summary>
        /// <param name="board">The Sudoku board to analyze.</param>
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

            // Initialize lists for tracking empty cells
            for (int i = 0; i < Size; i++)
            {
                EmptyCellsInRow[i] = new List<int>();
                EmptyCellsInCol[i] = new List<int>();
                EmptyCellsInBox[i] = new List<int>();
            }

            // Populate the state based on the current board configuration
            Initialize(board);
        }

        /// <summary>
        /// Scans the board and updates constraints and empty cell tracking.
        /// </summary>
        /// <param name="board">The Sudoku board being analyzed.</param>
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
                        // Store empty cell positions for easy lookup
                        EmptyCellsInRow[r].Add(c);
                        EmptyCellsInCol[c].Add(r);
                        EmptyCellsInBox[boxIndex].Add(r * Size + c);
                    }
                    else
                    {
                        // Update bitmask constraints for placed numbers
                        int mask = 1 << (val - 1);
                        RowConstraints[r] |= mask;
                        ColConstraints[c] |= mask;
                        BoxConstraints[boxIndex] |= mask;
                    }
                }
            }
        }
    }
}
