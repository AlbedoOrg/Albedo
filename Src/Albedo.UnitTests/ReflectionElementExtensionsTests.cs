using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Moq;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class ReflectionElementExtensionsTests
    {
        [Fact]
        public void AcceptThrowsOnNullElements()
        {
            // Fixture setup
            IEnumerable<IReflectionElement> elements = null;
            // Exercise system
            var e = Assert.Throws<ArgumentNullException>(() =>
                elements.Accept(new DelegatingReflectionVisitor<object>()));

            Assert.Equal("elements", e.ParamName);
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
            
            var elements = new[] { e1.Object, e2.Object, e3.Object };

            // Exercise system
            var actual = elements.Accept(v1);

            // Verify outcome
            var expected = v4;
            Assert.Equal(expected, actual);

            // Teardown
        }
    }
}
