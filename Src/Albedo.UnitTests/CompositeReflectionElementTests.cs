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

        [Fact]
        public void AcceptAcceptsVisitorForAllElements()
        {
            // Fixture setup
            var v1 = new Mock<IReflectionVisitor<int>>().Object;
            var v2 = new Mock<IReflectionVisitor<int>>().Object;
            var v3 = new Mock<IReflectionVisitor<int>>().Object;
            var v4 = new Mock<IReflectionVisitor<int>>().Object;
            var e1 = new Mock<IReflectionElement>();
            var e2 = new Mock<IReflectionElement>();
            var e3 = new Mock<IReflectionElement>();
            e1.Setup(x => x.Accept(v1)).Returns(v2);
            e2.Setup(x => x.Accept(v2)).Returns(v3);
            e3.Setup(x => x.Accept(v3)).Returns(v4);

            var sut = new CompositeReflectionElement(
                e1.Object,
                e2.Object,
                e3.Object);
            
            // Exercise system
            var actual = sut.Accept(v1);
            
            // Verify outcome
            var expected = v4;
            Assert.Equal(expected, actual);
            
            // Teardown
        }
    }
}