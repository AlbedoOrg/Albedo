using System;
using System.Linq;
using System.Reflection;

namespace Ploeh.Albedo
{
    /// <summary>
    /// An <see cref="IReflectionElement"/> representing a <see cref="ConstructorInfo"/> which
    /// can be visited by an <see cref="IReflectionVisitor{T}"/> implementation.
    /// </summary>
    public class ConstructorInfoElement : IReflectionElement
    {
        public ConstructorInfo ConstructorInfo { get; private set; }

        public ConstructorInfoElement(ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null) throw new ArgumentNullException("constructorInfo");
            ConstructorInfo = constructorInfo;
        }

        public IReflectionVisitor<T> Accept<T>(IReflectionVisitor<T> visitor)
        {
            if (visitor == null) throw new ArgumentNullException("visitor");
            return visitor.Visit(this);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != typeof(ConstructorInfoElement))
            {
                return false;
            }

            return this.ConstructorInfo.Equals(((ConstructorInfoElement)obj).ConstructorInfo);
        }

        public override int GetHashCode()
        {
            return this.ConstructorInfo.GetHashCode();
        }

    }
}