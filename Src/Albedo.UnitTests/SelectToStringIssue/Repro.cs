using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests.SelectToStringIssue
{
    public class Repro
    {
        [Fact]
        public void SelectMethodDeclaredOnBaseReturnsCorrectMethod()
        {
            var sut = new Methods<ClassWithMethods>();
            var expected = typeof(ClassWithMethods).GetMethod("ToString");

            var actual = sut.Select(x => x.ToString());

            Assert.Equal(expected, actual);
        }

        private class ClassWithMethods
        {
        }
    }
}
