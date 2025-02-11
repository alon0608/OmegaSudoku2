using AlonSudoku.Exceptions;
using AlonSudoku.Input;
using AlonSudoku.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlonSudokuTests.ValidationTest
{
    [TestClass]
    public sealed class ValidationTests
    {
        [TestMethod]
        public void EmptyString_ShouldPrintErrorMessage()
        {
            string emptyBoard = "";
            Assert.ThrowsException<EmptySudokuInputException>(() => SudokuInputValidation.ValidateInput(emptyBoard));
        }

        [TestMethod]
        public void NonSquareBoard_InvalidSudokuLengthException()
        {
            string invalidBoard = new string('0', 50);
            Assert.ThrowsException<InvalidSudokuLengthException>(() => SudokuInputValidation.ValidateInput(invalidBoard));
        }

        [TestMethod]
        public void TooLargeBoard_InvalidSudokuLengthException()
        {
            string tooLargeBoard = new string('0', 26 * 26);
            Assert.ThrowsException<InvalidSudokuLengthException>(() => SudokuInputValidation.ValidateInput(tooLargeBoard));
        }

        [TestMethod]
        public void InvalidCharacter_ShouldThrowInvalidSudokuCharacterException()
        {
            string str = "2";
            Assert.ThrowsException<InvalidSudokuCharacterException>(() => SudokuInputValidation.ValidateInput(str));
        }




    }
}
