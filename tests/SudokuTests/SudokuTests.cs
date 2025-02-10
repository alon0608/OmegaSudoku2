using Xunit;
using SudokuOmega7;

namespace SudokuTests
{
    public class SudokuTests
    {
        [Fact]
        public void test()
        {
            int num=1, num2=2,num3=9;
            int n=BoardStateManager.GetBoxIndex(num, num2, num3);
        }
    }
}
