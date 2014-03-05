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
            var sut = new Constructors();
            var expected = TypeWithCtors.Ctor;

            var actual = sut.Select(() => new TypeWithCtors());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectNullThrows()
        {
            var sut = new Constructors();
            Assert.Throws<ArgumentNullException>(() => sut.Select((Expression<Func<object>>)null));
        }

        [Fact]
        public void SelectNonConstructorThrows()
        {
            var sut = new Constructors();
            Assert.Throws<ArgumentException>(() => sut.Select(() => 1 + 1));
        }

        [Fact]
        public void SelectByQuerySyntaxReturnsCorrectConstructor()
        {
            var sut = new Constructors();
            var expected = TypeWithCtors.Ctor;

            var actual = from x in sut select new TypeWithCtors();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectNullByQuerySyntaxThrows()
        {
            var sut = new Constructors();
            Assert.Throws<ArgumentNullException>(
                () => sut.Select((Expression<Func<object, Constructors>>)null));
        }

        [Fact]
        public void SelectNonConstructorByQuerySyntaxThrows()
        {
            var sut = new Constructors();
            Assert.Throws<ArgumentException>(() => from x in sut select 1 + 1);
        }
    }
}