using System;
using System.IO;
using AlonSudoku.Validation;
using AlonSudoku.Exceptions;

namespace AlonSudoku.Input
{
    /// <summary>
    /// Handles reading a Sudoku puzzle from a file.
    /// </summary>
    public class FileInputSource : IInput
    {
        private string _filePath;

        public FileInputSource(string filePath)
        {
            _filePath = filePath;
        }

        public string GetInput()
        {
            while (true)
            {
                try
                {
                    if (!File.Exists(_filePath))
                        throw new SudokuFileNotFoundException(_filePath);

                    string input = File.ReadAllText(_filePath)
                        .Replace("\n", "")
                        .Replace("\r", "")
                        .Replace(".", "0")
                        .Trim();

                    if (string.IsNullOrWhiteSpace(input))
                        throw new SudokuEmptyFile("The file is empty or contains only whitespace!.");

                    SudokuInputValidation.ValidateInput(input);
                    return input;
                }
                catch (SudokuFileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (SudokuEmptyFile ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                Console.WriteLine("\n Please enter a new file path or type 'exit' to return to the main menu:");
                string newFilePath = Console.ReadLine();

                if (string.Equals(newFilePath, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n  Exiting program. Goodbye!");
                    Console.ResetColor();
                    Thread.Sleep(1500);
                    return null;
                }

                _filePath = newFilePath;
            }
        }
    }
}
