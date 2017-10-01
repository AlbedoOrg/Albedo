using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace Albedo.UnitTests
{
    public class CompositeReflectionElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            var sut = new CompositeReflectionElement();
            Assert.IsAssignableFrom<IReflectionElement>(sut);
        }

        [Fact]
        public void SutIsSequence()
        {
            var sut = new CompositeReflectionElement();
            Assert.IsAssignableFrom<IEnumerable<IReflectionElement>>(sut);
        }

        [Fact]
        public void SutYieldsInjectedElements()
        {
            var expected = new[]
            {
                new Mock<IReflectionElement>().Object,
                new Mock<IReflectionElement>().Object,
                new Mock<IReflectionElement>().Object
            };
            var sut = new CompositeReflectionElement(expected);
            Assert.True(expected.SequenceEqual(sut));
            Assert.True(
                expected.Cast<object>().SequenceEqual(sut.OfType<object>()));
        }

        [Fact]
        public void AcceptAcceptsVisitorForAllElements()
        {
            // Fixture setup
            var v1 = new Mock<IReflectionVisitor<int>>().Object;
            var v2 = new Mock<IReflectionVisitor<int>>().Object;
            var v3 = new Mock<IReflectionVisitor<int>>().Object;
            var v4 = new Mock<IReflectionVisitor<int>>().Object;
            var e1 = new Mock<IReflectionElement>();
            var e2 = new Mock<IReflectionElement>();
            var e3 = new Mock<IReflectionElement>();
            e1.Setup(x => x.Accept(v1)).Returns(v2);
            e2.Setup(x => x.Accept(v2)).Returns(v3);
            e3.Setup(x => x.Accept(v3)).Returns(v4);

            var sut = new CompositeReflectionElement(
                e1.Object,
                e2.Object,
                e3.Object);
            
            // Exercise system
            var actual = sut.Accept(v1);
            
            // Verify outcome
            var expected = v4;
            Assert.Equal(expected, actual);
            
            // Teardown
        }

        [Fact]
        public void SutEqualsOtherIdenticalInstance()
        {
            var e1 = new Mock<IReflectionElement>().Object;
            var e2 = new Mock<IReflectionElement>().Object;
            var e3 = new Mock<IReflectionElement>().Object;
            var sut = new CompositeReflectionElement(e1, e2, e3);
            var other = new CompositeReflectionElement(e1, e2, e3);

            var actual = sut.Equals(other);

            Assert.True(actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("bar")]
        [InlineData(1)]
        [InlineData(typeof(Version))]
        [InlineData(UriPartial.Query)]
        public void SutDoesNotEqualAnonymousObject(object other)
        {
            var e1 = new Mock<IReflectionElement>().Object;
            var e2 = new Mock<IReflectionElement>().Object;
            var e3 = new Mock<IReflectionElement>().Object;
            var sut = new CompositeReflectionElement(e1, e2, e3);

            var actual = sut.Equals(other);
            
            Assert.False(actual);
        }

        [Fact]
        public void SutDoesNotEqualDifferentInstanceOfSameType()
        {
            // Fixture setup
            var e1 = new Mock<IReflectionElement>().Object;
            var e2 = new Mock<IReflectionElement>().Object;
            var e3 = new Mock<IReflectionElement>().Object;
            var sut = new CompositeReflectionElement(e1, e2, e3);

            var e4 = new Mock<IReflectionElement>().Object;
            var e5 = new Mock<IReflectionElement>().Object;
            var e6 = new Mock<IReflectionElement>().Object;
            var other = new CompositeReflectionElement(e4, e5, e6);

            // Exercise system
            var actual = sut.Equals(other);

            // Verify outcome
            Assert.False(actual);

            // Teardown
        }

        [Fact]
        public void GetHashCodeReturnsCorrectResult()
        {
            var e1 = new Mock<IReflectionElement>().Object;
            var e2 = new Mock<IReflectionElement>().Object;
            var e3 = new Mock<IReflectionElement>().Object;
            var sut = new CompositeReflectionElement(e1, e2, e3);

            var actual = sut.GetHashCode();

            var expected = (from element in sut select element.GetHashCode())
                .Aggregate((x, y) => x ^ y);
            Assert.Equal(expected, actual);
        }
    }
}