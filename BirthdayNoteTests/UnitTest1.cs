using System;
using Xunit;

namespace BirthdayNoteTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(5 > new Random().Next(0, 10));
        }
    }
}
