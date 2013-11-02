using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using Xunit.Extensions;
using Moq;

namespace Ploeh.Albedo.UnitTests
{
    public class AssemblyElementMaterializerTests
    {
        [Fact]
        public void SutIsReflectionElementMaterializer()
        {
            var sut = new AssemblyElementMaterializer<object>();
            Assert.IsAssignableFrom<IReflectionElementMaterializer<object>>(sut);
        }

        [Theory]
        [InlineData(new object[] { new[] { typeof(Version) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElement) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElementTest) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElement), typeof(Version) } })]
        public void MaterializeAssembliesReturnsCorrectResult(
            Type[] containedTypes)
        {
            var assemblies = containedTypes.Select(t => t.Assembly).ToArray();
            var sut = new AssemblyElementMaterializer<object>();

            var actual = sut.Materialize(assemblies);

            var expected = assemblies
                .Select(a => new AssemblyElement(a))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MaterializeNullSourceThrows()
        {
            var sut = new AssemblyElementMaterializer<object>();
            Assert.Throws<ArgumentNullException>(() => sut.Materialize(null));
        }

        [Theory]
        [InlineData(new object[] { new[] { typeof(Version) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElement) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElementTest) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElement), typeof(Version) } })]
        public void MaterializePassesAssemblyElementsThrough(
            Type[] containedTypes)
        {
            var expected = containedTypes
                .Select(t => new AssemblyElement(t.Assembly))
                .ToArray();
            var sut = new AssemblyElementMaterializer<object>();

            var actual = sut.Materialize(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MaterializePassesAnyReflectionElementThrough()
        {
            var expected = new[] { new Mock<IReflectionElement>().Object };
            var sut = new AssemblyElementMaterializer<object>();

            var actual = sut.Materialize(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MaterializePreservesOrderingBetweenElementsAndAssemblies()
        {
            var source = new object[]
            {
                typeof(AssemblyElementTest).Assembly,
                new AssemblyElement(this.GetType().Assembly),
                typeof(Version).Assembly
            };
            var sut = new AssemblyElementMaterializer<object>();

            var actual = sut.Materialize(source);

            var expected = new[]
            {
                new AssemblyElement(typeof(AssemblyElementTest).Assembly),
                new AssemblyElement(this.GetType().Assembly),
                new AssemblyElement(typeof(Version).Assembly),
            };
            Assert.Equal(expected, actual);
        }
    }
}
