using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;

namespace Ploeh.Albedo.UnitTests
{
    public class ConstructorInfoElementMaterializerTests
    {
        [Fact]
        public void SutIsReflectionElementMaterializer()
        {
            var sut = new ConstructorInfoElementMaterializer<object>();
            Assert.IsAssignableFrom<IReflectionElementMaterializer<object>>(sut);
        }
    }
}
