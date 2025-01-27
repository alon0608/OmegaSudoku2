using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SudokuOmega7
{
    public class SudokuGame
    {
        private readonly SudokuBoard _board;
        private readonly SudokuSolver _solver;
        private readonly IValidation _validator;

        public SudokuGame(IInput inputSource, IValidation validator)
        {
            _validator = validator;
            string input = inputSource.GetInput();
            _board = new SudokuBoard(input);
            _solver = new SudokuSolver(_board);
        }

        public void Run()
        {
            Console.WriteLine("first board:");
            _board.PrintBoard();

            if (!_validator.IsValid(_board))
            {
                Console.WriteLine("invalid board.");
                return;
            }

            if (_solver.Solve())
            {
                Console.WriteLine("solution:");
                _board.PrintBoard();
            }
            else
            {
                Console.WriteLine("no solution for this board.");
            }
        }
    }
}

