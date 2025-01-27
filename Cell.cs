using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    public class Cell
    {
        public int Row { get; }
        public int Col { get; }
        public int Value { get; set; }
        public bool IsReadOnly { get; }

        public Cell(int row, int col, int value, bool isReadOnly)
        {
            Row = row;
            Col = col;
            Value = value;
            IsReadOnly = isReadOnly;
        }

    }
}
