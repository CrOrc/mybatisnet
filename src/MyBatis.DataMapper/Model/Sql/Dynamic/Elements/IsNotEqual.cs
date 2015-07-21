#region Apache Notice
/*****************************************************************************
 * $Revision: 408164 $
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

#region Imports
using System;
using System.Xml.Serialization;
using MyBatis.DataMapper.Model.Sql.Dynamic.Handlers;
using MyBatis.Common.Utilities.Objects.Members;

#endregion

namespace MyBatis.DataMapper.Model.Sql.Dynamic.Elements
{
	/// <summary>
	/// Represent an isNotEqual sql tag element.
	/// </summary>
	[Serializable]
	[XmlRoot("isNotEqual", Namespace="http://ibatis.apache.org/mapping")]
	public sealed class IsNotEqual: Conditional
	{

        /// <summary>
        /// Initializes a new instance of the <see cref="IsNotEqual"/> class.
        /// </summary>
        /// <param name="accessorFactory">The accessor factory.</param>
        public IsNotEqual(AccessorFactory accessorFactory)
		{
            this.Handler = new IsNotEqualTagHandler(accessorFactory);
		}
	}
}
