using AlonSudoku.Core.SudokuBoardClass;
using AlonSudoku.Solvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
