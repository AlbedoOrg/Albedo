using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class TypeElementTest
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            var type = this.GetType();
            // Exercise system
            var sut = new TypeElement(type);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void TypeIsCorrect()
        {
            // Fixture setup
            var type = this.GetType();
            var sut = new TypeElement(type);
            // Exercise system
            var actual = sut.Type;
            // Verify outcome
            Assert.Equal(type, actual);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullTypeThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new TypeElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new TypeElement(this.GetType());
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Accept((IReflectionVisitor<object>)null));
            // Teardown
        }

        [Fact]
        public void AcceptReturnsTheCorrectVisitorInstance()
        {
            // Fixture setup
            var sut = new TypeElement(this.GetType());

            var expected = new DelegatingReflectionVisitor<int>();
            var visitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitTypeElement = (e, v) => expected
            };
            // Exercise system
            var actual = sut.Accept(visitor);
            // Verify outcome
            Assert.Same(expected, actual);
            // Teardown
        }

        [Fact]
        public void AcceptCallsVisitOnceWithCorrectType()
        {
            // Fixture setup
            var observed = new List<TypeElement>();
            var dummyVisitor = new DelegatingReflectionVisitor<int> { OnTypeElementVisited = observed.Add };
            var sut = new TypeElement(this.GetType());
            // Exercise system
            sut.Accept(dummyVisitor);
            // Verify outcome
            Assert.True(new[] { sut }.SequenceEqual(observed));
            // Teardown
        }
    }
}
