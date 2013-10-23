using System;

namespace Ploeh.Albedo.UnitTests
{
    internal class DelegatingReflectionVisitor<T> : IReflectionVisitor<T>
    {
        public T Value { get; set; }

        public DelegatingReflectionVisitor()
        {
            this.OnAssemblyElementVisited = e => { };
            this.OnTypeElementVisited = e => { };
            this.OnParameterInfoElementVisited = e => { };
            this.OnPropertyInfoElementVisited = e => { };
            this.OnFieldInfoElementVisited = e => { };
            this.OnConstructorInfoElementVisited = e => { };
            this.OnMethodInfoElementVisited = e => { };

            this.OnVisitAssemblyElement = (element, visitor) => visitor;
            this.OnVisitTypeElement = (element, visitor) => visitor;
            this.OnVisitParameterInfoElement = (element, visitor) => visitor;
            this.OnVisitPropertyInfoElement = (element, visitor) => visitor;
            this.OnVisitFieldInfoElement = (element, visitor) => visitor;
            this.OnVisitConstructorInfoElement = (element, visitor) => visitor;
            this.OnVisitMethodInfoElement = (element, visitor) => visitor;
        }

        public Action<AssemblyElement> OnAssemblyElementVisited { get; set; }
        public Action<TypeElement> OnTypeElementVisited { get; set; }
        public Action<ParameterInfoElement> OnParameterInfoElementVisited { get; set; }
        public Action<PropertyInfoElement> OnPropertyInfoElementVisited { get; set; }
        public Action<FieldInfoElement> OnFieldInfoElementVisited { get; set; }
        public Action<ConstructorInfoElement> OnConstructorInfoElementVisited { get; set; }
        public Action<MethodInfoElement> OnMethodInfoElementVisited { get; set; }

        public Func<AssemblyElement, IReflectionVisitor<T>, IReflectionVisitor<T>> OnVisitAssemblyElement { get; set; }
        public Func<TypeElement, IReflectionVisitor<T>, IReflectionVisitor<T>> OnVisitTypeElement { get; set; }
        public Func<ParameterInfoElement, IReflectionVisitor<T>, IReflectionVisitor<T>> OnVisitParameterInfoElement { get; set; }
        public Func<PropertyInfoElement, IReflectionVisitor<T>, IReflectionVisitor<T>> OnVisitPropertyInfoElement { get; set; }
        public Func<FieldInfoElement, IReflectionVisitor<T>, IReflectionVisitor<T>> OnVisitFieldInfoElement { get; set; }
        public Func<ConstructorInfoElement, IReflectionVisitor<T>, IReflectionVisitor<T>> OnVisitConstructorInfoElement { get; set; }
        public Func<MethodInfoElement, IReflectionVisitor<T>, IReflectionVisitor<T>> OnVisitMethodInfoElement { get; set; }

        public virtual IReflectionVisitor<T> Visit(AssemblyElement assemblyElement)
        {
            try
            {
                return OnVisitAssemblyElement(assemblyElement, this);
            }
            finally
            {
                OnAssemblyElementVisited(assemblyElement);                
            }
        }

        public virtual IReflectionVisitor<T> Visit(TypeElement typeElement)
        {
            try
            {
                return OnVisitTypeElement(typeElement, this);
            }
            finally
            {
                OnTypeElementVisited(typeElement);
            }
        }

        public virtual IReflectionVisitor<T> Visit(ParameterInfoElement parameterInfoElement)
        {
            try
            {
                return OnVisitParameterInfoElement(parameterInfoElement, this);
            }
            finally
            {
                OnParameterInfoElementVisited(parameterInfoElement);
            }
        }

        public virtual IReflectionVisitor<T> Visit(PropertyInfoElement propertyInfoElement)
        {
            try
            {
                return OnVisitPropertyInfoElement(propertyInfoElement, this);
            }
            finally
            {
                OnPropertyInfoElementVisited(propertyInfoElement);
            }
        }

        public virtual IReflectionVisitor<T> Visit(FieldInfoElement fieldInfoElement)
        {
            try
            {
                return OnVisitFieldInfoElement(fieldInfoElement, this);
            }
            finally
            {
                OnFieldInfoElementVisited(fieldInfoElement);
            }
        }

        public virtual IReflectionVisitor<T> Visit(ConstructorInfoElement constructorInfoElement)
        {
            try
            {
                return OnVisitConstructorInfoElement(constructorInfoElement, this);
            }
            finally
            {
                OnConstructorInfoElementVisited(constructorInfoElement);
            }
        }

        public virtual IReflectionVisitor<T> Visit(MethodInfoElement methodInfoElement)
        {
            try
            {
                return OnVisitMethodInfoElement(methodInfoElement, this);
            }
            finally
            {
                OnMethodInfoElementVisited(methodInfoElement);
            }
        }
    }
}