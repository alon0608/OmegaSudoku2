using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AlonSudoku.Input
{

    public class FileInputSource : IInput
    {
        private readonly string _filePath;

        public FileInputSource(string filePath)
        {
            _filePath = filePath;
        }

        public string GetInput()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException($"File not found: {_filePath}");

            return File.ReadAllText(_filePath).Replace("\n", "").Replace("\r", "").Trim();
        }
    }

}
