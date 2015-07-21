
#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 383115 $
 * $Date: 2009-06-28 10:11:37 -0600 (Sun, 28 Jun 2009) $
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

#region Imports
using System;
using System.Data;
using System.Xml.Serialization;
using MyBatis.DataMapper.Model.ParameterMapping;
using MyBatis.DataMapper.Model.ResultMapping;
using MyBatis.DataMapper.Model.Cache;
using System.Diagnostics;
using MyBatis.DataMapper.Model.Sql.External;
using MyBatis.Common.Utilities.Objects;

#endregion


namespace MyBatis.DataMapper.Model.Statements
{
	/// <summary>
	/// Summary description for delete.
	/// </summary>
	[Serializable]
    [DebuggerDisplay("Delete: {Id}")]
	public class Delete : Statement
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Delete"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="parameterClass">The parameter class.</param>
        /// <param name="parameterMap">The parameter map.</param>
        /// <param name="resultClass">The result class.</param>
        /// <param name="resultMaps">The result maps.</param>
        /// <param name="listClass">The list class.</param>
        /// <param name="listClassFactory">The list class factory.</param>
        /// <param name="cacheModel">The cache model.</param>
        /// <param name="remapResults">if set to <c>true</c> [remap results].</param>
        /// <param name="extends">The extends.</param>
        /// <param name="sqlSource">The SQL source.</param>
        /// <param name="preserveWhitespace">Preserve whitespace.</param>
        public Delete(
            string id, 
            Type parameterClass,
            ParameterMap parameterMap,
            Type resultClass,
            ResultMapCollection resultMaps,
            Type listClass,
            IFactory listClassFactory,
            CacheModel cacheModel,
            bool remapResults,
            string extends,
            ISqlSource sqlSource,
            bool preserveWhitespace
            )
            : base(id, parameterClass, parameterMap, resultClass, resultMaps, listClass, listClassFactory, cacheModel, remapResults, extends, sqlSource, preserveWhitespace)
		{}

	}
}
