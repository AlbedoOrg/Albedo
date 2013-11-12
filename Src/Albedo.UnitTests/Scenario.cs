using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Extensions;

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

        private class SemanticElementComparer : IEqualityComparer<IReflectionElement>
        {
            public bool Equals(IReflectionElement x, IReflectionElement y)
            {
                var values = new CompositeReflectionElement(x, y)
                    .Accept(new SemanticReflectionVisitor())
                    .Value
                    .ToArray();
                return values.Length == 2
                    && values.Distinct().Count() == 1;
            }

            public int GetHashCode(IReflectionElement obj)
            {
                return obj
                    .Accept(new SemanticReflectionVisitor())
                    .Value
                    .Single()
                    .GetHashCode();
            }

            private class SemanticComparisonValue
            {
                private readonly string name;
                private readonly Type type;

                public SemanticComparisonValue(string name, Type type)
                {
                    this.name = name;
                    this.type = type;
                }

                public override bool Equals(object obj)
                {
                    var other = obj as SemanticComparisonValue;
                    if (other == null)
                        return base.Equals(obj);

                    return object.Equals(this.type, other.type)
                        && string.Equals(this.name, other.name,
                            StringComparison.OrdinalIgnoreCase);
                }

                public override int GetHashCode()
                {
                    return
                        this.name.ToUpperInvariant().GetHashCode() ^
                        this.type.GetHashCode();
                }
            }

            private class SemanticReflectionVisitor :
                ReflectionVisitor<IEnumerable<SemanticComparisonValue>>
            {
                private readonly SemanticComparisonValue[] values;

                public SemanticReflectionVisitor(
                    params SemanticComparisonValue[] values)
                {
                    this.values = values;
                }

                public override IEnumerable<SemanticComparisonValue> Value
                {
                    get { return this.values; }
                }

                public override IReflectionVisitor<IEnumerable<SemanticComparisonValue>> Visit(
                    FieldInfoElement fieldInfoElement)
                {
                    var v = new SemanticComparisonValue(
                        fieldInfoElement.FieldInfo.Name,
                        fieldInfoElement.FieldInfo.FieldType);
                    return new SemanticReflectionVisitor(
                        this.values.Concat(new[] { v }).ToArray());
                }

                public override IReflectionVisitor<IEnumerable<SemanticComparisonValue>> Visit(
                    ParameterInfoElement parameterInfoElement)
                {
                    var v = new SemanticComparisonValue(
                        parameterInfoElement.ParameterInfo.Name,
                        parameterInfoElement.ParameterInfo.ParameterType);
                    return new SemanticReflectionVisitor(
                        this.values.Concat(new[] { v }).ToArray());
                }

                public override IReflectionVisitor<IEnumerable<SemanticComparisonValue>> Visit(
                    PropertyInfoElement propertyInfoElement)
                {
                    var v = new SemanticComparisonValue(
                        propertyInfoElement.PropertyInfo.Name,
                        propertyInfoElement.PropertyInfo.PropertyType);
                    return new SemanticReflectionVisitor(
                        this.values.Concat(new[] { v }).ToArray());
                }
            }
        }
    }
}
