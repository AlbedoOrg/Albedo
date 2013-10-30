using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class CompositeReflectionElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            var sut = new CompositeReflectionElement();
            Assert.IsAssignableFrom<IReflectionElement>(sut);
        }

        [Fact]
        public void SutIsSequence()
        {
            var sut = new CompositeReflectionElement();
            Assert.IsAssignableFrom<IEnumerable<IReflectionElement>>(sut);
        }

        [Fact]
        public void SutYieldsInjectedElements()
        {
            var expected = new[]
            {
                new Mock<IReflectionElement>().Object,
                new Mock<IReflectionElement>().Object,
                new Mock<IReflectionElement>().Object
            };
            var sut = new CompositeReflectionElement(expected);
            Assert.True(expected.SequenceEqual(sut));
            Assert.True(
                expected.Cast<object>().SequenceEqual(sut.OfType<object>()));
        }
    }
}