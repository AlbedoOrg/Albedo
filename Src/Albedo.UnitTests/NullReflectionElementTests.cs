using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using Moq;

namespace Ploeh.Albedo.UnitTests
{
    public class NullReflectionElementTests
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            var sut = new NullReflectionElement();
            Assert.IsAssignableFrom<IReflectionElement>(sut);
        }

        [Fact]
        public void AcceptReturnsCorrectResult()
        {
            var expected = new Mock<IReflectionVisitor<object>>().Object;
            var sut = new NullReflectionElement();

            var actual = sut.Accept(expected);

            Assert.Equal(expected, actual);
        }
    }
}
