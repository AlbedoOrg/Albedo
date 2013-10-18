using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    public class PropertiesTests
    {
        [Fact]
        public void SelectPropertyReturnsCorrectProperty()
        {
            var sut = new Properties<ClassWithProperties>();

            PropertyInfo actual = sut.Select(x => x.ReadOnlyText);

            var expected = typeof(ClassWithProperties).GetProperty("ReadOnlyText");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QueryPropertyUsingLinqSyntax()
        {
            var sut = new Properties<ClassWithProperties>();

            var actual = from x in sut select x.ReadOnlyText;

            var expected = typeof(ClassWithProperties).GetProperty("ReadOnlyText");
            Assert.Equal(expected, actual);
        }

        private class ClassWithProperties
        {
            public string ReadOnlyText { get; private set; }
        }
    }
}
