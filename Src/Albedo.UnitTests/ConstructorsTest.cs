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

            var actual = Constructors.StaticSelect(() => new TypeWithCtors());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectNullThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => Constructors.StaticSelect((Expression<Func<object>>)null));
        }

        [Fact]
        public void SelectNonConstructorThrows()
        {
            Assert.Throws<ArgumentException>(
                () => Constructors.StaticSelect(() => 1 + 1));
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