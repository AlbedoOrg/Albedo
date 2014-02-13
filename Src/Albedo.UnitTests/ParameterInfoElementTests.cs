using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

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

        [Fact]
        public void SutEqualsOtherIdenticalInstance()
        {
            var par = TypeWithParameter.Parameter;
            var sut = new ParameterInfoElement(par);
            var other = new ParameterInfoElement(par);

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
            var sut = new ParameterInfoElement(TypeWithParameter.Parameter);
            var actual = sut.Equals(other);
            Assert.False(actual);
        }

        [Fact]
        public void SutDoesNotEqualDifferentInstanceOfSameType()
        {
            var sut = new ParameterInfoElement(TypeWithParameter.Parameter);
            var otherParameter = TypeWithParameter.OtherParameter;
            var other = new ParameterInfoElement(otherParameter);

            var actual = sut.Equals(other);

            Assert.False(actual);
        }

        [Fact]
        public void GetHashCodeReturnsCorrectResult()
        {
            var par = TypeWithParameter.Parameter;
            var sut = new ParameterInfoElement(par);

            var actual = sut.GetHashCode();

            var expected = par.GetHashCode();
            Assert.Equal(expected, actual);
        }
    }
}
