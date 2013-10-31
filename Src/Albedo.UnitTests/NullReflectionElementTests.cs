using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using Moq;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class NullReflectionElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            var sut = new NullReflectionElement();
            Assert.IsAssignableFrom<IReflectionElement>(sut);
        }

        [Fact]
        public void AcceptReturnsCorrectResult()
        {
            var expected = new Mock<IReflectionVisitor<object>>().Object;
            var sut = new NullReflectionElement();

            var actual = sut.Accept(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SutEqualsAnotherInstanceOfItsType()
        {
            var sut = new NullReflectionElement();
            var other = new NullReflectionElement();

            var actual = sut.Equals(other);

            Assert.True(actual);
        }

        [Fact]
        public void SutDoesNotEqualNull()
        {
            var sut = new NullReflectionElement();
            var actual = sut.Equals(null);
            Assert.False(actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("ploeh")]
        [InlineData(1)]
        [InlineData(typeof(Version))]
        [InlineData(DayOfWeek.Wednesday)]
        public void SutDoesNotEqualAnonymousObject(object other)
        {
            var sut = new NullReflectionElement();
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Fact]
        public void SutHasSameHashCodeAsAnotherInstanceOfItsType()
        {
            var sut = new NullReflectionElement();

            var actual = sut.GetHashCode();

            var other = new NullReflectionElement();
            var expected = other.GetHashCode();
            Assert.Equal(expected, actual);
        }
    }
}
