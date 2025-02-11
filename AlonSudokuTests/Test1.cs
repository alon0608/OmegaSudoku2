using AlonSudoku.Core.SudokuBoardClass;
using AlonSudoku.Solvers;

namespace AlonSudokuTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void Solve_ValidSudoku()
        {
            string validSudokuInput ="530070000600195000098000060800060003400803001700020006060000280000419005000080079"; 

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result, "The Sudoku solver should solve a valid Sudoku puzzle.");
        }
    }
}
