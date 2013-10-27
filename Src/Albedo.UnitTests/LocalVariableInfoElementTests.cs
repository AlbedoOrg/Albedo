using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class LocalVariableInfoElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new LocalVariableInfoElement(TypeWithLocalVariable.LocalVariable);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void LocalVariableInfoIsCorrect()
        {
            // Fixture setup
            var expected = TypeWithLocalVariable.LocalVariable;
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
            var sut = new LocalVariableInfoElement(TypeWithLocalVariable.LocalVariable);
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
            var sut = new LocalVariableInfoElement(TypeWithLocalVariable.LocalVariable);
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


        class TypeWithLocalVariable
        {
            public static LocalVariableInfo LocalVariable
            {
                get
                {
                    return typeof (TypeWithLocalVariable)
                        .GetMethod("TheMethod")
                        .GetMethodBody()
                        .LocalVariables[0];
                }
            }

            public void TheMethod()
            {
                // This is required to prevent the compiler from
                // warning and optimising away the local variable.
                var local = 1;
                local = local + 1;
                local = local + 2;
            }
        }
    }
}
