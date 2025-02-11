using AlonSudoku.Board;
using AlonSudoku.Solvers;

namespace AlonSudokuTests.SolveableBoards
{
    [TestClass]
    public sealed class UnsolveableBoards
    {
        [TestMethod]
        public void Small_UnsolveBoard1()
        {
            string validSudokuInput = "003000000400080037008000100040060073000900010000002000004070068600004000700000500";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Small_UnsolveBoard2()
        {
            string validSudokuInput = "023000009400000100090030040200910004000007800900040002300090001060000000000500000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Small_UnsolveBoard3()
        {
            string validSudokuInput = "100006080000700000090050000000560030300000000000003801500001060000020400802005010";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Medium_UnsolveBoard()
        {
            string validSudokuInput = ";0?0=>010690000000710000500:?0;4000000<0400070=005<3000800000000500@000:?80>10004<30>?8;00=20000>?8;270060000000000000900000000?0000?00000>0=000?3:0000>0026000000;>61029@0<00000100<0@00:40000800500:0?;>012600800?0;0000090<0@0;07000005<00?8:00003050:4080709";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsFalse(result);
        }
    }
}
