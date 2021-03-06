﻿using System;
using System.Reflection;
using Xunit;

namespace Albedo.UnitTests
{
    public class MethodInfoElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new MethodInfoElement(TypeWithMethods.Method);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void MethodInfoIsCorrect()
        {
            // Fixture setup
            var expected = TypeWithMethods.Method;
            var sut = new MethodInfoElement(expected);
            // Exercise system
            MethodInfo actual = sut.MethodInfo;
            // Verify outcome
            Assert.Equal(expected, actual);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullMethodInfoThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new MethodInfoElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new MethodInfoElement(TypeWithMethods.Method);
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
            var sut = new MethodInfoElement(TypeWithMethods.Method);
            var visitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitMethodInfoElement = e =>
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
            var mi = TypeWithMethods.Method;
            var sut = new MethodInfoElement(mi);
            var other = new MethodInfoElement(mi);

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
            var sut = new MethodInfoElement(TypeWithMethods.Method);
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Fact]
        public void SutDoesNotEqualDifferentInstanceOfSameType()
        {
            var sut = new MethodInfoElement(TypeWithMethods.Method);
            var otherMethod = this.GetType().GetMethods()[0];
            var other = new MethodInfoElement(otherMethod);

            var actual = sut.Equals(other);

            Assert.False(actual);
        }

        [Theory]
        [InlineData(typeof(Version))]
        [InlineData(typeof(TheoryAttribute))]
        [InlineData(typeof(MethodInfoElement))]
        [InlineData(typeof(TypeWithMethods))]
        public void GetHashCodeReturnsCorrectResult(Type t)
        {
            var mi = t.GetMethod(nameof(GetHashCode));
            var sut = new MethodInfoElement(mi);

            var actual = sut.GetHashCode();

            var expected = mi.GetHashCode();
            Assert.Equal(expected, actual);
        }
    }
}
