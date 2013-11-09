using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using Moq;

namespace Ploeh.Albedo.Refraction.UnitTests
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
    }
}
