using System;

namespace AlonSudoku.Exceptions
{
    /// <summary>
    /// Exception thrown when the input is empty or contains only whitespace.
    /// </summary>
    public class EmptySudokuInputException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptySudokuInputException"/> class with a custom message.
        /// </summary>
        /// <param name="message">Custom error message.</param>
        public EmptySudokuInputException(string message)
            : base(message)
        {
        }
    }
}