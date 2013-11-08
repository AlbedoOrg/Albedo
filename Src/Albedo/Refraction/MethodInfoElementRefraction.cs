﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo.Refraction
{
    /// <summary>
    /// Creates <see cref="MethodInfoElement" /> instances from a sequence of
    /// source objects.
    /// </summary>
    /// <typeparam name="T">The type of source objects.</typeparam>
    /// <seealso cref="Refract(IEnumerable{T})" />
    public class MethodInfoElementRefraction<T> : IReflectionElementRefraction<T>
    {
        /// <summary>
        /// Creates <see cref="MethodInfoElement" /> instances from a sequence
        /// of source objects.
        /// </summary>
        /// <param name="source">The source objects.</param>
        /// <returns>
        /// A sequence of <see cref="MethodInfoElement" /> instances created
        /// from <paramref name="source" />.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source" /> is null.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method creates <see cref="MethodInfoElement" /> instances from
        /// all matching elements in <paramref name="source" />. An element is
        /// matching if it's an <see cref="MethodInfo" /> instance, in which
        /// case a corresponding <strong>MethodInfoElement</strong> is created
        /// and returned.
        /// </para>
        /// </remarks>
        /// <seealso cref="IReflectionElementRefraction{T}" />
        public IEnumerable<IReflectionElement> Refract(IEnumerable<T> source)
        {
            return source
                .OfType<MethodInfo>()
                .Select(mi => new MethodInfoElement(mi))
                .Cast<IReflectionElement>();
        }
    }
}