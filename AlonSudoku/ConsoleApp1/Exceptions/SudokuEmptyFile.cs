namespace AlonSudoku.Exceptions
{
    /// <summary>
    /// Exception thrown when the Sudoku puzzle format is invalid.
    /// </summary>
    public class SudokuEmptyFile : Exception
    {
        public SudokuEmptyFile(string message)
            : base($"Error: {message}") { }
    }
}
