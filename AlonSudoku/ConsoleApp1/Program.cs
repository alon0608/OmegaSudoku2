using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuOmega7
{
    //main class
    internal class Program
    {
        public static void Main(string[] args)
        {
            IInput inputSource;

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1: Enter Sudoku from Console");
                Console.WriteLine("2: Load Sudoku from File");
                Console.WriteLine("3: Exit");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 3))
                {
                    Console.WriteLine("Invalid choice. Please enter 1, 2, or 3:");
                }

                if (choice == 3)
                {
                    Console.WriteLine("Exiting program. Goodbye!");
                    break;
                }

                if (choice == 2)
                {
                    Console.Write("Enter the file path: ");
                    string filePath = Console.ReadLine();
                    inputSource = new FileInputSource(filePath);
                }
                else
                {
                    inputSource = new ConsoleInputSource();
                }

                var game = new SudokuGame(inputSource);

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                game.Run();

                stopwatch.Stop();
                Console.WriteLine($"\nExecution Time: {stopwatch.ElapsedMilliseconds} ms\n");
            }
        }
    }
}
