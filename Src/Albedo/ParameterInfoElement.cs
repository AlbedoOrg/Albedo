using System;
using System.Reflection;

namespace Ploeh.Albedo
{
    /// <summary>
    /// An <see cref="IReflectionElement"/> representing a <see cref="ParameterInfo"/> which
    /// can be visited by an <see cref="IReflectionVisitor{T}"/> implementation.
    /// </summary>
    public class ParameterInfoElement : IReflectionElement
    {
        /// <summary>
        /// Gets the <see cref="System.Reflection.ParameterInfo"/> instance this element points to.
        /// </summary>
        public ParameterInfo ParameterInfo { get; private set; }

        /// <summary>
        /// Constructs a new instance of the <see cref="ParameterInfoElement"/> which represents
        /// the specified <see cref="System.Reflection.ParameterInfo"/>.
        /// </summary>
        /// <param name="parameterInfo">The <see cref="System.Reflection.ParameterInfo"/> this 
        /// element represents.</param>
        public ParameterInfoElement(ParameterInfo parameterInfo)
        {
            if (parameterInfo == null) throw new ArgumentNullException("parameterInfo");
            ParameterInfo = parameterInfo;
        }

        /// <summary>
        /// Accepts the provided <see cref="IReflectionVisitor{T}"/>, by calling the
        /// appropriate strongly-typed <see cref="IReflectionVisitor{T}.Visit(ParameterInfoElement)"/>
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

            return this.ParameterInfo.Equals(((ParameterInfoElement)obj).ParameterInfo);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary> 
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.ParameterInfo.GetHashCode();
        }

    }
}