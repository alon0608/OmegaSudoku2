using AlonSudoku.Validation;
using System;

namespace AlonSudoku.Input
{
    public class ConsoleInputSource : IInput
    {
        public string GetInput()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the Sudoku puzzle as a single line:");
                    string input = Console.ReadLine()?.Replace(".", "0");

                    if (string.IsNullOrWhiteSpace(input))
                        throw new ArgumentException("Input cannot be empty.");

                    SudokuInputValidation.ValidateInput(input);

                    return input;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
