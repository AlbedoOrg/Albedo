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
    }
}