
#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 383115 $
 * $Date: 2008-03-16 02:10:31 -0600 (Sun, 16 Mar 2008) $
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

using Apache.Ibatis.DataMapper.Configuration.Sql.Dynamic.Handlers;
#endregion


namespace Apache.Ibatis.DataMapper.Configuration.Sql.Dynamic.Elements
{
	/// <summary>
	/// Summary description for Conditional.
	/// </summary>
	[Serializable]
	public abstract class Conditional : BaseTag
	{

		#region Fields
		
		[NonSerialized]
		private string _compareValue = string.Empty;
		[NonSerialized]
		private string _compareProperty = string.Empty;

		#endregion

		/// <summary>
		/// CompareProperty attribute
		/// </summary>
		[XmlAttribute("compareProperty")]
		public string CompareProperty
		{
			get
			{
				return _compareProperty;
			}
			set
			{
				_compareProperty = value;
			}
		}


		/// <summary>
		/// CompareValue attribute
		/// </summary>
		[XmlAttribute("compareValue")]
		public string CompareValue
		{
			get
			{
				return _compareValue;
			}
			set
			{
				_compareValue = value;
			}
		}
	}
}
