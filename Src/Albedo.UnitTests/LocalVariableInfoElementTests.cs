﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class LocalVariableInfoElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new LocalVariableInfoElement(TypeWithLocalVariables.LocalVariable);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void LocalVariableInfoIsCorrect()
        {
            // Fixture setup
            var expected = TypeWithLocalVariables.LocalVariable;
            var sut = new LocalVariableInfoElement(expected);
            // Exercise system
            LocalVariableInfo actual = sut.LocalVariableInfo;
            // Verify outcome
            Assert.Equal(expected, actual);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullLocalVariableInfoThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new LocalVariableInfoElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new LocalVariableInfoElement(TypeWithLocalVariables.LocalVariable);
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
            var sut = new LocalVariableInfoElement(TypeWithLocalVariables.LocalVariable);
            var visitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitLocalVariableInfoElement = e =>
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
            var lvi = TypeWithLocalVariables.LocalVariable;
            var sut = new LocalVariableInfoElement(lvi);
            var other = new LocalVariableInfoElement(lvi);

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
            var sut = new LocalVariableInfoElement(TypeWithLocalVariables.LocalVariable);
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Fact]
        public void SutDoesNotEqualDifferentInstanceOfSameType()
        {
            var sut = new LocalVariableInfoElement(TypeWithLocalVariables.LocalVariable);
            var otherLocalVariable = TypeWithLocalVariables.OtherLocalVariable;
            var other = new LocalVariableInfoElement(otherLocalVariable);

            var actual = sut.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void GetHashCodeReturnsCorrectResult()
        {
            var lvi = TypeWithLocalVariables.LocalVariable;
            var sut = new LocalVariableInfoElement(lvi);

            var actual = sut.GetHashCode();

            var expected = lvi.GetHashCode();
            Assert.Equal(expected, actual);
        }
    }
}
