using Xunit;

namespace Lingva.BusinessLayer.UnitTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("one arg", "second arg")]
        [InlineData("one arg 2", "second arg 2")]
        public void Test2(string arg1, string arg2)
        {
        }

        [Fact]
        public void Test1()
        {
        }
    }
}