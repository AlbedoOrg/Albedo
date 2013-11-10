using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class ReflectionVisitorTests
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new ReflectionVisitor<int>();
            Assert.IsAssignableFrom<IReflectionVisitor<int>>(sut);
        }
    }
}
