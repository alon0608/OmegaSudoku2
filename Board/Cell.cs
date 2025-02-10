﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    /// <summary>
    /// This class is a single Sudoku cell.
    /// It knows its row, column, a value (1-9 or 0 if empty), 
    /// and whether it's locked from changes.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// The row index (0-based).
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// The column index (0-based).
        /// </summary>
        public int Col { get; }

        /// <summary>
        /// The current value (0 means empty).
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// True if it's read-only (locked), false otherwise.
        /// </summary>
        public bool IsReadOnly { get; }

        /// <summary>
        /// Create a cell with row, col, value, and read-only status.
        /// </summary>
        public Cell(int row, int col, int value, bool isReadOnly)
        {
            Row = row;
            Col = col;
            Value = value;
            IsReadOnly = isReadOnly;
        }

        /// <summary>
        /// Returns '.' if empty, or the digit if not.
        /// </summary>
        public char GetCharValue()
        {
            return Value == 0 ? '.' : ConvertToChar(Value);
        }

        /// <summary>
        /// Converts a number to a single char ('1' to '9').
        /// </summary>
        public char ConvertToChar(int value)
        {
            return (char)('0' + value);
        }
    }
}
