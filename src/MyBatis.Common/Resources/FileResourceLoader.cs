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
using MyBatis.Common.Exceptions;

namespace MyBatis.Common.Resources
{
    /// <summary>
    /// File resource loader implementation of the
    /// <see cref="IResourceLoader"/> interface.
    /// </summary>
    /// <remarks>
    /// <p>
    /// This <see cref="IResourceLoader"/> implementation
    /// allows the creation of file resource.
    /// </p>
    /// <p>
    /// URI must be format as file://'filename.text' ...
    /// </p>
    /// </remarks>
    public class FileResourceLoader : IResourceLoader
    {

        /// <summary>
        /// The resource is a file on the local computer. 
        /// </summary>
        public const string Scheme = "file";

        #region IResourceLoader Members

        /// <summary>
        /// Check if this loader accepts the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>True or false</returns>
        public bool Accept(Uri uri)
        {
            return Scheme.Equals(uri.Scheme);
        }

        /// <summary>
        /// Return an <see cref="IResource"/> handle for the
        /// specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>
        /// An appropriate <see cref="IResource"/> handle.
        /// </returns>
        public IResource Create(Uri uri)
        {
            if (Accept(uri) && uri.IsFile)
            {
                return new FileResource(uri);
            }
            else
            {
                throw new ResourceException(string.Format(
                     "{0} cannot be resolved - " +
                     "resource does not use '{1}:' protocol.", uri.AbsoluteUri, Scheme));
            }
        }

        #endregion
    }
}
