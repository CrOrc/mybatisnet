#region Apache Notice
/*****************************************************************************
 * $Revision: 408099 $
 * $LastChangedDate: 2008-06-28 09:26:16 -0600 (Sat, 28 Jun 2008) $
 * $LastChangedBy: gbayon $
 * 
 * MyBatis.NET Data Mapper
 * Copyright (C) 2008/2005 - The Apache Software Foundation
 *  
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *      http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 ********************************************************************************/
#endregion

using System;
using System.IO;

namespace MyBatis.Common.Resources
{
    /// <summary>
    /// Convenience base class for <see cref="IResource"/>
    /// implementations, pre-implementing typical behavior.
    /// </summary>
    public abstract class AbstractResource : IResource
    {
        /// <summary>
        /// The default special character that denotes the base (home, or root)
        /// path.
        /// </summary>
        protected const string DefaultBasePathPlaceHolder = "~";

        protected Stream stream = null;

        #region IResource Members

        /// <summary>
        /// Returns the <see cref="System.Uri"/> handle for this resource.
        /// </summary>
        /// <value></value>
        public abstract Uri Uri { get; }

        /// <summary>
        /// Returns a <see cref="System.IO.FileInfo"/> handle for this resource.
        /// </summary>
        /// <value>The <see cref="System.IO.FileInfo"/> handle for this resource.</value>
        /// <remarks>
        /// 	<p>
        /// For safety, always check the value of the
        /// <see cref="System.Uri.IsFile "/> property prior to
        /// accessing this property; resources that cannot be exposed as
        /// a <see cref="System.IO.FileInfo"/> will typically return
        /// <see langword="false"/> from a call to this property.
        /// </p>
        /// </remarks>
        /// <exception cref="System.IO.IOException">
        /// If the resource is not available on a filesystem, or cannot be
        /// exposed as a <see cref="System.IO.FileInfo"/> handle.
        /// </exception>
        public abstract FileInfo FileInfo { get; }

        /// <summary>
        /// Return an <see cref="System.IO.Stream"/> for this resource.
        /// </summary>
        /// <value>An <see cref="System.IO.Stream"/>.</value>
        /// <remarks>
        /// 	<note type="caution">
        /// Clients of this interface must be aware that every access of this
        /// property will create a <i>fresh</i>
        /// 		<see cref="System.IO.Stream"/>;
        /// it is the responsibility of the calling code to close any such
        /// <see cref="System.IO.Stream"/>.
        /// </note>
        /// </remarks>
        /// <exception cref="System.IO.IOException">
        /// If the stream could not be opened.
        /// </exception>
        public abstract Stream Stream { get; }

        /// <summary>
        /// Returns a description for this resource.
        /// </summary>
        /// <value>A description for this resource.</value>
        /// <remarks>
        /// 	<p>
        /// The description is typically used for diagnostics and other such
        /// logging when working with the resource.
        /// </p>
        /// 	<p>
        /// Implementations are also encouraged to return this value from their
        /// <see cref="System.Object.ToString()"/> method.
        /// </p>
        /// </remarks>
        public abstract string Description { get; }


        /// <summary>
        /// Creates a resource relative to this resource.
        /// </summary>
        /// <param name="relativePath">The path (always resolved as relative to this resource).</param>
        /// <returns>The relative resource.</returns>
        /// <exception cref="System.IO.IOException">
        /// If the relative resource could not be created from the supplied
        /// path.
        /// </exception>
        /// <exception cref="System.NotSupportedException">
        /// If the resource does not support the notion of a relative path.
        /// </exception>
        public abstract IResource CreateRelative(String relativePath);

        #endregion

        /// <summary>
        /// Strips any protocol name from the supplied
        /// <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">An Uri resource.</param>
        /// <returns>
        /// The name of the resource without the protocol name.
        /// </returns>
        /// <remarks>
        /// 	<p>
        /// If the supplied <paramref name="uri"/> does not
        /// have any protocol associated with it, then the supplied
        /// <paramref name="uri"/> will be returned as-is.
        /// </p>
        /// </remarks>
        /// <example>
        /// 	<code language="C#">
        /// GetResourceNameWithoutProtocol("http://www.mycompany.com/resource.txt");
        /// // returns www.mycompany.com/resource.txt
        /// </code>
        /// </example>
        protected virtual string GetResourceNameWithoutProtocol(Uri uri)
        {
            int index = uri.Scheme.Length + Uri.SchemeDelimiter.Length;

            if (index == -1)
            {
                return uri.AbsoluteUri;
            }
            else
            {
                return uri.AbsoluteUri.Substring(index); 
            }
        }

        /// <summary>
        /// This implementation returns the
        /// <see cref="Description"/> of this resource.
        /// </summary>
        /// <seealso cref="IResource.Description"/>
        public override string ToString()
        {
            return Description;
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            if (stream != null)
            {
                stream.Dispose();
            }
        }

        #endregion

    }
}
