using AlonSudoku.Validation;
using AlonSudoku.Exceptions;
using System;

namespace AlonSudoku.Input
{
    /// <summary>
    /// Handles user input for Sudoku puzzles from the console.
    /// </summary>
    public class ConsoleInputSource : IInput
    {
        public string GetInput()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the Sudoku puzzle as a single line (use '.' or '0' for empty cells):");
                    string input = Console.ReadLine()?.Replace(".", "0");

                    if (string.IsNullOrWhiteSpace(input))
                        throw new SudokuInvalidFormatException("Input cannot be empty. Please enter a valid Sudoku string.");

                    SudokuInputValidation.ValidateInput(input);
                    return input;
                }
                catch (SudokuInvalidFormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine("Please try again.\n");
            }
        }
    }
}
