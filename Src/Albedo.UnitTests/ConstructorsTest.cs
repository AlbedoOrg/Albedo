using System;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class ConstructorsTest
    {
        [Fact]
        public void SelectReturnsCorrectConstructor()
        {
            var sut = new Constructors();
            var expected = TypeWithCtors.Ctor;

            var actual = sut.Select(() => new TypeWithCtors());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectNullThrows()
        {
            var sut = new Constructors();
            Assert.Throws<ArgumentNullException>(() => sut.Select<object>(null));
        }

        [Fact]
        public void SelectNonConstructorThrows()
        {
            var sut = new Constructors();
            Assert.Throws<ArgumentException>(() => sut.Select(() => 1 + 1));
        }
    }
}