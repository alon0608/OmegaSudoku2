using System;

namespace SudokuOmega7
{
    public class SudokuSolver
    {
        private readonly SudokuBoard _board;
        private readonly SolverState _state;

        public SudokuSolver(SudokuBoard board)
        {
            _board = board;
            _state = new SolverState(board);
        }

        public bool SolveWithBacktracking()
        {
            return Backtrack();
        }

        private bool Backtrack()
        {
            while (ApplySolvingStrategies()) ;

            if (BoardStateManager.IsSolved(_board))
                return true;

            var (bestCandidateRow, bestCandidateCol) = FindBestCandidateCell();
            if (bestCandidateRow == -1 && bestCandidateCol == -1)
                return false;

            int boxIndex = BoardStateManager.GetBoxIndex(bestCandidateRow, bestCandidateCol, _state.BoxSize);
            int availableNumbersMask = ~(_state.RowConstraints[bestCandidateRow] |
                                         _state.ColConstraints[bestCandidateCol] |
                                         _state.BoxConstraints[boxIndex]) & ((1 << _state.Size) - 1);

            for (int val = 1; val <= _state.Size; val++)
            {
                int mask = 1 << (val - 1);
                if ((availableNumbersMask & mask) != 0)
                {
                    SudokuBoard clonedBoard = _board.Clone();
                    int[] clonedRowConstraints = (int[])_state.RowConstraints.Clone();
                    int[] clonedColConstraints = (int[])_state.ColConstraints.Clone();
                    int[] clonedBoxConstraints = (int[])_state.BoxConstraints.Clone();

                    BoardStateManager.PlaceNumber(_board, _state.RowConstraints, _state.ColConstraints, _state.BoxConstraints,
                                                  bestCandidateRow, bestCandidateCol, val, _state.BoxSize);

                    bool result = Backtrack();
                    if (result)
                        return true;

                    BoardStateManager.UndoMove(_board, clonedBoard, _state.RowConstraints, clonedRowConstraints,
                                               _state.ColConstraints, clonedColConstraints, _state.BoxConstraints, clonedBoxConstraints);
                }
            }

            return false;
        }

        private bool ApplySolvingStrategies()
        {
            bool progress = false;
            progress |= NakedSinglesSolver.ApplyNakedSingles(_board, _state.RowConstraints, _state.ColConstraints, _state.BoxConstraints, _state.Size, _state.BoxSize);
            if (_state.Size >= 16)
            {
                progress |= HiddenSinglesSolver.ApplyHiddenSingles(_board, _state.RowConstraints, _state.ColConstraints, _state.BoxConstraints, _state.Size, _state.BoxSize);
            }
            return progress;
        }

        private (int row, int col) FindBestCandidateCell()
        {
            int minOptions = _state.Size + 1;
            int bestCandidateRow = -1;
            int bestCandidateCol = -1;
            int highestDegree = -1;
            bool foundSingleOption = false;

            for (int r = 0; r < _state.Size; r++)
            {
                foreach (int c in _state.EmptyCellsInRow[r])
                {
                    if (_board.GetCellValue(r, c) != 0)
                        continue;

                    int usedMask = _state.RowConstraints[r] | _state.ColConstraints[c] | _state.BoxConstraints[BoardStateManager.GetBoxIndex(r, c, _state.BoxSize)];
                    int options = ~usedMask & ((1 << _state.Size) - 1);
                    int optionCount = BoardStateManager.PopCount(options);

                    int degree = _state.EmptyCellsInRow[r].Count + _state.EmptyCellsInCol[c].Count + _state.EmptyCellsInBox[BoardStateManager.GetBoxIndex(r, c, _state.BoxSize)].Count;

                    if (optionCount < minOptions || (optionCount == minOptions && degree > highestDegree))
                    {
                        minOptions = optionCount;
                        bestCandidateRow = r;
                        bestCandidateCol = c;
                        highestDegree = degree;
                        if (optionCount == 1)
                            foundSingleOption = true;
                    }
                }
            }

            return (bestCandidateRow, bestCandidateCol);
        }
    }
}
