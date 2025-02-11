namespace AlonSudoku.Exceptions
{
    /// <summary>
    /// Exception thrown when the Sudoku puzzle format is invalid.
    /// </summary>
    public class SudokuInvalidFormatException : Exception
    {
        public SudokuInvalidFormatException(string message)
            : base($"Error: Invalid Sudoku format - {message}") { }
    }
}
