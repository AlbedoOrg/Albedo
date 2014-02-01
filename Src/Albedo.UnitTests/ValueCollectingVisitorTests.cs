using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo.Refraction;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class ValueCollectingVisitorTests
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new ValueCollectingVisitor<object>(new object());
            Assert.IsAssignableFrom<IReflectionVisitor<IEnumerable<object>>>(sut);
            Assert.IsAssignableFrom<ReflectionVisitor<IEnumerable<object>>>(sut);
        }

        [Fact]
        public void ValueIsInitiallyCorrect()
        {
            var expectedValues = new[] {new object(), new object()};
            var sut = new ValueCollectingVisitor<object>(new object(), expectedValues);
            Assert.Equal(expectedValues, sut.Value);
        }

        [Fact]
        public void ConstructWithNullTargetThrows()
        {
            var e = Assert.Throws<ArgumentNullException>(() =>
                new ValueCollectingVisitor<object>(null));

            Assert.Equal("target", e.ParamName);
        }

        [Theory]
        [ClassData(typeof(AllElementsExceptPropertyAndField))]
        public void DoesNotVisitElementsOtherThanPropertyAndField(
            IReflectionElement element)
        {
            var sut = new ValueCollectingVisitor<object>(new object());
            var acceptResult = element.Accept(sut);
            Assert.Same(sut, acceptResult);
        }

        [Fact]
        public void VisitFieldElementWithNoInitialValuesProducesTheCorrectValue()
        {
            var reflect = new Fields<TypeWithPublicIntValues>();
            var target = new TypeWithPublicIntValues();
            var sut = new ValueCollectingVisitor<object>(target);
            var field = reflect.Select(x => x.Field2).ToElement();

            // Act
            var result = sut.Visit(field);

            // Assert
            Assert.Equal(new object[] { 2 }, result.Value);
        }

        [Fact]
        public void VisitPropertyElementWithNoInitialValuesProducesTheCorrectValue()
        {
            var reflect = new Properties<TypeWithPublicIntValues>();
            var target = new TypeWithPublicIntValues();
            var sut = new ValueCollectingVisitor<object>(target);
            var field = reflect.Select(x => x.Property2).ToElement();

            // Act
            var result = sut.Visit(field);

            // Assert
            Assert.Equal(new object[] { 2 }, result.Value);
        }

        [Fact]
        public void VisitPropertyThatIsNotAssignableFromValueCollectionTypeProducesNothing()
        {
            var reflect = new Properties<TypeWithPublicIntValues>();
            var target = new TypeWithPublicIntValues();
            var sut = new ValueCollectingVisitor<string>(target);
            var field = reflect.Select(x => x.Property2).ToElement();

            // Act
            var result = sut.Visit(field);

            // Assert
            Assert.Same(sut, result);
        }

        [Fact]
        public void VisitFieldThatIsNotAssignableFromValueCollectionTypeProducesNothing()
        {
            var reflect = new Fields<TypeWithPublicIntValues>();
            var target = new TypeWithPublicIntValues();
            var sut = new ValueCollectingVisitor<string>(target);
            var field = reflect.Select(x => x.Field2).ToElement();

            // Act
            var result = sut.Visit(field);

            // Assert
            Assert.Same(sut, result);
        }

        [Fact]
        public void AcceptFieldElementsWithInitialValuesProducesTheCorrectValues()
        {
            // Arrange
            var initialVisitorValues = new[] { 9, 8, 7 };
            var target = new TypeWithPublicIntValues();

            var expected = initialVisitorValues
                .Concat(new[] { target.Field1, target.Field2, target.Field3 });

            var reflect = new Fields<TypeWithPublicIntValues>();
            var fieldElements = new[]
            {
                reflect.Select(x => x.Field1),
                reflect.Select(x => x.Field2),
                reflect.Select(x => x.Field3)
            }.Select(e => e.ToElement()).Cast<IReflectionElement>();

            var sut = new ValueCollectingVisitor<int>(target, initialVisitorValues);

            // Act
            var result = fieldElements.Accept(sut);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        [Fact]
        public void AcceptPropertyElementsWithInitialValuesProducesTheCorrectValues()
        {
            // Arrange
            var initialVisitorValues = new[] { 9, 8, 7 };
            var target = new TypeWithPublicIntValues();

            var expected = initialVisitorValues
                .Concat(new[] { target.Property1, target.Property2 });

            var reflect = new Properties<TypeWithPublicIntValues>();
            var propertyElements = new[]
            {
                reflect.Select(x => x.Property1),
                reflect.Select(x => x.Property2),
            }.Select(e => e.ToElement()).Cast<IReflectionElement>();

            var sut = new ValueCollectingVisitor<int>(target, initialVisitorValues);

            // Act
            var result = propertyElements.Accept(sut);

            // Assert
            Assert.Equal(expected, result.Value);
        }

        class TypeWithPublicIntValues
        {
            public int Field1 = 1;
            public int Field2 = 2;
            public int Field3 = 3;

            public int Property1 { get { return 1; } }
            public int Property2 { get { return 2; } }
        }

        class AllElementsExceptPropertyAndField : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new NullReflectionElement() };
                yield return new object[] { new AssemblyElement(typeof(AssemblyElement).Assembly) };
                yield return new object[] { new ConstructorInfoElement(GetType().GetConstructors()[0]) };
                yield return new object[] { new EventInfoElement(typeof(AppDomain).GetEvents()[0]) };
                yield return new object[] { new LocalVariableInfoElement(TypeWithLocalVariable.LocalVariable) };
                yield return new object[] { new MethodInfoElement(typeof(MethodInfoElement).GetMethods()[0]) };
                yield return new object[] { new ParameterInfoElement(TypeWithParameter.Parameter) };
                yield return new object[] { new TypeElement(typeof(TypeElement)) };
                yield return new object[] { new ParameterInfoElement(TypeWithParameter.Parameter) };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        class TypeWithLocalVariable
        {
            public static LocalVariableInfo LocalVariable
            {
                get
                {
                    return typeof(TypeWithLocalVariable)
                        .GetMethod("TheMethod")
                        .GetMethodBody()
                        .LocalVariables[0];
                }
            }

            public static LocalVariableInfo OtherLocalVariable
            {
                get
                {
                    return typeof(TypeWithLocalVariable)
                        .GetMethod("TheOtherMethod")
                        .GetMethodBody()
                        .LocalVariables[0];
                }
            }

            public void TheMethod()
            {
                // This is required to prevent the compiler from
                // warning and optimising away the local variable.
                var local = 1;
                local = local + 1;
                local = local + 2;
            }

            public void TheOtherMethod()
            {
                // This is required to prevent the compiler from
                // warning and optimising away the local variable.
                var local = 1;
                local = local + 1;
                local = local + 2;
            }
        }

        class TypeWithParameter
        {
            public static ParameterInfo Parameter
            {
                get
                {
                    return typeof(TypeWithParameter)
                        .GetMethod("TheMethod")
                        .GetParameters()
                        .First();
                }
            }

            public static ParameterInfo OtherParameter
            {
                get
                {
                    return typeof(TypeWithParameter)
                        .GetMethod("TheOtherMethod")
                        .GetParameters()
                        .First();
                }
            }

            public void TheMethod(int param1)
            {
            }

            public void TheOtherMethod(int param1)
            {
            }
        }
    }
}
