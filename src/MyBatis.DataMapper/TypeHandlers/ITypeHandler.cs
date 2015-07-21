
#region Apache Notice
/*****************************************************************************
 * $Revision: 389819 $
 * $LastChangedDate: 2008-09-21 13:25:16 -0600 (Sun, 21 Sep 2008) $
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
using System.Data;
using MyBatis.DataMapper.Model.ResultMapping;

namespace MyBatis.DataMapper.TypeHandlers
{
	/// <summary>
	/// Summary description for ITypeHandler.
	/// </summary>
	public interface ITypeHandler
	{
        /// <summary>
        /// Gets a value indicating whether this instance is simple type.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is simple type; otherwise, <c>false</c>.
        /// </value>
		bool IsSimpleType{get;}

		/// <summary>
		/// Gets a column value by the name
		/// </summary>
		/// <param name="mapping"></param>
		/// <param name="dataReader"></param>
		/// <returns></returns>
		object GetValueByName(ResultProperty mapping, IDataReader dataReader);

		/// <summary>
		/// Gets a column value by the index
		/// </summary>
		/// <param name="mapping"></param>
		/// <param name="dataReader"></param>
		/// <returns></returns>
		object GetValueByIndex(ResultProperty mapping, IDataReader dataReader);

		/// <summary>
		/// Retrieve ouput database value of an output parameter
		/// </summary>
		/// <param name="outputValue">ouput database value</param>
		/// <param name="parameterType">type used in EnumTypeHandler</param>
		/// <returns></returns>
		object GetDataBaseValue(object outputValue, Type parameterType);

		/// <summary>
		///  Sets a parameter on a IDbCommand
		/// </summary>
		/// <param name="dataParameter">the parameter</param>
		/// <param name="parameterValue">the parameter value</param>
		/// <param name="dbType">the dbType of the parameter</param>
		void SetParameter(IDataParameter dataParameter, object parameterValue, string dbType);

		/// <summary>
		/// Converts the String to the type that this handler deals with
		/// </summary>
		/// <param name="type">the tyepe of the property (used only for enum conversion)</param>
		/// <param name="s">the String value</param>
		/// <returns>the converted value</returns>
		object ValueOf(Type type, string s);

		/// <summary>
		///  Compares two values (that this handler deals with) for equality
		/// </summary>
		/// <param name="obj">one of the objects</param>
		/// <param name="str">the other object as a String</param>
		/// <returns>true if they are equal</returns>
		 bool Equals(object obj, string str);

        /// <summary>
        /// The null value for this type
        /// </summary>
        object NullValue { get;}
	}
}
