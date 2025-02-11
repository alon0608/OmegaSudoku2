using AlonSudoku.Exceptions;
using AlonSudoku.Validation;
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
                    Console.WriteLine("\nEnter the Sudoku puzzle as a single line (use '.' or '0' for empty cells):");
                    string? input = Console.ReadLine();
                    if (input is null) throw new OperationCanceledException();

                    input = input.Replace(".", "0");

                    if (string.IsNullOrWhiteSpace(input))
                        throw new EmptySudokuInputException("Input cannot be empty. Please enter a valid Sudoku string.");

                    SudokuInputValidation.ValidateInput(input);
                    return input;
                }
                catch (OperationCanceledException)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nCTRL+C/CTRL+Z detected!.");
                    Console.WriteLine("\nplease enter a valid sudoku string.");
                    Console.ResetColor();
                }
                catch (EmptySudokuInputException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                }
                catch (InvalidSudokuLengthException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                }
                catch (InvalidSudokuCharacterException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }
    }
}
