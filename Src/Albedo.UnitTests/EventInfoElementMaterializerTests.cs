using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;

namespace Ploeh.Albedo.UnitTests
{
    public class EventInfoElementMaterializerTests
    {
        [Fact]
        public void SutIsReflectionElementMaterializer()
        {
            var sut = new EventInfoElementMaterializer<object>();
            Assert.IsAssignableFrom<IReflectionElementMaterializer<object>>(sut);
        }
    }
}
