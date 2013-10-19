﻿using System;
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

        private class ClassWithFields
        {
            public readonly string ReadOnlyText = string.Empty;
        }

        private class ClassWithProperties
        {
            public string ReadOnlyText { get; private set; }
        }
    }
}
