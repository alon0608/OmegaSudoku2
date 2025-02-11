using AlonSudoku.Board;
using AlonSudoku.Solvers;

namespace AlonSudokuTests.SolveableBoards
{
    [TestClass]
    public sealed class SolveableMediumSudokus
    {
        [TestMethod]
        public void Medium_Sudoko1()
        {
            string validSudokuInput = "00=90000000000?60000000000007000000000000000000:400000@5003000000000000000000070060300000090>04000000009000000000:0000?0000000000000100800000>000000000020000?000810000000005=0000040@00000300120000000000001000000>00000000000000003000002000000700;00000000000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Medium_Sudoko2()
        {
            string validSudokuInput = "000000000000<0000000000000000000000006000<00000070=0000900400000>00@00000000000000000800700000000059000;010000000000000309000000000=9000000000003000800400;00056009000000000000000001000000000000000000000000<00000002000000000000006000<49003=00=30<09000000000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Medium_Sudoko3()
        {
            string validSudokuInput = "00<04000000000000000?0010070000060000500000000=0000=300000000050000070000000000006>0000000000=0000000?02000000005010>006000000000000000400?000000050:0000=0040000000000000000000000000000;00002=00000>400000000@000000?0000000080080000000;000000009000000000002";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Medium_Sudoko4()
        {
            string validSudokuInput = "0000390000600007000;0800000793000500000093000000900000000000000;00000600000000900;000030000000000000>000000000<000100000:0000000000000000800000001000002>00000000<2600007000>90000000000000000@0000000000000000020000000@0000=0000000000200000000000000000090008";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void Medium_Sudoko5()
        {
            string validSudokuInput = "00020000100000=0000000000000000000=@000<000?00000000000400000000000000000000000900@00000>0?0002000000000000000004;26000=0800000000000000000=000000<0400030000000?90000@006<000050000003009400600000<0000000000000000008000900000000000500000000<0000000000000300";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Medium_Sudoko6()
        {
            string validSudokuInput = "000000000000<0000000000000000000000006000<00000070=0000900400000>00@00000000000000000800700000000059000;010000000000000309000000000=9000000000003000800400;00056009000000000000000001000000000000000000000000<00000002000000000000006000<49003=00=30<09000000000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Medium_Sudoko7()
        {
            string validSudokuInput = "00<04000000000000000?0010070000060000500000000=0000=300000000050000070000000000006>0000000000=0000000?02000000005010>006000000000000000400?000000050:0000=0040000000000000000000000000000;00002=00000>400000000@000000?0000000080080000000;000000009000000000002";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Medium_Sudoko8()
        {
            string validSudokuInput = "0000390000600007000;0800000793000500000093000000900000000000000;00000600000000900;000030000000000000>000000000<000100000:0000000000000000800000001000002>00000000<2600007000>90000000000000000@0000000000000000020000000@0000=0000000000200000000000000000090008";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Medium_Sudoko9()
        {
            string validSudokuInput = "00020000100000=0000000000000000000=@000<000?00000000000400000000000000000000000900@00000>0?0002000000000000000004;26000=0800000000000000000=000000<0400030000000?90000@006<000050000003009400600000<0000000000000000008000900000000000500000000<0000000000000300";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Medium_Sudoko10()
        {
            string validSudokuInput = "00000000<00000000=00000000;@0000300000005000>0000000000=00000000000=080000000000040@00000000=07000000060000000000000;05000070:000540030090@0000?00000:000;0000000<0000000000000000000000000000000000000080000=00000080000000000>0000024;000000000:00050000032000";

            SudokuBoard board = new SudokuBoard(validSudokuInput);
            SolverController solver = new SolverController(board);

            bool result = solver.Solve();

            Assert.IsTrue(result);
        }


    }
}
