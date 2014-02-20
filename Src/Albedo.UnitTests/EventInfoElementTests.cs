﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class EventInfoElementTests
    {
        [Fact]
        public void SutIsReflectionElement()  
        {
            // Fixture setup
            // Exercise system
            var sut = new EventInfoElement(TypeWithEvents.LocalEvent);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void EventInfoIsCorrect()
        {
            // Fixture setup
            var expected = TypeWithEvents.LocalEvent;
            var sut = new EventInfoElement(expected);
            // Exercise system
            EventInfo actual = sut.EventInfo;
            // Verify outcome
            Assert.Equal(expected, actual);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullEventInfoThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new EventInfoElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new EventInfoElement(TypeWithEvents.LocalEvent);
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
            var sut = new EventInfoElement(TypeWithEvents.LocalEvent);
            var visitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitEventInfoElement = e =>
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
            var ei = TypeWithEvents.LocalEvent;
            var sut = new EventInfoElement(ei);
            var other = new EventInfoElement(ei);

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
            var sut = new EventInfoElement(TypeWithEvents.LocalEvent);
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Fact]
        public void SutDoesNotEqualDifferentInstanceOfSameType()
        {
            var sut = new EventInfoElement(TypeWithEvents.LocalEvent);
            var otherEvent = typeof(Assembly).GetEvent("ModuleResolve");
            var other = new EventInfoElement(otherEvent);

            var actual = sut.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void GetHashCodeReturnsCorrectResult()
        {
            var ei = TypeWithEvents.LocalEvent;
            var sut = new EventInfoElement(ei);

            var actual = sut.GetHashCode();

            var expected = ei.GetHashCode();
            Assert.Equal(expected, actual);
        }
    }
}
