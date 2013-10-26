using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class MethodInfoElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new MethodInfoElement(TypeWithMethod.Method);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void MethodInfoIsCorrect()
        {
            // Fixture setup
            var expected = TypeWithMethod.Method;
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
            var sut = new MethodInfoElement(TypeWithMethod.Method);
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
            var sut = new MethodInfoElement(TypeWithMethod.Method);
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


        class TypeWithMethod
        {
            public static MethodInfo Method
            {
                get
                {
                    return typeof(TypeWithMethod).GetMethods()[0];
                }
            }

            public void TheMethod()
            {
            }
        }
    }
}
