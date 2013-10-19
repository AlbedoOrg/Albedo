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

        [Fact]
        public void SelectNullThrows()
        {
            var sut = new Properties<ClassWithProperties>();
            Assert.Throws<ArgumentNullException>(
                () => sut.Select<object>(null));
        }

        [Fact]
        public void SelectNonMemberExpressionThrows()
        {
            var sut = new Properties<ClassWithProperties>();
            Assert.Throws<ArgumentException>(
                () => sut.Select(x => x.ToString()));
        }

        [Fact]
        public void SelectFieldThrows()
        {
            var sut = new Properties<ClassWithFields>();
            Assert.Throws<ArgumentException>(
                () => sut.Select(x => x.ReadOnlyText));
        }

        private class ClassWithProperties
        {
            public string ReadOnlyText { get; private set; }
        }

        private class ClassWithFields
        {
            public readonly string ReadOnlyText = "";
        }
    }
}
