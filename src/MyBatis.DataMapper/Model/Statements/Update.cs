
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

using System;
using System.Data;
using System.Xml.Serialization;
using MyBatis.DataMapper.Model.ParameterMapping;
using MyBatis.DataMapper.Model.ResultMapping;
using MyBatis.DataMapper.Model.Cache;
using System.Diagnostics;
using MyBatis.DataMapper.Model.Sql.External;

namespace MyBatis.DataMapper.Model.Statements
{
	/// <summary>
	/// Summary description for Update.
	/// </summary>
	[Serializable]
    [DebuggerDisplay("Update: {Id}")]
	public class Update : Statement
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Update"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="parameterClass">The parameter class.</param>
        /// <param name="parameterMap">The parameter map.</param>
        /// <param name="remapResults">if set to <c>true</c> [remap results].</param>
        /// <param name="extends">The extends.</param>
        /// <param name="sqlSource">The SQL source.</param>
        /// <param name="preserveWhitespace">Preserve whitespace.</param>
        public Update(
            string id, 
            Type parameterClass,
            ParameterMap parameterMap,
            bool remapResults,
            string extends,
            ISqlSource sqlSource,
            bool preserveWhitespace)
            : base(id, parameterClass, parameterMap, null, new ResultMapCollection(), null, null, null, remapResults, extends, sqlSource, preserveWhitespace)
		{}

	}
}
