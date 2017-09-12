using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class ConstructorInfoElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new ConstructorInfoElement(TypeWithCtors.Ctor);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void ConstructorInfoIsCorrect()
        {
            // Fixture setup
            var expected = TypeWithCtors.Ctor;
            var sut = new ConstructorInfoElement(expected);
            // Exercise system
            ConstructorInfo actual = sut.ConstructorInfo;
            // Verify outcome
            Assert.Equal(expected, actual);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullConstructorInfoThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new ConstructorInfoElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new ConstructorInfoElement(TypeWithCtors.Ctor);
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Accept((IReflectionVisitor<object>)null));
            // Teardown
        }

        [Fact]
        public void AcceptCallsTheCorrectVisitorMethodAndReturnsTheCorrectInstance()
        {
            // Fixture setup
            var expected = new DelegatingReflectionVisitor<int>();
            var sut = new ConstructorInfoElement(TypeWithCtors.Ctor);
            var visitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitConstructorInfoElement = e =>
                    e == sut ? expected : new DelegatingReflectionVisitor<int>()
            };

            // Exercise system
            var actual = sut.Accept(visitor);
            // Verify outcome
            Assert.Same(expected, actual);
            // Teardown
        }

        [Fact]
        public void SutEqualsOtherIdenticalInstance()
        {
            var ci = TypeWithCtors.Ctor;
            var sut = new ConstructorInfoElement(ci);
            var other = new ConstructorInfoElement(ci);

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
            var sut = new ConstructorInfoElement(TypeWithCtors.Ctor);
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Fact]
        public void SutDoesNotEqualDifferentInstanceOfSameType()
        {
            var sut = new ConstructorInfoElement(TypeWithCtors.Ctor);
            var otherCtor = this.GetType().GetConstructor(Type.EmptyTypes);
            var other = new ConstructorInfoElement(otherCtor);

            var actual = sut.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void GetHashCodeReturnsCorrectResult()
        {
            var ci = TypeWithCtors.Ctor;
            var sut = new ConstructorInfoElement(ci);

            var actual = sut.GetHashCode();

            var expected = ci.GetHashCode();
            Assert.Equal(expected, actual);
        }
    }
}
