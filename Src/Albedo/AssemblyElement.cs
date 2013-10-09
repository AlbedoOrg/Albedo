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
        /// <summary>
        /// Gets the <see cref="System.Reflection.Assembly"/> instance this element points to.
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Constructs a new instance of the <see cref="AssemblyElement"/> which represents
        /// the specified <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> this element represents.</param>
        public AssemblyElement(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            this.Assembly = assembly;
        }

        /// <summary>
        /// Accepts the provided <see cref="IReflectionVisitor{T}"/>, by calling the
        /// appropriate strongly-typed <see cref="IReflectionVisitor{T}.Visit(Ploeh.Albedo.AssemblyElement)"/>
        /// method on the visitor.
        /// </summary>
        /// <typeparam name="T">The type of observation or result which the
        /// <see cref="IReflectionVisitor{T}"/> instance produces when visiting nodes.</typeparam>
        /// <param name="visitor">The <see cref="IReflectionVisitor{T}"/> instance.</param>
        /// <returns>A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.</returns>
        public IReflectionVisitor<T> Accept<T>(IReflectionVisitor<T> visitor)
        {
            if (visitor == null) throw new ArgumentNullException("visitor");
            return visitor.Visit(this);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current
        /// <see cref="T:System.Object"/>. 
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current 
        /// <see cref="T:System.Object"/>; otherwise, false. 
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current
        /// <see cref="T:System.Object"/>.
        /// </param>
        /// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/>
        /// parameter is null.</exception>
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

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary> 
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Assembly.GetHashCode();
        }

    }
}
