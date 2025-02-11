using AlonSudoku.Input;
using AlonSudoku.Core.SudokuGame;
using System;
using System.Diagnostics;
using System.Threading;

namespace AlonSudoku
{
    /// <summary>
    /// Manages the overall Sudoku game flow.
    /// Handles user input, game execution, and displays results.
    /// </summary>
    public static class MainController
    {
        private static bool _ctrlCPressed = false; 

        /// <summary>
        /// Starts the Sudoku game loop with error handling and Ctrl+C interception.
        /// </summary>
        public static void Run()
        {
            Console.CancelKeyPress += (sender, args) =>
            {
                args.Cancel = true; 
                _ctrlCPressed = true; 
            };

            while (true)
            {
                try
                {
                    Console.Clear();
                    PrintHeader();

                    if (_ctrlCPressed)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n  Ctrl+C detected! Please choose an option again.");
                        Console.ResetColor();
                        _ctrlCPressed = false; 
                    }

                    Console.WriteLine("  [1]  Enter Sudoku from Console");
                    Console.WriteLine("  [2]  Load Sudoku from File");
                    Console.WriteLine("  [3]  Exit");
                    Console.Write("\n Select an option: ");

                    int choice;
                    while (!TryReadInt(out choice) || (choice < 1 || choice > 3))
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
                catch (Exception ex)
                {
                    HandleException(ex);
                }
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

        /// <summary>
        /// Reads an integer input safely.
        /// Prevents crashes from invalid input (non-numeric characters).
        /// </summary>
        /// <param name="result">Output parameter to store the parsed integer.</param>
        /// <returns>True if parsing was successful, otherwise false.</returns>
        private static bool TryReadInt(out int result)
        {
            string input = Console.ReadLine();
            return int.TryParse(input, out result);
        }

        /// <summary>
        /// Handles exceptions that occur in the program.
        /// Ensures the console does not crash and provides feedback to the user.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        private static void HandleException(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  An unexpected error occurred: {ex.Message}");
            Console.WriteLine("  Returning to the main menu...");
            Console.ResetColor();
        }
    }
}
