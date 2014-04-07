using System;
using System.Linq.Expressions;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class ConstructorsTest
    {
        [Fact]
        public void SelectReturnsCorrectConstructor()
        {
            var expected = TypeWithCtors.Ctor;
            var actual = Constructors.Select(() => new TypeWithCtors());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectNullThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => Constructors.Select((Expression<Func<object>>)null));
        }

        [Fact]
        public void SelectNonConstructorThrows()
        {
            Assert.Throws<ArgumentException>(
                () => Constructors.Select(() => 1 + 1));
        }
    }
}