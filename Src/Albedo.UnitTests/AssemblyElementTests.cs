using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class AssemblyElementTest
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new AssemblyElement(this.GetType().Assembly);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void AssemblyIsCorrect()
        {
            // Fixture setup
            var expectedAssembly = this.GetType().Assembly;
            var sut = new AssemblyElement(expectedAssembly);
            // Exercise system
            Assembly actualAssembly = sut.Assembly;
            // Verify outcome
            Assert.Equal(expectedAssembly, actualAssembly);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullAssemblyThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new AssemblyElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new AssemblyElement(this.GetType().Assembly);
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
            var sut = new AssemblyElement(this.GetType().Assembly);
            var visitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitAssemblyElement = e =>
                    e == sut ? expected : new DelegatingReflectionVisitor<int>()
            };

            // Exercise system
            var actual = sut.Accept(visitor);
            // Verify outcome
            Assert.Same(expected, actual);
            // Teardown
        }
    }
}
