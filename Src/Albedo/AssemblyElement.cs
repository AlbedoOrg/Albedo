using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    /// <summary>
    /// An <see cref="IReflectionElement"/> representing an <see cref="Assembly"/> which
    /// can be visited by an <see cref="IReflectionVisitor{T}"/> implementation.
    /// </summary>
    public class AssemblyElement : IReflectionElement
    {
        public Assembly Assembly { get; private set; }

        public AssemblyElement(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            this.Assembly = assembly;
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

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Assembly.Equals(((AssemblyElement)obj).Assembly);
        }

        public override int GetHashCode()
        {
            return this.Assembly.GetHashCode();
        }

    }
}
