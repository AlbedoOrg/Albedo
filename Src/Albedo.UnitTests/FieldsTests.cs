using System;
using Xunit;
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

        [Fact]
        public void SelectNullThrows()
        {
            var sut = new Fields<ClassWithFields>();
            Assert.Throws<ArgumentNullException>(
                () => sut.Select<object>(null));
        }

        [Fact]
        public void SelectNonMemberExpressionThrows()
        {
            var sut = new Fields<ClassWithFields>();
            Assert.Throws<ArgumentException>(
                () => sut.Select(x => x.ToString()));
        }

        [Fact]
        public void SelectPropertiesThrows()
        {
            var sut = new Fields<ClassWithProperties>();
            Assert.Throws<ArgumentException>(
                () => sut.Select(x => x.ReadOnlyText));
        }

        [Fact]
        public void SelectFieldDeclaredOnBaseReturnsCorrectField()
        {
            var sut = new Fields<SubClassWithFields>();
            var expected = typeof(SubClassWithFields).GetField("ReadOnlyText");

            var actual = sut.Select(x => x.ReadOnlyText);

            Assert.Equal(expected, actual);
        }

        private class ClassWithFields
        {
            public readonly string ReadOnlyText = string.Empty;
        }

        private class SubClassWithFields : ClassWithFields
        {
        }

        private class ClassWithProperties
        {
            public string ReadOnlyText { get; private set; }
        }

        [Fact]
        public void SelectStaticFieldReturnsCorrectField()
        {
            var sut = new Fields();

            FieldInfo actual = sut.Select(() => Uri.SchemeDelimiter);

            var expected = typeof(Uri).GetField("SchemeDelimiter");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectStaticNullThrows()
        {
            var sut = new Fields();
            Assert.Throws<ArgumentNullException>(
                () => sut.Select<object>(null));
        }

        [Fact]
        public void SelectStaticNonMemberExpressionThrows()
        {
            var sut = new Fields();
            Assert.Throws<ArgumentException>(
                () => sut.Select(() => new object().ToString()));
        }
    }
}
