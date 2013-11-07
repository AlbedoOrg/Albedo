using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using Xunit.Extensions;
using Moq;
using System.Reflection;

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

        [Theory, ClassData(typeof(SourceObjects))]
        public void MaterializeAssembliesReturnsCorrectResult(
            object[] objects)
        {
            var sut = new AssemblyElementMaterializer<object>();

            var actual = sut.Materialize(objects);

            var expected = objects
                .OfType<Assembly>()
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

        private class SourceObjects : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new[]
                {
                    new object[]
                    {
                        typeof(Version).Assembly
                    } 
                };
                yield return new[]
                {
                    new object[]
                    {
                        typeof(AssemblyElement).Assembly
                    } 
                };
                yield return new[]
                {
                    new object[]
                    {
                        typeof(AssemblyElementTest).Assembly
                    } 
                };
                yield return new[]
                {
                    new object[]
                    {
                        typeof(AssemblyElementTest).Assembly,
                        typeof(Version).Assembly
                    } 
                };
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
