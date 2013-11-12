using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Extensions;
using Ploeh.Albedo.UnitTests.Samples.SemanticComparison;

namespace Ploeh.Albedo.UnitTests
{
    public class Scenario
    {
        [Fact]
        public void SelectParameterLessMethodUsingInstanceSyntax()
        {
            MethodInfo mi = new Methods<Version>().Select(v => v.ToString());
            Assert.Equal("ToString", mi.Name);
        }

        [Fact]
        public void SelectParameterLessMethodUsingLinqSyntax()
        {
            MethodInfo mi = from v in new Methods<Version>()
                            select v.ToString();
            Assert.Equal("ToString", mi.Name);
        }

        [Fact]
        public void SelectPropertyUsingInstanceSyntax()
        {
            PropertyInfo pi = new Properties<Version>().Select(v => v.Major);
            Assert.Equal("Major", pi.Name);
        }

        [Fact]
        public void SelectPropertyUsingLinqSyntax()
        {
            PropertyInfo pi = from v in new Properties<Version>()
                              select v.Major;
            Assert.Equal("Major", pi.Name);
        }

        [Fact]
        public void SelectFieldUsingInstanceSyntax()
        {
            FieldInfo fi = new Fields<ClassWithFields>().Select(v => v.Text);
            Assert.Equal("Text", fi.Name);
        }

        [Fact]
        public void SelectFieldUsingLinqSyntax()
        {
            FieldInfo fi = from v in new Fields<ClassWithFields>()
                           select v.Text;
            Assert.Equal("Text", fi.Name);
        }

        private class ClassWithFields
        {
            public readonly string Text = string.Empty;
        }

        [Theory]
        [InlineData("Major", "major", true)]
        [InlineData("Major", "minor", false)]
        [InlineData("Minor", "major", false)]
        [InlineData("Minor", "minor", true)]
        public void MatchContructorArgumentAgainstReadOnlyProperty(
            string propertyName, string parameterName, bool expected)
        {
            var prop = new PropertyInfoElement(
                typeof(Version).GetProperty(propertyName));
            var param = new ParameterInfoElement(
                typeof(Version)
                    .GetConstructor(new[] { typeof(int), typeof(int) })
                    .GetParameters()
                    .Where(p => p.Name == parameterName)
                    .Single());

            var actual = new SemanticElementComparer().Equals(prop, param);

            Assert.Equal(expected, actual);
        }
    }
}
