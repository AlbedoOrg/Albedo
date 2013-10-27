using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class ParameterInfoElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new ParameterInfoElement(TypeWithParameter.Parameter);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void ParameterInfoIsCorrect()
        {
            // Fixture setup
            var expected = TypeWithParameter.Parameter;
            var sut = new ParameterInfoElement(expected);
            // Exercise system
            ParameterInfo actual = sut.ParameterInfo;
            // Verify outcome
            Assert.Equal(expected, actual);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullParameterInfoThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new ParameterInfoElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new ParameterInfoElement(TypeWithParameter.Parameter);
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
            var sut = new ParameterInfoElement(TypeWithParameter.Parameter);
            var visitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitParameterInfoElement = e =>
                    e == sut ? expected : new DelegatingReflectionVisitor<int>()
            };

            // Exercise system
            var actual = sut.Accept(visitor);
            // Verify outcome
            Assert.Same(expected, actual);
            // Teardown
        }


        class TypeWithParameter
        {
            public static ParameterInfo Parameter
            {
                get
                {
                    return typeof(TypeWithParameter)
                        .GetMethods()
                        .Single(m => m.Name == "TheMethod")
                        .GetParameters()
                        .First();
                }
            }

            public void TheMethod(int param1)
            {
            }
        }
    }
}
