﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ploeh.Albedo
{
    /// <summary>
    /// Creates <see cref="ParameterInfoElement" /> instances from a sequence of
    /// source objects.
    /// </summary>
    /// <typeparam name="T">The type of source objects.</typeparam>
    /// <seealso cref="Materialize(IEnumerable{T})" />
    public class ParameterInfoElementMaterializer<T> : IReflectionElementMaterializer<T>
    {
        /// <summary>
        /// Creates <see cref="ParameterInfoElement" /> instances from a sequence
        /// of source objects.
        /// </summary>
        /// <param name="source">The source objects.</param>
        /// <returns>
        /// A sequence of <see cref="ParameterInfoElement" /> instances created
        /// from <paramref name="source" />.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source" /> is null.
        /// </exception>
        /// <remarks>
        /// <para>
        /// This method creates <see cref="ParameterInfoElement" /> instances from
        /// all matching elements in <paramref name="source" />. An element is
        /// matching if it's an <see cref="ParameterInfo" /> instance, in which
        /// case a corresponding <strong>ParameterInfoElement</strong> is created
        /// and returned.
        /// </para>
        /// </remarks>
        /// <seealso cref="IReflectionElementMaterializer{T}" />
        public IEnumerable<IReflectionElement> Materialize(IEnumerable<T> source)
        {
            return source
                .OfType<ParameterInfo>()
                .Select(mi => new ParameterInfoElement(mi))
                .Cast<IReflectionElement>();
        }
    }
}