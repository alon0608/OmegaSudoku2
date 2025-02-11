using AlonSudoku.Board;
using AlonSudoku.Solvers;

namespace AlonSudokuTests
{
    [TestClass]
    public sealed class SolveableSmallSudokus
    {
        [TestMethod]
        public void Hard_Small_Sudoko1()
        {
            string validSudokuInput = "400000805030000000000700000020000060000080400000010000000006030705002000001040000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Hard_Small_Sudoko2()
        {
            string validSudokuInput = "900800000000000500000000000020010003010000060000400070708600000000030100400000200";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Hard_Small_Sudoko3()
        {
            string validSudokuInput = "005300000800000020070010500400005300010070006003200080060500009004000030000009700";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Hard_Small_Sudoko4()
        {
            string validSudokuInput = "000006000059000008200008000045000000003000000006003054000325006000000000000000000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Hard_Small_Sudoko5()
        {
            string validSudokuInput = "000006000059000008200008000045000000003000000006003054000325006000000000000000000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Hard_Small_Sudoko6()
        {
            string validSudokuInput = "800000000095000000067000000000020485000403192000000736000651947000732518000894263";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Hard_Small_Sudoko7()
        {
            string validSudokuInput = "800000000059000000076000000000978245000653198000412736000591000000836010000724080";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Hard_Small_Sudoko8()
        {
            string validSudokuInput = "800000000059000000067000000000756238000418796000923415000571060000892107000634000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Hard_Small_Sudoko9()
        {
            string validSudokuInput = "800000000095000000076000000000974286000512943000638517000791060000423708000856000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Hard_Small_Sudoko10()
        {
            string validSudokuInput = "800000000059000000067000000000030265000406198000000437000753916000891742000624583";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }
    }
}
