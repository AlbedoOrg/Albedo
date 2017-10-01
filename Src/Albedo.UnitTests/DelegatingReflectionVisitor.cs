using System;

namespace Albedo.UnitTests
{
    internal class DelegatingReflectionVisitor<T> : IReflectionVisitor<T>
    {
        public T Value { get; set; }

        public DelegatingReflectionVisitor()
        {
            this.OnVisitAssemblyElement = e => this;
            this.OnVisitConstructorInfoElement = e => this;
            this.OnVisitFieldInfoElement = e => this;
            this.OnVisitMethodInfoElement = e => this;
            this.OnVisitParameterInfoElement = e => this;
            this.OnVisitPropertyInfoElement = e => this;
            this.OnVisitTypeElement = e => this;
            this.OnVisitLocalVariableInfoElement = e => this;
            this.OnVisitEventInfoElement = e => this;

            this.OnVisitAssemblyElements = e => this;
            this.OnVisitConstructorInfoElements = e => this;
            this.OnVisitFieldInfoElements = e => this;
            this.OnVisitMethodInfoElements = e => this;
            this.OnVisitParameterInfoElements = e => this;
            this.OnVisitPropertyInfoElements = e => this;
            this.OnVisitTypeElements = e => this;
            this.OnVisitLocalVariableInfoElements = e => this;
            this.OnVisitEventInfoElements = e => this;
        }

        public Func<AssemblyElement, IReflectionVisitor<T>> OnVisitAssemblyElement { get; set; }
        public Func<ConstructorInfoElement, IReflectionVisitor<T>> OnVisitConstructorInfoElement { get; set; }
        public Func<FieldInfoElement, IReflectionVisitor<T>> OnVisitFieldInfoElement { get; set; }
        public Func<MethodInfoElement, IReflectionVisitor<T>> OnVisitMethodInfoElement { get; set; }
        public Func<ParameterInfoElement, IReflectionVisitor<T>> OnVisitParameterInfoElement { get; set; }
        public Func<PropertyInfoElement, IReflectionVisitor<T>> OnVisitPropertyInfoElement { get; set; }
        public Func<TypeElement, IReflectionVisitor<T>> OnVisitTypeElement { get; set; }
        public Func<LocalVariableInfoElement, IReflectionVisitor<T>> OnVisitLocalVariableInfoElement { get; set; }
        public Func<EventInfoElement, IReflectionVisitor<T>> OnVisitEventInfoElement { get; set; }

        public Func<AssemblyElement[], IReflectionVisitor<T>> OnVisitAssemblyElements { get; set; }
        public Func<ConstructorInfoElement[], IReflectionVisitor<T>> OnVisitConstructorInfoElements { get; set; }
        public Func<FieldInfoElement[], IReflectionVisitor<T>> OnVisitFieldInfoElements { get; set; }
        public Func<MethodInfoElement[], IReflectionVisitor<T>> OnVisitMethodInfoElements { get; set; }
        public Func<ParameterInfoElement[], IReflectionVisitor<T>> OnVisitParameterInfoElements { get; set; }
        public Func<PropertyInfoElement[], IReflectionVisitor<T>> OnVisitPropertyInfoElements { get; set; }
        public Func<TypeElement[], IReflectionVisitor<T>> OnVisitTypeElements { get; set; }
        public Func<LocalVariableInfoElement[], IReflectionVisitor<T>> OnVisitLocalVariableInfoElements { get; set; }
        public Func<EventInfoElement[], IReflectionVisitor<T>> OnVisitEventInfoElements { get; set; }

        public IReflectionVisitor<T> Visit(AssemblyElement[] assemblyElements)
        {
            return OnVisitAssemblyElements(assemblyElements);
        }

        public virtual IReflectionVisitor<T> Visit(AssemblyElement assemblyElement)
        {
            return OnVisitAssemblyElement(assemblyElement);
        }

        public virtual IReflectionVisitor<T> Visit(ConstructorInfoElement constructorInfoElement)
        {
            return OnVisitConstructorInfoElement(constructorInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(FieldInfoElement fieldInfoElement)
        {
            return OnVisitFieldInfoElement(fieldInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(MethodInfoElement methodInfoElement)
        {
            return OnVisitMethodInfoElement(methodInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(ParameterInfoElement parameterInfoElement)
        {
            return OnVisitParameterInfoElement(parameterInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(PropertyInfoElement propertyInfoElement)
        {
            return OnVisitPropertyInfoElement(propertyInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(TypeElement typeElement)
        {
            return OnVisitTypeElement(typeElement);
        }

        public virtual IReflectionVisitor<T> Visit(LocalVariableInfoElement localVariableInfoElement)
        {
            return OnVisitLocalVariableInfoElement(localVariableInfoElement);
        }

        public virtual IReflectionVisitor<T> Visit(EventInfoElement eventInfoElement)
        {
            return OnVisitEventInfoElement(eventInfoElement);
        }

        public IReflectionVisitor<T> Visit(params TypeElement[] typeElements)
        {
            return OnVisitTypeElements(typeElements);
        }

        public IReflectionVisitor<T> Visit(params FieldInfoElement[] fieldInfoElements)
        {
            return OnVisitFieldInfoElements(fieldInfoElements);
        }

        public IReflectionVisitor<T> Visit(params ConstructorInfoElement[] constructorInfoElements)
        {
            return OnVisitConstructorInfoElements(constructorInfoElements);
        }

        public IReflectionVisitor<T> Visit(params PropertyInfoElement[] propertyInfoElements)
        {
            return OnVisitPropertyInfoElements(propertyInfoElements);
        }

        public IReflectionVisitor<T> Visit(params MethodInfoElement[] methodInfoElements)
        {
            return OnVisitMethodInfoElements(methodInfoElements);
        }

        public IReflectionVisitor<T> Visit(params EventInfoElement[] eventInfoElements)
        {
            return OnVisitEventInfoElements(eventInfoElements);
        }

        public IReflectionVisitor<T> Visit(params ParameterInfoElement[] parameterInfoElements)
        {
            return OnVisitParameterInfoElements(parameterInfoElements);
        }

        public IReflectionVisitor<T> Visit(params LocalVariableInfoElement[] localVariableInfoElements)
        {
            return OnVisitLocalVariableInfoElements(localVariableInfoElements);
        }
    }
}
