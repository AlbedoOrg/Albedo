using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

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
