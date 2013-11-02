using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class TypeElementMaterializerTests
    {
        [Fact]
        public void SutIsReflectionElementMaterializer()
        {
            var sut = new TypeElementMaterializer<object>();
            Assert.IsAssignableFrom<IReflectionElementMaterializer<object>>(sut);
        }

        [Theory]
        [InlineData(new object[] { new[] { typeof(Version) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElement) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElementTest) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElement), typeof(Version) } })]
        [InlineData(new object[] { new object[] { typeof(AssemblyElement), "", typeof(Version) } })]
        public void MaterializeTypesReturnsCorrectResult(object[] types)
        {
            var sut = new TypeElementMaterializer<object>();

            var actual = sut.Materialize(types);

            var expected = types
                .OfType<Type>()
                .Select(t => new TypeElement(t))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MaterializeNullSourceThrows()
        {
            var sut = new TypeElementMaterializer<object>();
            Assert.Throws<ArgumentNullException>(() => sut.Materialize(null));
        }
    }
}
