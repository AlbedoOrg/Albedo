using System;
using System.Collections.Generic;
using System.Linq;
using Albedo.Refraction;
using Moq;
using Xunit;

namespace Albedo.UnitTests.Refraction
{
    public class CompositeReflectionElementRefractionTests
    {
        [Fact]
        public void SutIsReflectionElementRefraction()
        {
            var sut = new CompositeReflectionElementRefraction<object>();
            Assert.IsAssignableFrom<IReflectionElementRefraction<object>>(sut);
        }

        [Fact]
        public void SutIsIteratorOfRefractions()
        {
            var sut = new CompositeReflectionElementRefraction<object>();
            Assert.IsAssignableFrom<IEnumerable<IReflectionElementRefraction<object>>>(sut);
        }

        [Fact]
        public void SutYieldsInjectedArray()
        {
            var expected = new[]
            {
                new Mock<IReflectionElementRefraction<object>>().Object,
                new Mock<IReflectionElementRefraction<object>>().Object,
                new Mock<IReflectionElementRefraction<object>>().Object
            };
            var sut = 
                new CompositeReflectionElementRefraction<object>(expected);

            Assert.True(expected.SequenceEqual(sut));
            Assert.True(
                expected.Cast<object>().SequenceEqual(sut.OfType<object>()));
        }

        [Fact]
        public void ConstructorWithNullArrayThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new CompositeReflectionElementRefraction<object>(null));
        }

        [Fact]
        public void RefractReturnsCorrectResult()
        {
            // Fixture setup
            var source = Enumerable.Range(1, 4).ToArray();
            var expected = Enumerable
                .Range(1, 10)
                .Select(_ => new Mock<IReflectionElement>().Object)
                .ToArray();
            var refractionStubs = Enumerable
                .Range(1, 3)
                .Select(_ => new Mock<IReflectionElementRefraction<int>>())
                .ToArray();
            refractionStubs[0].Setup(r => r.Refract(source)).Returns(expected.Take(4));
            refractionStubs[1].Setup(r => r.Refract(source)).Returns(expected.Skip(4).Take(2));
            refractionStubs[2].Setup(r => r.Refract(source)).Returns(expected.Skip(6).Take(4));

            var sut = new CompositeReflectionElementRefraction<int>(
                refractionStubs.Select(td => td.Object).ToArray());
            // Exercise system
            var actual = sut.Refract(source);
            // Verify outcome
            Assert.Equal(expected, actual);
            // Teardown
        }

        [Fact]
        public void RefractNullThrows()
        {
            var sut = new CompositeReflectionElementRefraction<object>();
            Assert.Throws<ArgumentNullException>(() => sut.Refract(null));
        }
    }
}
