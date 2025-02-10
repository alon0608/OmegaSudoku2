using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{

    public class ConsoleInputSource : IInput
    {
        public string GetInput()
        {
            Console.WriteLine("Enter the Sudoku puzzle as a single line:");
            String input = Console.ReadLine();
            input = input.Replace(".", "0");
            return input;
        }
    }

}
