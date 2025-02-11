using System;

namespace AlonSudoku.Exceptions
{
    /// <summary>
    /// Exception thrown when the Sudoku input contains an invalid character.
    /// </summary>
    public class InvalidSudokuCharacterException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSudokuCharacterException"/> class with a custom message.
        /// </summary>
        /// <param name="message">Custom error message.</param>
        public InvalidSudokuCharacterException(string message)
            : base(message)
        {
        }
    }
}