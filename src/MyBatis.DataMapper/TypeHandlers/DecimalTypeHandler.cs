
#region Apache Notice
/*****************************************************************************
 * $Revision: 476843 $
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
using System.Globalization;

using MyBatis.DataMapper.Model.ResultMapping;

namespace MyBatis.DataMapper.TypeHandlers
{
	/// <summary>
	/// DecimalTypeHandler.
	/// </summary>
    public sealed class DecimalTypeHandler : BaseTypeHandler
	{
        /// <summary>
		/// 
		/// </summary>
		/// <param name="mapping"></param>
		/// <param name="dataReader"></param>
		/// <returns></returns>
		public override object GetValueByName(ResultProperty mapping, IDataReader dataReader)
        {
            int index = dataReader.GetOrdinal(mapping.ColumnName);

            return dataReader.IsDBNull(index) ? (object) DBNull.Value : dataReader.GetDecimal(index);
        }

	    /// <summary>
        /// Gets a column value by the index
        /// </summary>
        /// <param name="mapping"></param>
        /// <param name="dataReader"></param>
        /// <returns></returns>
		public override object GetValueByIndex(ResultProperty mapping, IDataReader dataReader)
	    {
	        if (dataReader.IsDBNull(mapping.ColumnIndex))
			{
				return DBNull.Value;
			}
	        return dataReader.GetDecimal(mapping.ColumnIndex);
	    }

	    /// <summary>
        /// Converts the String to the type that this handler deals with
        /// </summary>
        /// <param name="type">the tyepe of the property (used only for enum conversion)</param>
        /// <param name="s">the String value</param>
        /// <returns>the converted value</returns>
        /// <remarks>
        /// value decimal must be  in format ######.##
        /// where . is separator for decimal
        /// </remarks> 
		public override object ValueOf(Type type, string s)
		{
			CultureInfo culture = new CultureInfo( "en-US" );
			return decimal.Parse(s, culture);
		}

        /// <summary>
        /// Retrieve ouput database value of an output parameter
        /// </summary>
        /// <param name="outputValue">ouput database value</param>
        /// <param name="parameterType">type used in EnumTypeHandler</param>
        /// <returns></returns>
		public override object GetDataBaseValue(object outputValue, Type parameterType )
		{
			return Convert.ToDecimal(outputValue);
		}


        /// <summary>
        /// Gets a value indicating whether this instance is simple type.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is simple type; otherwise, <c>false</c>.
        /// </value>
		public override bool IsSimpleType
		{
			get { return true; }
		}

        //public override object NullValue
        //{
        //    get { throw new InvalidCastException("DecimalTypeHandler could not cast a null value in decimal field."); }
        //}
	}
}
