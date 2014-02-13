using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Moq;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public abstract class ReflectionVisitorTests<T>
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new ReflectionVisitor();
            Assert.IsAssignableFrom<IReflectionVisitor<T>>(sut);
        }

        [Fact(Skip = "Conflicted")]
        public void VisitAssemblyElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var assemblyElement =
                new AssemblyElement(this.GetType().Assembly);

            var actual = sut.Visit(assemblyElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void VisitConstructorInfoElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var constructorInfoElement =
                new ConstructorInfoElement(
                    this.GetType().GetConstructor(Type.EmptyTypes));

            var actual = sut.Visit(constructorInfoElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void VisitFieldInfoElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var fieldInfoElement =
                new FieldInfoElement(new Dummy().Field);

            var actual = sut.Visit(fieldInfoElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void VisitMethodInfoElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var methodInfoElement =
                new MethodInfoElement(new Dummy().Method);

            var actual = sut.Visit(methodInfoElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void VisitParameterInfoElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var parameterInfoElement =
                new ParameterInfoElement(new Dummy().Parameter);

            var actual = sut.Visit(parameterInfoElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void VisitPropertyInfoElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var propertyInfoElement =
                new PropertyInfoElement(new Dummy().Property);

            var actual = sut.Visit(propertyInfoElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact(Skip = "Conflicted")]
        public void VisitTypeElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var typeElement =
                new TypeElement(this.GetType());

            var actual = sut.Visit(typeElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void VisitLocalVariableInfoElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var localVariableInfoElement =
                new LocalVariableInfoElement(new Dummy().LocalVariable);

            var actual = sut.Visit(localVariableInfoElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void VisitEventInfoElementReturnsCorrectResult()
        {
            var sut = new ReflectionVisitor();
            var eventInfoElement =
                new EventInfoElement(new Dummy().Event);

            var actual = sut.Visit(eventInfoElement);

            var expected = sut;
            Assert.Same(expected, actual);
        }

        [Fact]
        public void VisitNullAssemblyElementThrows()
        {
            var sut = new ReflectionVisitor();

            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((AssemblyElement)null));
            Assert.Equal("assemblyElement", e.ParamName);
        }

        [Fact]
        public void VisitAssemblyRelaiesTypeElements()
        {
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;
            Assembly assembly = Assembly.GetExecutingAssembly();
            Mock.Get(sut).Setup(x => x.Visit(It.Is<TypeElement[]>(
                    p => p.SequenceEqual(assembly.GetTypes().Select(t => t.ToElement())))))
                .Returns(expected);

            var actual = sut.Visit(assembly.ToElement());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitNullTypeElementsThrows()
        {
            var sut = new ReflectionVisitor();

            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((TypeElement[])null));
            Assert.Equal("typeElements", e.ParamName);
        }

        [Fact]
        public void VisitEmptyTypeElementsReturnSUTItself()
        {
            var sut = new ReflectionVisitor();

            var actual = sut.Visit(new TypeElement[0]);

            Assert.Equal(sut, actual);
        }

        [Fact]
        public void VisitTypeElementsRelaiesEachTypeElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor1 = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor2 = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;

            var typeElement1 = typeof(object).ToElement();
            var typeElement2 = typeof(int).ToElement();
            var typeElement3 = typeof(string).ToElement();

            Mock.Get(sut).Setup(x => x.Visit(typeElement1)).Returns(visitor1).Verifiable();
            Mock.Get(visitor1).Setup(x => x.Visit(typeElement2)).Returns(visitor2).Verifiable();
            Mock.Get(visitor2).Setup(x => x.Visit(typeElement3)).Returns(expected).Verifiable();

            // Exercise system
            var actual = sut.Visit(new[] { typeElement1, typeElement2, typeElement3});

            // Verify outcome
            Assert.Equal(expected, actual);
            Mock.Get(sut).Verify();
            Mock.Get(visitor1).Verify();
            Mock.Get(visitor2).Verify();
        }

        [Fact]
        public void VisitTypeElementRelaiesFieldInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;
            var typeElement = typeof(TypeWithField).ToElement();
            Mock.Get(sut).Setup(x => x.Visit(It.Is<FieldInfoElement[]>(
                    p => p.SequenceEqual(typeElement.Type.GetFields().Select(f => f.ToElement())))))
                .Returns(expected);
            Mock.Get(expected).Setup(x => x.Visit(It.IsAny<ConstructorInfoElement[]>())).Returns(expected);
            Mock.Get(expected).Setup(x => x.Visit(It.IsAny<PropertyInfoElement[]>())).Returns(expected);
            Mock.Get(expected).Setup(x => x.Visit(It.IsAny<MethodInfoElement[]>())).Returns(expected);
            Mock.Get(expected).Setup(x => x.Visit(It.IsAny<EventInfoElement[]>())).Returns(expected);

            // Exercise system
            var actual = sut.Visit(typeElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitTypeElementRelaiesConstructorInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;
            var typeElement = typeof(TypeWithCtor).ToElement();
            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<FieldInfoElement[]>())).Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.Is<ConstructorInfoElement[]>(
                    p => p.SequenceEqual(typeElement.Type.GetConstructors().Select(c => c.ToElement())))))
                .Returns(expected);
            Mock.Get(expected).Setup(x => x.Visit(It.IsAny<PropertyInfoElement[]>())).Returns(expected);
            Mock.Get(expected).Setup(x => x.Visit(It.IsAny<MethodInfoElement[]>())).Returns(expected);
            Mock.Get(expected).Setup(x => x.Visit(It.IsAny<EventInfoElement[]>())).Returns(expected);

            // Exercise system
            var actual = sut.Visit(typeElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitTypeElementRelaiesPropertyInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;
            var typeElement = typeof(TypeWithProperty).ToElement();
            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<FieldInfoElement[]>())).Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<ConstructorInfoElement[]>())).Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.Is<PropertyInfoElement[]>(
                    p => p.SequenceEqual(typeElement.Type.GetProperties().Select(pi => pi.ToElement())))))
                .Returns(expected);
            Mock.Get(expected).Setup(x => x.Visit(It.IsAny<MethodInfoElement[]>())).Returns(expected);
            Mock.Get(expected).Setup(x => x.Visit(It.IsAny<EventInfoElement[]>())).Returns(expected);

            // Exercise system
            var actual = sut.Visit(typeElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitTypeElementRelaiesMethodInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;
            var typeElement = typeof(TypeWithMethod).ToElement();
            var methodInfos = typeElement.Type.GetMethods()
                .Except(typeElement.Type.GetProperties().SelectMany(p => p.GetAccessors()));
            Assert.Equal(6, methodInfos.Count());

            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<FieldInfoElement[]>())).Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<ConstructorInfoElement[]>())).Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<PropertyInfoElement[]>())).Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.Is<MethodInfoElement[]>(
                    p => p.SequenceEqual(methodInfos.Select(m => m.ToElement())))))
                .Returns(expected);
            Mock.Get(expected).Setup(x => x.Visit(It.IsAny<EventInfoElement[]>())).Returns(expected);

            // Exercise system
            var actual = sut.Visit(typeElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitTypeElementRelaiesEventInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;
            var typeElement = typeof(TypeWithEvent).ToElement();
            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<FieldInfoElement[]>())).Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<ConstructorInfoElement[]>())).Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<PropertyInfoElement[]>())).Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<MethodInfoElement[]>())).Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.Is<EventInfoElement[]>(
                    p => p.SequenceEqual(typeElement.Type.GetEvents().Select(e => e.ToElement())))))
                .Returns(expected);

            // Exercise system
            var actual = sut.Visit(typeElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitNullFieldInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();

            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((FieldInfoElement[])null));
            Assert.Equal("fieldInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitFieldInfoElementsRelaiesEachFieldInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;

            var fieldInfoElement1 = TypeWithField.Field.ToElement();
            var fieldInfoElement2 = TypeWithField.OtherField.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(fieldInfoElement1)).Returns(visitor).Verifiable();
            Mock.Get(visitor).Setup(x => x.Visit(fieldInfoElement2)).Returns(expected).Verifiable();

            // Exercise system
            var actual = sut.Visit(new[] { fieldInfoElement1, fieldInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
            Mock.Get(sut).Verify();
            Mock.Get(visitor).Verify();
        }

        [Fact]
        public void VisitNullConstructorInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();

            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement[])null));
            Assert.Equal("constructorInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitConstructorInfoElementsRelaiesEachConstructorInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;

            var constructorInfoElement1 = TypeWithCtor.Ctor.ToElement();
            var constructorInfoElement2 = TypeWithCtor.OtherCtor.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(constructorInfoElement1)).Returns(visitor).Verifiable();
            Mock.Get(visitor).Setup(x => x.Visit(constructorInfoElement2)).Returns(expected).Verifiable();

            // Exercise system
            var actual = sut.Visit(new[] { constructorInfoElement1, constructorInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
            Mock.Get(sut).Verify();
            Mock.Get(visitor).Verify();
        }

        [Fact]
        public void VisitNullPropertyInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();

            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement[])null));
            Assert.Equal("propertyInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitPropertyInfoElementsRelaiesEachPropertyInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;

            var propertyInfoElement1 = TypeWithProperty.Property.ToElement();
            var propertyInfoElement2 = TypeWithProperty.OtherProperty.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(propertyInfoElement1)).Returns(visitor).Verifiable();
            Mock.Get(visitor).Setup(x => x.Visit(propertyInfoElement2)).Returns(expected).Verifiable();

            // Exercise system
            var actual = sut.Visit(new[] { propertyInfoElement1, propertyInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
            Mock.Get(sut).Verify();
            Mock.Get(visitor).Verify();
        }

        [Fact]
        public void VisitNullMethodInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();

            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement[])null));
            Assert.Equal("methodInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitMethodInfoElementsRelaiesEachMethodInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;

            var methodInfoElement1 = TypeWithMethod.Method.ToElement();
            var methodInfoElement2 = TypeWithMethod.OtherMethod.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(methodInfoElement1)).Returns(visitor).Verifiable();
            Mock.Get(visitor).Setup(x => x.Visit(methodInfoElement2)).Returns(expected).Verifiable();

            // Exercise system
            var actual = sut.Visit(new[] { methodInfoElement1, methodInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
            Mock.Get(sut).Verify();
            Mock.Get(visitor).Verify();
        }

        [Fact]
        public void VisitNullEventInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();

            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((EventInfoElement[])null));
            Assert.Equal("eventInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitEventInfoElementsRelaiesEachEventInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;

            var eventInfoElement1 = TypeWithEvent.LocalEvent.ToElement();
            var eventInfoElement2 = TypeWithEvent.OtherEvent.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(eventInfoElement1)).Returns(visitor).Verifiable();
            Mock.Get(visitor).Setup(x => x.Visit(eventInfoElement2)).Returns(expected).Verifiable();

            // Exercise system
            var actual = sut.Visit(new[] { eventInfoElement1, eventInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
            Mock.Get(sut).Verify();
            Mock.Get(visitor).Verify();
        }

        [Fact]
        public void VisitNullParameterInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();

            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((ParameterInfoElement[])null));
            Assert.Equal("parameterInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitParameterInfoElementsRelaiesEachParameterInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;

            var parameterInfoElement1 = TypeWithParameter.Parameter.ToElement();
            var parameterInfoElement2 = TypeWithParameter.OtherParameter.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(parameterInfoElement1)).Returns(visitor).Verifiable();
            Mock.Get(visitor).Setup(x => x.Visit(parameterInfoElement2)).Returns(expected).Verifiable();

            // Exercise system
            var actual = sut.Visit(new[] { parameterInfoElement1, parameterInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
            Mock.Get(sut).Verify();
            Mock.Get(visitor).Verify();
        }

        [Fact]
        public void VisitNullLoalVariableInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();

            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((LocalVariableInfoElement[])null));
            Assert.Equal("localVariableInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitLocalVariableInfoElementsRelaiesEachLocalVariableInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;

            var localVariableInfoElement1 = TypeWithLocalVariable.LocalVariable.ToElement();
            var localVariableInfoElement2 = TypeWithLocalVariable.OtherLocalVariable.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(localVariableInfoElement1)).Returns(visitor).Verifiable();
            Mock.Get(visitor).Setup(x => x.Visit(localVariableInfoElement2)).Returns(expected).Verifiable();

            // Exercise system
            var actual = sut.Visit(new[] { localVariableInfoElement1, localVariableInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
            Mock.Get(sut).Verify();
            Mock.Get(visitor).Verify();
        }

        [Fact]
        public void VisitConstructorInfoElementRelaiesParameterInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;
            var constructorInfoElement = TypeWithCtor.OtherCtor.ToElement();
            var parameterInfoElements = constructorInfoElement.ConstructorInfo
                .GetParameters().Select(pi => pi.ToElement());

            Mock.Get(sut).Setup(x => x.Visit(It.Is<ParameterInfoElement[]>(
                    p => p.SequenceEqual(parameterInfoElements))))
                .Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement[]>())).Returns(expected);

            // Exercise system
            var actual = sut.Visit(constructorInfoElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitConstructorInfoElementRelaiesLocalVariableInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;
            var constructorInfoElement = TypeWithCtor.OtherCtor.ToElement();
            var localVariableTypes = TypeWithCtor.LocalVariablesOfOtherCtor.Select(l => l.LocalType);

            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<ParameterInfoElement[]>())).Returns(sut);
            Mock.Get(sut).Setup(x => x.Visit(It.Is<LocalVariableInfoElement[]>(
                   l => l.Select(li => li.LocalVariableInfo.LocalType).SequenceEqual(localVariableTypes))))
               .Returns(expected);

            // Exercise system
            var actual = sut.Visit(constructorInfoElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        private class ReflectionVisitor : ReflectionVisitor<T>
        {
            public override T Value
            {
                get { throw new NotImplementedException(); }
            }
        }

        private class Dummy
        {
            internal FieldInfo Field
            {
                get
                {
                    return this.GetType().GetFields(
                        BindingFlags.NonPublic | BindingFlags.Instance)[0];
                }
            }

            internal MethodInfo Method
            {
                get
                {
                    return this.GetType().GetMethods(
                        BindingFlags.NonPublic | BindingFlags.Instance)[0];
                }
            }

            internal ParameterInfo Parameter
            {
                get
                {
                    return this
                        .GetType()
                        .GetMethods(
                            BindingFlags.NonPublic | BindingFlags.Instance)
                        .SelectMany(x => x.GetParameters())
                        .First();
                }
            }

            internal PropertyInfo Property
            {
                get
                {
                    return this.GetType().GetProperties(
                        BindingFlags.NonPublic | BindingFlags.Instance)[0];
                }
            }

            internal LocalVariableInfo LocalVariable
            {
                get
                {
                    return this
                        .GetType()
                        .GetMethod(
                            "AnonymousMethodWithLocalVariable",
                            BindingFlags.NonPublic | BindingFlags.Instance)
                        .GetMethodBody()
                        .LocalVariables
                        .First();
                }
            }

            internal EventInfo Event
            {
                get
                {
                    return this.GetType().GetEvents(
                        BindingFlags.NonPublic | BindingFlags.Instance)[0];
                }
            }

            private int anonymousValue = 123;

            private int AnonymousProperty
            {
                get { return this.anonymousValue; }
            }

            private event EventHandler AnonymousEvent = (s, e) => { };

            private string AnonymousMethodWithLocalVariable() 
            {
                string value = "foo";
                return value;
            }

            private void AnonymousMethodWithParameter(object o) 
            {
            }
        }
    }

    public class ReflectionVisitorTestsOfObject : ReflectionVisitorTests<object> { }
    public class ReflectionVisitorTestsOfDouble : ReflectionVisitorTests<double> { }
    public class ReflectionVisitorTestsOfString : ReflectionVisitorTests<string> { }
}
