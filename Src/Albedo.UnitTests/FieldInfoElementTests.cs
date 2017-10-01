using System;
using System.Reflection;
using Xunit;

namespace Albedo.UnitTests
{
    public class FieldInfoElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new FieldInfoElement(TypeWithFields.Field);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void FieldInfoIsCorrect()
        {
            // Fixture setup
            var expected = TypeWithFields.Field;
            var sut = new FieldInfoElement(expected);
            // Exercise system
            FieldInfo actual = sut.FieldInfo;
            // Verify outcome
            Assert.Equal(expected, actual);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullFieldInfoThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new FieldInfoElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new FieldInfoElement(TypeWithFields.Field);
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
            var sut = new FieldInfoElement(TypeWithFields.Field);
            var visitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitFieldInfoElement = e =>
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
            var fi = TypeWithFields.Field;
            var sut = new FieldInfoElement(fi);
            var other = new FieldInfoElement(fi);

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
            var sut = new FieldInfoElement(TypeWithFields.Field);
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Fact]
        public void SutDoesNotEqualDifferentInstanceOfSameType()
        {
            var sut = new FieldInfoElement(TypeWithFields.Field);
            var otherField = typeof(InterfaceMapping).GetField("InterfaceType");
            var other = new FieldInfoElement(otherField);

            var actual = sut.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void GetHashCodeReturnsCorrectResult()
        {
            var fi = TypeWithFields.Field;
            var sut = new FieldInfoElement(fi);

            var actual = sut.GetHashCode();

            var expected = fi.GetHashCode();
            Assert.Equal(expected, actual);
        }
    }
}
