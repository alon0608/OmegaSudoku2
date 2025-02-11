using System;

namespace AlonSudoku.Exceptions
{
    /// <summary>
    /// Exception thrown when the Sudoku input length is invalid .
    /// </summary>
    public class InvalidSudokuLengthException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSudokuLengthException"/> class with a custom message.
        /// </summary>
        /// <param name="message">Custom error message.</param>
        public InvalidSudokuLengthException(string message)
            : base(message)
        {
        }
    }
}