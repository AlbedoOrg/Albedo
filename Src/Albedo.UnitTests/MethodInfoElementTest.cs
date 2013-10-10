using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class MethodInfoElementTest
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new MethodInfoElement(typeof(TypeWithMethod).GetMethods().First());
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void MethodInfoIsCorrect()
        {
            // Fixture setup
            var expectedMethod = typeof(TypeWithMethod).GetMethods().First();
            var sut = new MethodInfoElement(expectedMethod);
            // Exercise system
            var actual = sut.MethodInfo;
            // Verify outcome
            Assert.Equal(expectedMethod, actual);
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
            var sut = new MethodInfoElement(typeof(TypeWithMethod).GetMethods().First());
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Accept((IReflectionVisitor<object>)null));
            // Teardown
        }

        [Fact]
        public void AcceptCallsVisitOnceWithCorrectType()
        {
            // Fixture setup
            var observed = new List<MethodInfoElement>();
            var dummyVisitor = new DelegatingReflectionVisitor<int> { OnVisitMethodInfoElement = observed.Add };
            var sut = new MethodInfoElement(typeof(TypeWithMethod).GetMethods().First());
            // Exercise system
            sut.Accept(dummyVisitor);
            // Verify outcome
            Assert.True(new[] { sut }.SequenceEqual(observed));
            // Teardown
        }

        public class TypeWithMethod
        {
            public void Method()
            {
            }
        }

        public class TypeWithMethodAndParameters<TParam1, TParam2, TParam3>
        {
            public void Method(TParam1 param1, TParam2 param2, TParam3 param3)
            {
            }
        }

    }
}
