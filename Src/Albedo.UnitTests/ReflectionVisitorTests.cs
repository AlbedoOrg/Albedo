﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        [Fact]
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

        [Fact]
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
        public void VisitAssemblyRelaysTypeElements()
        {
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new ReflectionVisitor();
            Assembly assembly = Assembly.GetExecutingAssembly();
            var typeElements = assembly.GetTypes().Select(t => t.ToElement()).ToArray();
            Mock.Get(sut).Setup(x => x.Visit(It.Is<TypeElement[]>(
                    p => AreEquivalent(p, typeElements))))
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
        public void VisitTypeElementsRelaysEachTypeElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor1 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor2 = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();

            var typeElement1 = typeof(object).ToElement();
            var typeElement2 = typeof(int).ToElement();
            var typeElement3 = typeof(string).ToElement();

            Mock.Get(sut).Setup(x => x.Visit(typeElement1)).Returns(visitor1);
            Mock.Get(visitor1).Setup(x => x.Visit(typeElement2)).Returns(visitor2);
            Mock.Get(visitor2).Setup(x => x.Visit(typeElement3)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(new[] { typeElement1, typeElement2, typeElement3 });

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitTypeElementRelaysFieldInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor1 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor2 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor3 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor4 = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();
            var typeElement = typeof(TypeWithFields).ToElement();
            var fieldInfoElements = typeElement.Type.GetFields(GetDefaultBindingFlags())
                .Select(f => f.ToElement()).ToArray();
            Assert.Equal(4, fieldInfoElements.Length);

            Mock.Get(sut).Setup(x => x.Visit(It.Is<FieldInfoElement[]>(
                    p => AreEquivalent(p, fieldInfoElements))))
                .Returns(visitor1);
            Mock.Get(visitor1).Setup(x => x.Visit(It.IsAny<ConstructorInfoElement[]>())).Returns(visitor2);
            Mock.Get(visitor2).Setup(x => x.Visit(It.IsAny<PropertyInfoElement[]>())).Returns(visitor3);
            Mock.Get(visitor3).Setup(x => x.Visit(It.IsAny<MethodInfoElement[]>())).Returns(visitor4);
            Mock.Get(visitor4).Setup(x => x.Visit(It.IsAny<EventInfoElement[]>())).Returns(expected);

            // Exercise system
            var actual = sut.Visit(typeElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitTypeElementRelaysConstructorInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor1 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor2 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor3 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor4 = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();
            var typeElement = typeof(TypeWithCtors).ToElement();
            var constructorInfoElements = typeElement.Type.GetConstructors(GetDefaultBindingFlags())
                .Select(c => c.ToElement()).ToArray();
            Assert.Equal(3, constructorInfoElements.Length);
            
            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<FieldInfoElement[]>())).Returns(visitor1);
            Mock.Get(visitor1).Setup(x => x.Visit(It.Is<ConstructorInfoElement[]>(
                    p => AreEquivalent(p, constructorInfoElements))))
                .Returns(visitor2);
            Mock.Get(visitor2).Setup(x => x.Visit(It.IsAny<PropertyInfoElement[]>())).Returns(visitor3);
            Mock.Get(visitor3).Setup(x => x.Visit(It.IsAny<MethodInfoElement[]>())).Returns(visitor4);
            Mock.Get(visitor4).Setup(x => x.Visit(It.IsAny<EventInfoElement[]>())).Returns(expected);

            // Exercise system
            var actual = sut.Visit(typeElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitTypeElementRelaysPropertyInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor1 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor2 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor3 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor4 = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();
            var typeElement = typeof(TypeWithProperties).ToElement();
            var propertyInfoElements = typeElement.Type.GetProperties(GetDefaultBindingFlags())
                .Select(pi => pi.ToElement()).ToArray();
            Assert.Equal(10, propertyInfoElements.Length);

            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<FieldInfoElement[]>())).Returns(visitor1);
            Mock.Get(visitor1).Setup(x => x.Visit(It.IsAny<ConstructorInfoElement[]>())).Returns(visitor2);
            Mock.Get(visitor2).Setup(x => x.Visit(It.Is<PropertyInfoElement[]>(
                    p => AreEquivalent(p, propertyInfoElements))))
                .Returns(visitor3);
            Mock.Get(visitor3).Setup(x => x.Visit(It.IsAny<MethodInfoElement[]>())).Returns(visitor4);
            Mock.Get(visitor4).Setup(x => x.Visit(It.IsAny<EventInfoElement[]>())).Returns(expected);

            // Exercise system
            var actual = sut.Visit(typeElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitTypeElementRelaysMethodInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor1 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor2 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor3 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor4 = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();
            var typeElement = typeof(TypeWithMethods).ToElement();
            var methodInfoElements = typeElement.Type.GetMethods(GetDefaultBindingFlags())
                .Except(typeElement.Type.GetProperties(GetDefaultBindingFlags())
                    .SelectMany(p => p.GetAccessors(true)))
                .Select(m => m.ToElement())
                .ToArray();
            Assert.Equal(9, methodInfoElements.Length);

            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<FieldInfoElement[]>())).Returns(visitor1);
            Mock.Get(visitor1).Setup(x => x.Visit(It.IsAny<ConstructorInfoElement[]>())).Returns(visitor2);
            Mock.Get(visitor2).Setup(x => x.Visit(It.IsAny<PropertyInfoElement[]>())).Returns(visitor3);
            Mock.Get(visitor3).Setup(x => x.Visit(It.Is<MethodInfoElement[]>(
                    p => AreEquivalent(p, methodInfoElements))))
                .Returns(visitor4);
            Mock.Get(visitor4).Setup(x => x.Visit(It.IsAny<EventInfoElement[]>())).Returns(expected);

            // Exercise system
            var actual = sut.Visit(typeElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitTypeElementRelaysEventInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor1 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor2 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor3 = new Mock<ReflectionVisitor<T>>().Object;
            var visitor4 = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();
            var typeElement = typeof(TypeWithEvents).ToElement();
            var eventInfoElements = typeElement.Type.GetEvents(GetDefaultBindingFlags())
                .Select(e => e.ToElement()).ToArray();
            Assert.Equal(3, eventInfoElements.Length);

            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<FieldInfoElement[]>())).Returns(visitor1);
            Mock.Get(visitor1).Setup(x => x.Visit(It.IsAny<ConstructorInfoElement[]>())).Returns(visitor2);
            Mock.Get(visitor2).Setup(x => x.Visit(It.IsAny<PropertyInfoElement[]>())).Returns(visitor3);
            Mock.Get(visitor3).Setup(x => x.Visit(It.IsAny<MethodInfoElement[]>())).Returns(visitor4);
            Mock.Get(visitor4).Setup(x => x.Visit(It.Is<EventInfoElement[]>(
                    p => AreEquivalent(p, eventInfoElements))))
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
        public void VisitFieldInfoElementsRelaysEachFieldInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();

            var fieldInfoElement1 = TypeWithFields.Field.ToElement();
            var fieldInfoElement2 = TypeWithFields.OtherField.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(fieldInfoElement1)).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(fieldInfoElement2)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(new[] { fieldInfoElement1, fieldInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitNullConstructorInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();
            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement[])null));
            Assert.Equal("constructorInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitConstructorInfoElementsRelaysEachConstructorInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();

            var constructorInfoElement1 = TypeWithCtors.Ctor.ToElement();
            var constructorInfoElement2 = TypeWithCtors.OtherCtor.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(constructorInfoElement1)).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(constructorInfoElement2)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(new[] { constructorInfoElement1, constructorInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitNullPropertyInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();
            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement[])null));
            Assert.Equal("propertyInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitPropertyInfoElementsRelaysEachPropertyInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();

            var propertyInfoElement1 = TypeWithProperties.Property.ToElement();
            var propertyInfoElement2 = TypeWithProperties.OtherProperty.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(propertyInfoElement1)).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(propertyInfoElement2)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(new[] { propertyInfoElement1, propertyInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitNullMethodInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();
            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement[])null));
            Assert.Equal("methodInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitMethodInfoElementsRelaysEachMethodInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();

            var methodInfoElement1 = TypeWithMethods.Method.ToElement();
            var methodInfoElement2 = TypeWithMethods.OtherMethod.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(methodInfoElement1)).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(methodInfoElement2)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(new[] { methodInfoElement1, methodInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitNullEventInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();
            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((EventInfoElement[])null));
            Assert.Equal("eventInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitEventInfoElementsRelaysEachEventInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();

            var eventInfoElement1 = TypeWithEvents.LocalEvent.ToElement();
            var eventInfoElement2 = TypeWithEvents.OtherEvent.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(eventInfoElement1)).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(eventInfoElement2)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(new[] { eventInfoElement1, eventInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitNullParameterInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();
            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((ParameterInfoElement[])null));
            Assert.Equal("parameterInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitParameterInfoElementsRelaysEachParameterInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();

            var parameterInfoElement1 = TypeWithParameters.Parameter.ToElement();
            var parameterInfoElement2 = TypeWithParameters.OtherParameter.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(parameterInfoElement1)).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(parameterInfoElement2)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(new[] { parameterInfoElement1, parameterInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitNullLoalVariableInfoElementsThrows()
        {
            var sut = new ReflectionVisitor();
            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((LocalVariableInfoElement[])null));
            Assert.Equal("localVariableInfoElements", e.ParamName);
        }

        [Fact]
        public void VisitLocalVariableInfoElementsRelaysEachLocalVariableInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();

            var localVariableInfoElement1 = TypeWithLocalVariables.LocalVariable.ToElement();
            var localVariableInfoElement2 = TypeWithLocalVariables.OtherLocalVariable.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(localVariableInfoElement1)).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(localVariableInfoElement2)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(new[] { localVariableInfoElement1, localVariableInfoElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitConstructorInfoElementRelaysParameterInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();
            var constructorInfoElement = TypeWithCtors.OtherCtor.ToElement();
            var parameterInfoElements = constructorInfoElement.ConstructorInfo
                .GetParameters().Select(pi => pi.ToElement()).ToArray();

            Mock.Get(sut).Setup(x => x.Visit(It.Is<ParameterInfoElement[]>(
                    p => AreEquivalent(p, parameterInfoElements))))
                .Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement[]>())).Returns(expected);

            // Exercise system
            var actual = sut.Visit(constructorInfoElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitConstructorInfoElementRelaysLocalVariableInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();
            var constructorInfoElement = TypeWithCtors.OtherCtor.ToElement();
            var localVariableTypes = TypeWithCtors.LocalVariablesOfOtherCtor.Select(l => l.LocalType).ToArray();

            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<ParameterInfoElement[]>())).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(It.Is<LocalVariableInfoElement[]>(
                   l => AreEquivalent(l.Select(li => li.LocalVariableInfo.LocalType).ToArray(), localVariableTypes))))
               .Returns(expected);

            // Exercise system
            var actual = sut.Visit(constructorInfoElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitMethodInfoElementRelaysParameterInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();
            var methodInfoElement = TypeWithMethods.OtherMethod.ToElement();
            var parameterInfoElements = methodInfoElement.MethodInfo
                .GetParameters().Select(pi => pi.ToElement()).ToArray();

            Mock.Get(sut).Setup(x => x.Visit(It.Is<ParameterInfoElement[]>(
                    p => AreEquivalent(p, parameterInfoElements))))
                .Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(It.IsAny<LocalVariableInfoElement[]>())).Returns(expected);

            // Exercise system
            var actual = sut.Visit(methodInfoElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitMethodInfoElementRelaysLocalVariableInfoElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();
            var methodInfoElement = TypeWithMethods.OtherMethod.ToElement();
            var localVariableTypes = TypeWithMethods.LocalVariablesOfOtherMethod.Select(l => l.LocalType).ToArray();

            Mock.Get(sut).Setup(x => x.Visit(It.IsAny<ParameterInfoElement[]>())).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(It.Is<LocalVariableInfoElement[]>(
                   l => AreEquivalent(l.Select(li => li.LocalVariableInfo.LocalType).ToArray(), localVariableTypes))))
               .Returns(expected);

            // Exercise system
            var actual = sut.Visit(methodInfoElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitPropertyInfoElementRelaysGetAndSetMethodInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();
            var propertyInfoElement = TypeWithProperties.OtherProperty.ToElement();
            var getMethodInfoElement = propertyInfoElement.PropertyInfo.GetGetMethod().ToElement();
            var setMethodInfoElement = propertyInfoElement.PropertyInfo.GetSetMethod().ToElement();

            Mock.Get(sut).Setup(x => x.Visit(getMethodInfoElement)).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(setMethodInfoElement)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(propertyInfoElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitReadOnlyPropertyInfoElementOnlyRelaysGetMethodInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new ReflectionVisitor();
            var readOnlyPropertyInfoElement = TypeWithProperties.ReadOnlyProperty.ToElement();
            var getMethodInfoElement = readOnlyPropertyInfoElement.PropertyInfo.GetGetMethod().ToElement();
            Mock.Get(sut).Setup(x => x.Visit(getMethodInfoElement)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(readOnlyPropertyInfoElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitWriteOnlyPropertyInfoElementOnlyRelaysSetMethodInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new ReflectionVisitor();
            var writeOnlyPropertyInfoElement = TypeWithProperties.WriteOnlyProperty.ToElement();
            var setMethodInfoElement = writeOnlyPropertyInfoElement.PropertyInfo.GetSetMethod().ToElement();
            Mock.Get(sut).Setup(x => x.Visit(setMethodInfoElement)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(writeOnlyPropertyInfoElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitMethodElementIfLocalVariablesReturnsNullDoesNotThrows()
        {
            // Fixture setup
            var sut = new ReflectionVisitor();

            // The GetType method of ValueType represents NULL LocalVariables.
            var methodInfoElements = typeof(int).GetMethod("GetType").ToElement();

            // Exercise system
            // Verify outcome
            Assert.DoesNotThrow(() => sut.Visit(methodInfoElements));
        }

        [Fact]
        public void VisitNullTypeElementThrows()
        {
            var sut = new ReflectionVisitor();
            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((TypeElement)null));
            Assert.Equal("typeElement", e.ParamName);
        }

        [Fact]
        public void VisitNullConstructorInfoElementThrows()
        {
            var sut = new ReflectionVisitor();
            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((ConstructorInfoElement)null));
            Assert.Equal("constructorInfoElement", e.ParamName);
        }

        [Fact]
        public void VisitNullPropertyInfoElementThrows()
        {
            var sut = new ReflectionVisitor();
            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((PropertyInfoElement)null));
            Assert.Equal("propertyInfoElement", e.ParamName);
        }

        [Fact]
        public void VisitNullMethodInfoElementThrows()
        {
            var sut = new ReflectionVisitor();
            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((MethodInfoElement)null));
            Assert.Equal("methodInfoElement", e.ParamName);
        }

        [Fact]
        public void VisitNullAssemblyElementsThrows()
        {
            var sut = new ReflectionVisitor();
            var e = Assert.Throws<ArgumentNullException>(() => sut.Visit((AssemblyElement[])null));
            Assert.Equal("assemblyElements", e.ParamName);
        }

        [Fact]
        public void VisitAssemblyElementsRelaysEachAssemblyElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();

            var assemblyElement1 = typeof(ReflectionVisitor<>).Assembly.ToElement();
            var assemblyElement2 = typeof(ReflectionVisitorTests<>).Assembly.ToElement();

            Mock.Get(sut).Setup(x => x.Visit(assemblyElement1)).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(assemblyElement2)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(new[] { assemblyElement1, assemblyElement2 });

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void VisitPropertyInfoElementRelaysNonPublicGetAndSetMethodInfoElement()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var visitor = new Mock<ReflectionVisitor<T>>().Object;
            var expected = new ReflectionVisitor();
            var propertyInfoElement = TypeWithProperties.PrivateProperty.ToElement();
            var getMethodInfoElement = propertyInfoElement.PropertyInfo.GetGetMethod(true).ToElement();
            var setMethodInfoElement = propertyInfoElement.PropertyInfo.GetSetMethod(true).ToElement();

            Mock.Get(sut).Setup(x => x.Visit(getMethodInfoElement)).Returns(visitor);
            Mock.Get(visitor).Setup(x => x.Visit(setMethodInfoElement)).Returns(expected);

            // Exercise system
            var actual = sut.Visit(propertyInfoElement);

            // Verify outcome
            Assert.Equal(expected, actual);
        }

        private static bool AreEquivalent<TItem>(
            ICollection<TItem> expected,
            ICollection<TItem> actual)
        {
            return !expected.Except(actual).Any() && actual.Count == expected.Count;
        }

        private static BindingFlags GetDefaultBindingFlags()
        {
            return BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
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
