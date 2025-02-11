using AlonSudoku.Core.SudokuGame;
using AlonSudoku.Input;
using System.Diagnostics;

namespace AlonSudoku
{
    /// <summary>
    /// Manages the overall Sudoku game flow.
    /// Handles user input, game execution, and displays results.
    /// </summary>
    public static class MainController
    {
        /// <summary>
        /// Starts the Sudoku game loop.
        /// </summary>
        public static void Run()
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

                IInput inputSource = GetInputSource(choice);
                if (inputSource == null) continue;

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
        /// Handles user selection of input method (console or file).
        /// </summary>
        /// <param name="choice">User's selection.</param>
        /// <returns>Returns an IInput implementation.</returns>
        private static IInput GetInputSource(int choice)
        {
            if (choice == 2)
            {
                Console.Write("\n Enter the file path: ");
                string filePath = Console.ReadLine();
                return new FileInputSource(filePath);
            }
            else
            {
                return new ConsoleInputSource();
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
