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
            while (true)
            {
                Console.Clear();
                PrintHeader();

                Console.WriteLine("  [1]  Enter Sudoku from Console");
                Console.WriteLine("  [2]  Load Sudoku from File");
                Console.WriteLine("  [3]  Exit");
                Console.Write("\n Select an option: ");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 3))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("   Invalid choice. Please enter 1, 2, or 3: ");
                    Console.ResetColor();
                }

                if (choice == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  Exiting program. Goodbye!");
                    Console.ResetColor();
                    break;
                }

                IInput inputSource;
                if (choice == 2)
                {
                    Console.Write("\n Enter the file path: ");
                    string filePath = Console.ReadLine();
                    inputSource = new FileInputSource(filePath);
                }
                else
                {
                    inputSource = new ConsoleInputSource();
                }

                var game = new SudokuGame(inputSource);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n   Solving Sudoku...\n");
                Console.ResetColor();

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                game.Run();

                stopwatch.Stop();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  Execution Time: {stopwatch.ElapsedMilliseconds} ms\n");
                Console.ResetColor();

                Console.WriteLine("  Press any key to return to the menu...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Prints the main menu header.
        /// </summary>
        private static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("  ╔═══════════════════════════════════════╗");
            Console.WriteLine("  ║            Alon Sudoku Solver         ║");
            Console.WriteLine("  ╚═══════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}
