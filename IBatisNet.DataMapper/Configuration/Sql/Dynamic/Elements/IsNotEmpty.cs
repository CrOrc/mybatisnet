
#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: $
 * $Date: $
 * 
 * iBATIS.NET Data Mapper
 * Copyright (C) 2004 - Gilles Bayon
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

using IBatisNet.DataMapper.Configuration.Sql.Dynamic.Handlers;
#endregion

namespace IBatisNet.DataMapper.Configuration.Sql.Dynamic.Elements
{
	/// <summary>
	/// Represent an isNotEmpty sql tag element.
	/// </summary>
	[Serializable]
	[XmlRoot("isNotEmpty", Namespace="http://ibatis.apache.org/mapping")]
	public class IsNotEmpty : BaseTag
	{
		#region  Constructor (s) / Destructor
		/// <summary>
		/// 
		/// </summary>
		public IsNotEmpty()
		{
			this.Handler = new IsNotEmptyTagHandler();
		}
		#endregion
	}
}
