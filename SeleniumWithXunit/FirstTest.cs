using System.Threading;
using Xunit;

namespace SeleniumWithXunit
{   
    public class FirstTest
    {
        [Fact]
        public void TestMethod1()
        {
            Thread.Sleep(2000);
        }

        [Fact]
        public void TestMethod2()
        {
            Thread.Sleep(2000);
        }
    }
}
