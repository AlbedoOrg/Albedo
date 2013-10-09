using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class ConstructorInfoElementTest
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new ConstructorInfoElement(this.GetType().GetConstructors().First());
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void ConstructorInfoIsCorrect()
        {
            // Fixture setup
            var expectedCtor = this.GetType().GetConstructors().First();
            var sut = new ConstructorInfoElement(expectedCtor);
            // Exercise system
            var actual = sut.ConstructorInfo;
            // Verify outcome
            Assert.Equal(expectedCtor, actual);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullConstructorInfoThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new ConstructorInfoElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new ConstructorInfoElement(this.GetType().GetConstructors().First());
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
            var observed = new List<ConstructorInfoElement>();
            var dummyVisitor = new DelegatingReflectionVisitor<int>
                { OnVisitConstructorInfoElement = observed.Add };
            var sut = new ConstructorInfoElement(
                typeof(TypeWithConstructorParameters<int, int, int>).GetConstructors().First());
            // Exercise system
            sut.Accept(dummyVisitor);
            // Verify outcome
            Assert.True(new[] { sut }.SequenceEqual(observed));
            // Teardown
        }

        class TypeWithConstructorParameters<TParam1, TParam2, TParam3>
        {
            public TypeWithConstructorParameters(TParam1 param1, TParam2 param2, TParam3 param3)
            {
            }
        }

    }
}
