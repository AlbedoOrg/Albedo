using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class AssemblyElementTest
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new AssemblyElement(this.GetType().Assembly);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void AssemblyIsCorrect()
        {
            // Fixture setup
            var expectedAssembly = this.GetType().Assembly;
            var sut = new AssemblyElement(expectedAssembly);
            // Exercise system
            Assembly actualAssembly = sut.Assembly;
            // Verify outcome
            Assert.Equal(expectedAssembly, actualAssembly);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullAssemblyThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new AssemblyElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new AssemblyElement(this.GetType().Assembly);
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
            var sut = new AssemblyElement(this.GetType().Assembly);
            var visitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitAssemblyElement = e =>
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
            var a = this.GetType().Assembly;
            var sut = new AssemblyElement(a);
            var other = new AssemblyElement(a);

            var actual = sut.Equals(other);

            Assert.True(actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("fnaah")]
        [InlineData(1)]
        [InlineData(typeof(Version))]
        [InlineData(UriPartial.Query)]
        public void SutDoesNotEqualAnonymousObject(object other)
        {
            var sut = new AssemblyElement(this.GetType().Assembly);
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Fact]
        public void SutDoesNotEqualDifferentInstanceOfSameType()
        {
            var sut = new AssemblyElement(this.GetType().Assembly);
            var other = new AssemblyElement(typeof(Version).Assembly);

            var actual = sut.Equals(other);

            Assert.False(actual);
        }

        [Theory]
        [InlineData(typeof(Assembly))]
        [InlineData(typeof(AssemblyElement))]
        [InlineData(typeof(TheoryAttribute))]
        public void GetHashCodeReturnsCorrectResult(Type containedType)
        {
            var a = containedType.Assembly;
            var sut = new AssemblyElement(a);

            var actual = sut.GetHashCode();

            var expected = a.GetHashCode();
            Assert.Equal(expected, actual);
        }
    }
}
