using System;
using System.Reflection;

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
        /// Determines whether the specified <see cref="Object" />, is equal to
        /// this instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object" /> to compare with this
        /// instance.</param>
        /// <returns>
        /// <see langword="true" /> if the specified <see cref="Object" /> is
        /// equal to this instance; otherwise, <see langword="false" />.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Two instances of <see cref="AssemblyElement" /> are considered to
        /// be equal if their <see cref="Assembly" /> values are equal.
        /// </para>
        /// </remarks>
        public override bool Equals(object obj)
        {
            var other = obj as AssemblyElement;
            if (other == null)
                return false;

            return object.Equals(this.Assembly, other.Assembly);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing
        /// algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Assembly.GetHashCode();
        }
    }
}
