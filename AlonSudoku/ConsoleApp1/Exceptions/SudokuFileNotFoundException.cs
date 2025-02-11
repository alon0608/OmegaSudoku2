namespace AlonSudoku.Exceptions
{
    /// <summary>
    /// Exception thrown when the specified Sudoku file is not found.
    /// </summary>
    public class SudokuFileNotFoundException : Exception
    {
        public SudokuFileNotFoundException(string filePath)
            : base($"Error: Sudoku file not found: {filePath}") { }
    }
}
