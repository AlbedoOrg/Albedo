using System;
using System.Linq;

namespace Ploeh.Albedo
{
    /// <summary>
    /// An <see cref="IReflectionElement"/> representing a <see cref="System.Type"/> which
    /// can be visited by an <see cref="IReflectionVisitor{T}"/> implementation.
    /// </summary>
    public class TypeElement : IReflectionElement
    {
        /// <summary>
        /// Gets the <see cref="System.Type"/> instance this element represents.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods",
            Justification = "The name follows a consistent pattern with the other reflection elements, and most users will understand the distiction between 'GetType()' and 'Type'.")]
        public Type Type { get; private set; }

        /// <summary>
        /// Constructs a new instance of the <see cref="TypeElement"/> which represents
        /// the specified <see cref="System.Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="System.Type"/> this element represents.</param>
        public TypeElement(Type type)
        {
            if (type == null) throw new ArgumentNullException("type");
            this.Type = type;
        }

        /// <summary>
        /// Accepts the provided <see cref="IReflectionVisitor{T}"/>, by calling the
        /// appropriate strongly-typed <see cref="IReflectionVisitor{T}.Visit(TypeElement)"/>
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

        public override bool Equals(object obj)
        {
            var other = obj as TypeElement;
            if (other == null)
                return false;

            return object.Equals(this.Type, other.Type);
        }

        public override int GetHashCode()
        {
            return this.Type.GetHashCode();
        }
    }
}
