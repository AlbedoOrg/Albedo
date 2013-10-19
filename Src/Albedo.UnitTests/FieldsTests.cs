using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    public class FieldsTests
    {
        [Fact]
        public void SelectFieldReturnsCorrectField()
        {
            var sut = new Fields<ClassWithFields>();

            FieldInfo actual = sut.Select(x => x.ReadOnlyText);

            var expected = typeof(ClassWithFields).GetField("ReadOnlyText");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void QueryFieldUsingLinqSyntaxReturnsCorrectField()
        {
            var sut = new Fields<ClassWithFields>();

            FieldInfo actual = from x in sut select x.ReadOnlyText;

            var expected = typeof(ClassWithFields).GetField("ReadOnlyText");
            Assert.Equal(expected, actual);
        }

        private class ClassWithFields
        {
            public readonly string ReadOnlyText = string.Empty;
        }
    }
}
