using System;
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
            var sut = new EventInfoElement(TypeWithEvent.LocalEvent);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void EventInfoIsCorrect()
        {
            // Fixture setup
            var expected = TypeWithEvent.LocalEvent;
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
            var sut = new EventInfoElement(TypeWithEvent.LocalEvent);
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
            var sut = new EventInfoElement(TypeWithEvent.LocalEvent);
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
            var c = TypeWithEvent.LocalEvent;
            var sut = new EventInfoElement(c);
            var other = new EventInfoElement(c);

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
            var sut = new EventInfoElement(TypeWithEvent.LocalEvent);
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Fact]
        public void SutDoesNotEqualDifferentInstanceOfSameType()
        {
            var sut = new EventInfoElement(TypeWithEvent.LocalEvent);
            var otherEvent = typeof(Assembly).GetEvent("ModuleResolve");
            var other = new EventInfoElement(otherEvent);

            var actual = sut.Equals(other);

            Assert.False(actual);
        }

        [Theory]
        [InlineData(typeof(Version))]
        [InlineData(typeof(TheoryAttribute))]
        [InlineData(typeof(EventInfoElement))]
        public void GetHashCodeReturnsCorrectResult(Type t)
        {
            var c = TypeWithEvent.LocalEvent;
            var sut = new EventInfoElement(c);

            var actual = sut.GetHashCode();

            var expected = c.GetHashCode();
            Assert.Equal(expected, actual);
        }


        class TypeWithEvent
        {
            public static EventInfo LocalEvent
            {
                get
                {
                    return typeof (TypeWithEvent).GetEvent("TheEvent");
                }
            }

            public event EventHandler TheEvent
            {
                add { throw new NotImplementedException(); }
                remove { throw new NotImplementedException(); }
            }
        }
    }
}
