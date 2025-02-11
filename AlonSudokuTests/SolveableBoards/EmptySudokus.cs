using AlonSudoku.Board;
using AlonSudoku.Solvers;

namespace AlonSudokuTests.SolveableBoards
{
    [TestClass]
    public sealed class EmptySudokus
    {
        [TestMethod]
        public void Empty_Sudoku_1X1()
        {
            string validSudokuInput = "0";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Empty_Sudoku_4X4()
        {
            string validSudokuInput = "0000000000000000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Empty_Sudoku_9X9()
        {
            string validSudokuInput = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Empty_Sudoku_16X16()
        {
            string validSudokuInput = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Empty_Sudoku_25X25()
        {
            string validSudokuInput = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }
    }
}
