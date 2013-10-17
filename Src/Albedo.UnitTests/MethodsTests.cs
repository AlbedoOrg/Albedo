using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    public class MethodsTests
    {
        [Fact]
        public void SelectParameterLessReturnsCorrectMethod()
        {
            var sut = new Methods<ClassWithMethods>();
            
            MethodInfo actual = sut.Select(x => x.OmitParameters());

            var expected = typeof(ClassWithMethods).GetMethod("OmitParameters");
            Assert.Equal(expected, actual);
        }

        private class ClassWithMethods
        {
            public void OmitParameters()
            {
            }
        }
    }
}
