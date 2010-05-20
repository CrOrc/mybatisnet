
#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: $
 * $Date: $
 * Author : Gilles Bayon
 * iBATIS.NET Data Mapper
 * Copyright (C) 2004 - Apache Fondation
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

#region Using

using System;
using System.Xml.Serialization;

#endregion

namespace IBatisNet.DataMapper.Configuration.ResultMapping
{
	/// <summary>
	/// Summary description for SubMap.
	/// </summary>
	[Serializable]
	[XmlRoot("subMap")]
	public class SubMap
	{
		// <resultMap id="document" class="Document">
		//			<result property="Id" column="Document_ID"/>
		//			<result property="Title" column="Document_Title"/>
		//			<discriminator column="Document_Type" [formula="CustomFormula, AssemblyName"] /> 
		//						-- attribute column (not used if discriminator use a custom formula)
		//						-- attribute formula (not required will used the DefaultFormula) calculate the discriminator value (DefaultFormula is default), else used an aliasType wich implement IDiscriminatorFormula), 
		//			<subMap value="Book" -- discriminator value
		//					resultMapping="book" />
		//	</resultMap>
		//
		//  <resultMap 
		//		id="book"  
		//		class="Book"
		//		extend="document">
		//  ...
		// </resultMap>

		#region Fields
		[NonSerialized]
		private string _discriminatorValue = string.Empty;
		[NonSerialized]
		private string _resultMapName = string.Empty;
		[NonSerialized]
		private ResultMap _resultMap = null;
		#endregion 

		#region Properties

		/// <summary>
		/// Discriminator value
		/// </summary>
		[XmlAttribute("value")]
		public string DiscriminatorValue
		{
			get { return _discriminatorValue; }
			set { _discriminatorValue = value; }
		}

		/// <summary>
		/// The name of the ResultMap used if the column value is = to the Discriminator Value
		/// </summary>
		[XmlAttribute("resultMapping")]
		public string ResultMapName
		{
			get { return _resultMapName; }
			set { _resultMapName = value; }
		}

		/// <summary>
		/// The resultMap used if the column value is = to the Discriminator Value
		/// </summary>
		[XmlIgnore]
		public ResultMap ResultMap
		{
			get { return _resultMap; }
			set { _resultMap = value; }
		}

		#endregion 

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public SubMap()
		{
		}
		#endregion 

	}
}
