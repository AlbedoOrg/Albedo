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
            var assembly = Assembly.GetExecutingAssembly();
            Mock.Get(sut).Setup(x => x.Visit(It.Is<TypeElement[]>(
                    p => p.Select(t => t.Type).SequenceEqual(assembly.GetTypes()))))
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
        public void VisitTypeElementRelaiesFieldElements()
        {
            // Fixture setup
            var sut = new Mock<ReflectionVisitor<T>> { CallBase = true }.Object;
            var expected = new Mock<ReflectionVisitor<T>>().Object;
            var typeElement = typeof(TypeWithField).ToElement();
            Mock.Get(sut).Setup(x => x.Visit(It.Is<FieldInfoElement[]>(
                    p => p.Select(f => f.FieldInfo).SequenceEqual(typeElement.Type.GetFields()))))
                .Returns(expected);

            // Exercise system
            var actual = sut.Visit(typeElement);

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
