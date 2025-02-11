using System;
using System.IO;
using AlonSudoku.Validation;

namespace AlonSudoku.Input
{
    /// <summary>
    /// Handles reading a Sudoku puzzle from a file.
    /// Ensures the file exists and contains valid input before returning it.
    /// </summary>
    public class FileInputSource : IInput
    {
        /// <summary>
        /// Stores the file path of the Sudoku puzzle.
        /// </summary>
        private string _filePath;

        /// <summary>
        /// Initializes a new instance of the FileInputSource class.
        /// </summary>
        /// <param name="filePath">The path to the file containing the Sudoku puzzle.</param>
        public FileInputSource(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Reads the Sudoku puzzle from the file, validates it, and returns it as a string.
        /// If the file is missing or invalid, the user is prompted to enter a new file path.
        /// </summary>
        /// <returns>The validated Sudoku puzzle as a single-line string, or null if the user exits.</returns>
        public string GetInput()
        {
            while (true) // Keep asking until valid input is provided or user exits
            {
                try
                {
                    // Check if the file exists
                    if (!File.Exists(_filePath))
                        throw new FileNotFoundException($"Error: File not found: {_filePath}");

                    // Read file and clean the content (remove newlines and replace '.' with '0')
                    string input = File.ReadAllText(_filePath)
                        .Replace("\n", "")
                        .Replace("\r", "")
                        .Replace(".", "0")
                        .Trim();

                    // Check if the file is empty
                    if (string.IsNullOrWhiteSpace(input))
                        throw new ArgumentException("Error: The file is empty or contains only whitespace.");

                    // Validate the input format
                    SudokuInputValidation.ValidateInput(input);

                    return input; // Return the validated input
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                // Prompt the user for a new file path
                Console.WriteLine("\n Please enter a new file path or type 'exit' to return to the main menu:");
                string newFilePath = Console.ReadLine();

                // Exit if the user types 'exit'
                if (string.Equals(newFilePath, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                // Update the file path for the next iteration
                _filePath = newFilePath;
            }
        }
    }
}
