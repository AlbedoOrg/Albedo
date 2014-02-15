using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Extensions;
using Ploeh.Albedo.UnitTests.Samples.SemanticComparison;
using Ploeh.Albedo.UnitTests.Samples.ValueWriting;

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

            var actual = 
                new SemanticElementComparer(new SemanticReflectionVisitor())
                    .Equals(prop, param);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReadFromProperty()
        {
            PropertyInfo pi = from v in new Properties<Version>()
                              select v.Minor;
            var version = new Version(2, 7);
            var visitor = new ValueCollectingVisitor(version);

            var actual = new PropertyInfoElement(pi).Accept(visitor);

            Assert.Equal(version.Minor, actual.Value.OfType<int>().First());
        }

        [Fact]
        public void ReadFromSeveralPropertiesAndFields()
        {
            var ts = new TimeSpan(2, 4, 3, 8, 9);
            var elements = ts.GetType().GetPublicPropertiesAndFields().ToArray();

            var actual = elements.Accept(new ValueCollectingVisitor(ts));

            var actualValues = actual.Value.ToArray();
            Assert.Equal(elements.Length, actualValues.Length);
            Assert.Equal(1, actualValues.Count(ts.Days.Equals));
            Assert.Equal(1, actualValues.Count(ts.Hours.Equals));
            Assert.Equal(1, actualValues.Count(ts.Milliseconds.Equals));
            Assert.Equal(1, actualValues.Count(ts.Minutes.Equals));
            Assert.Equal(1, actualValues.Count(ts.Seconds.Equals));
            Assert.Equal(1, actualValues.Count(ts.Ticks.Equals));
            Assert.Equal(1, actualValues.Count(ts.TotalDays.Equals));
            Assert.Equal(1, actualValues.Count(ts.TotalHours.Equals));
            Assert.Equal(1, actualValues.Count(ts.TotalMilliseconds.Equals));
            Assert.Equal(1, actualValues.Count(ts.TotalMinutes.Equals));
            Assert.Equal(1, actualValues.Count(ts.TotalSeconds.Equals));
            Assert.Equal(1, actualValues.Count(TimeSpan.MaxValue.Equals));
            Assert.Equal(1, actualValues.Count(TimeSpan.MinValue.Equals));
            Assert.Equal(1, actualValues.Count(TimeSpan.TicksPerDay.Equals));
            Assert.Equal(1, actualValues.Count(TimeSpan.TicksPerHour.Equals));
            Assert.Equal(1, actualValues.Count(TimeSpan.TicksPerMillisecond.Equals));
            Assert.Equal(1, actualValues.Count(TimeSpan.TicksPerMinute.Equals));
            Assert.Equal(1, actualValues.Count(TimeSpan.TicksPerSecond.Equals));
            Assert.Equal(1, actualValues.Count(TimeSpan.Zero.Equals));
        }

        [Fact]
        public void WriteToSeveralPropertiesAndFields()
        {
            var t = new ClassWithWritablePropertiesAndFields<int>();
            var elements = t.GetType().GetPublicPropertiesAndFields().ToArray();

            elements.Accept(new ValueWritingVisitor(t)).Value(42);
            
            Assert.Equal(42, t.Field1);
            Assert.Equal(42, t.Field2);
            Assert.Equal(42, t.Property1);
            Assert.Equal(42, t.Property2);
        }
    }
}
