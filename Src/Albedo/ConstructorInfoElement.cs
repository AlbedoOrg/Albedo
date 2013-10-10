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
        /// <summary>
        /// Gets the <see cref="System.Reflection.ConstructorInfo"/> instance this element points to.
        /// </summary>
        public ConstructorInfo ConstructorInfo { get; private set; }

        /// <summary>
        /// Constructs a new instance of the <see cref="ConstructorInfoElement"/> which represents
        /// the specified <see cref="System.Reflection.ConstructorInfo"/>.
        /// </summary>
        /// <param name="constructorInfo">The <see cref="System.Reflection.ConstructorInfo"/> this 
        /// element represents.</param>
        public ConstructorInfoElement(ConstructorInfo constructorInfo)
        {
            if (constructorInfo == null) throw new ArgumentNullException("constructorInfo");
            ConstructorInfo = constructorInfo;
        }

        /// <summary>
        /// Accepts the provided <see cref="IReflectionVisitor{T}"/>, by calling the
        /// appropriate strongly-typed <see cref="IReflectionVisitor{T}.Visit(ConstructorInfoElement)"/>
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

            if (obj.GetType() != typeof(ConstructorInfoElement))
            {
                return false;
            }

            return this.ConstructorInfo.Equals(((ConstructorInfoElement)obj).ConstructorInfo);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary> 
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.ConstructorInfo.GetHashCode();
        }

    }
}