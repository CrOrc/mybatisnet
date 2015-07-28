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
    /// Url resource loader implementation of the
    /// <see cref="MyBatis.Common.Resources.IResourceLoader"/> interface.
    /// </summary>
    /// <remarks>
    /// <p>
    /// This <see cref="MyBatis.Common.Resources.IResourceLoader"/> implementation
    /// allows the creation of embeded assembly resource.
    /// </p>
    /// <remarks>
    /// <p>
    /// Obviously supports resolution as a <see cref="System.Uri"/>, and also
    /// as a <see cref="System.IO.FileInfo"/> in the case of the <c>"file:"</c>
    /// protocol.
    /// </p>
    /// </remarks>
    /// <example>
    /// <p>
    /// Some examples of the strings that can be used to initialize a new
    /// instance of the <see cref="MyBatis.Common.Resources.UrlResource"/> class
    /// include...
    /// <list type="bullet">
    /// <item>
    /// <description>ftp://Config/SqlMap.config</description>
    /// </item>
    /// <item>
    /// <description>http://www.mycompany.com/SqlMap.config</description>
    /// </item>
    /// </list>
    /// </p>
    /// </example>
    /// </remarks>
    public class UrlResourceLoader : IResourceLoader
    {

        /// <summary>
        /// The resource is accessed through FTP. 
        /// </summary>
        public const string SchemeFtp = "ftp";
        /// <summary>
        /// The resource is accessed through HTTP. 
        /// </summary>
        public const string SchemeHttp = "http";
        /// <summary>
        /// The resource is accessed through SSL-encrypted HTTP. 
        /// </summary>
        public const string SchemeHttps = "https";

        #region IResourceLoader Members

        /// <summary>
        /// Check if this loader accepts the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>True or false</returns>
        public bool Accept(Uri uri)
        {
            return (SchemeFtp.Equals(uri.Scheme) ||
                SchemeHttp.Equals(uri.Scheme) ||
                SchemeHttps.Equals(uri.Scheme));
        }

        /// <summary>
        /// Return an <see cref="MyBatis.Common.Resources.IResource"/> handle for the
        /// specified URI.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns>
        /// An appropriate <see cref="MyBatis.Common.Resources.IResource"/> handle.
        /// </returns>
        public IResource Create(Uri resource)
        {
            if (Accept(resource))
           {
               return new UrlResource(resource);
           }
           else
           {
               throw new ResourceException(string.Format( 
                    "{0} cannot be resolved - " +
                    "resource does not use '{1}:', '{2}:', '{3}:'  protocols.", resource, SchemeFtp, SchemeHttp, SchemeHttps));
           }
        }


        #endregion
    }
}
