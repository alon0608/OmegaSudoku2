using AlonSudoku.Validation;
using System;

namespace AlonSudoku.Input
{
    /// <summary>
    /// Handles user input for Sudoku puzzles from the console.
    /// Ensures that the input is correctly formatted before returning it.
    /// </summary>
    public class ConsoleInputSource : IInput
    {
        /// <summary>
        /// Continuously prompts the user to enter a Sudoku puzzle.
        /// The input is validated before being returned.
        /// </summary>
        /// <returns>A validated Sudoku string where '.' is replaced with '0'.</returns>
        public string GetInput()
        {
            while (true) // Keep asking until valid input is provided
            {
                try
                {
                    Console.WriteLine("Enter the Sudoku puzzle as a single line (use '.' for empty cells):");
                    string input = Console.ReadLine()?.Replace(".", "0"); // Convert '.' to '0' for processing

                    if (string.IsNullOrWhiteSpace(input))
                        throw new ArgumentException("Input cannot be empty. Please enter a valid Sudoku string.");

                    // Validate the input format
                    SudokuInputValidation.ValidateInput(input);

                    return input; // Return the validated input
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error: {ex.Message}");
                    Console.WriteLine("Please try again.\n");
                }
            }
        }
    }
}
