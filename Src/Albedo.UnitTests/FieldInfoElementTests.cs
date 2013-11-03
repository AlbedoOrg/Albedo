using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class FieldInfoElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new FieldInfoElement(TypeWithField.Field);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void FieldInfoIsCorrect()
        {
            // Fixture setup
            var expected = TypeWithField.Field;
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
            var sut = new FieldInfoElement(TypeWithField.Field);
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
            var sut = new FieldInfoElement(TypeWithField.Field);
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
            var fi = TypeWithField.Field;
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
            var sut = new FieldInfoElement(TypeWithField.Field);
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Fact]
        public void SutDoesNotEqualDifferentInstanceOfSameType()
        {
            var sut = new FieldInfoElement(TypeWithField.Field);
            var otherField = typeof(InterfaceMapping).GetField("InterfaceType");
            var other = new FieldInfoElement(otherField);

            var actual = sut.Equals(other);

            Assert.False(actual);
        }

        [Theory]
        [InlineData(typeof(Version))]
        [InlineData(typeof(TheoryAttribute))]
        [InlineData(typeof(FieldInfoElement))]
        public void GetHashCodeReturnsCorrectResult(Type t)
        {
            var fi = TypeWithField.Field;
            var sut = new FieldInfoElement(fi);

            var actual = sut.GetHashCode();

            var expected = fi.GetHashCode();
            Assert.Equal(expected, actual);
        }


        class TypeWithField
        {
            public static FieldInfo Field
            {
                get
                {
                    return typeof(TypeWithField).GetFields()[0];
                }
            }

            public int TheField = 0;
        }
    }
}
