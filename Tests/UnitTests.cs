using NUnit.Framework;
using static Bloggr.Tests.Playground;

namespace Tests
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void MyFirstTest()
        {
            Assert.That(true, Is.False);
        }

        [TestCase(1, 2)]
        [TestCase(4, 200)]
        [TestCase(434, 24)]
        [TestCase(5, 0)]
        public void Add_Always_Returns_ExpectedValue(int lhs, int rhs)
        {
            var systemUnderTest = new Calculator();
            Assert.That(systemUnderTest.Add(lhs, rhs), Is.EqualTo(lhs + rhs));
        }
    }
}