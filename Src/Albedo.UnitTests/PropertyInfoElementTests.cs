using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class PropertyInfoElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new PropertyInfoElement(TypeWithProperty.Property);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void PropertyInfoIsCorrect()
        {
            // Fixture setup
            var expected = TypeWithProperty.Property;
            var sut = new PropertyInfoElement(expected);
            // Exercise system
            PropertyInfo actual = sut.PropertyInfo;
            // Verify outcome
            Assert.Equal(expected, actual);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullPropertyInfoThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new PropertyInfoElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new PropertyInfoElement(TypeWithProperty.Property);
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
            var sut = new PropertyInfoElement(TypeWithProperty.Property);
            var visitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitPropertyInfoElement = e =>
                    e == sut ? expected : new DelegatingReflectionVisitor<int>()
            };

            // Exercise system
            var actual = sut.Accept(visitor);
            // Verify outcome
            Assert.Same(expected, actual);
            // Teardown
        }


        class TypeWithProperty
        {
            public static PropertyInfo Property
            {
                get
                {
                    return typeof (TypeWithProperty).GetProperty("TheProperty");
                }
            }

            public int TheProperty { get; set; }
        }
    }
}
