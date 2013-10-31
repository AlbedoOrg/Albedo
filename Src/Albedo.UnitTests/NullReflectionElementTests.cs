using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;

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
    }
}
