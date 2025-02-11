using AlonSudoku.Input;
using AlonSudoku.Core.SudokuGame;
using System;
using System.Diagnostics;
using System.Threading;

namespace AlonSudoku
{
    /// <summary>
    /// Main program class to run the Sudoku game.
    /// </summary>
    internal class Program
    {
        public static void Main(string[] args)
        {
            MainController.Run();
        }
    }
}
