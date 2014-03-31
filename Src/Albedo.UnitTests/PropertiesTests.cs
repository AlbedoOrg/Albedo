using System;
using Xunit;
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

        [Fact]
        public void SelectPropertyDeclaredOnBaseReturnsCorrectProperty()
        {
            var sut = new Properties<SubClassWithProperties>();
            var expected = typeof(SubClassWithProperties).GetProperty("ReadOnlyText");

            var actual = sut.Select(x => x.ReadOnlyText);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectStaticPropertyReturnsCorrectProperty()
        {
            var expected = typeof(ClassWithStaticProperties).GetProperty("StaticProperty");

            var actual = Properties.Select(() => ClassWithStaticProperties.StaticProperty);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PropertiesSelectNullThrows()
        {
            Assert.Throws<ArgumentNullException>(() => Properties.Select<object>(null));
        }

        [Fact]
        public void PropertiesSelectStaticFieldThrows()
        {
            Assert.Throws<ArgumentException>(() => Properties.Select(() => Uri.SchemeDelimiter));
        }

        [Fact]
        public void PropertiesSelectPropertyFromSubClassSelectCorrectProperty()
        {
            var expected = typeof(ClassWithStaticProperties).GetProperty("StaticProperty");

            var actual = Properties.Select(() => SubClassWithStaticProperties.StaticProperty);

            Assert.Equal(expected, actual);
        }

        private class ClassWithProperties
        {
            public string ReadOnlyText { get; private set; }
        }

        private class SubClassWithProperties : ClassWithProperties
        {
        }

        private class ClassWithFields
        {
            public readonly string ReadOnlyText = "";
        }

        private class ClassWithStaticProperties
        {
            public static object StaticProperty { get; set; } 
        }

        private class SubClassWithStaticProperties : ClassWithStaticProperties
        {
        }
    }
}
