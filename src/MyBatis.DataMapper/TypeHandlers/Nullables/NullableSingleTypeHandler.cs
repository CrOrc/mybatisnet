#region Apache Notice
/*****************************************************************************
 * $Revision: 378879 $
 * $LastChangedDate: 2008-10-11 10:07:44 -0600 (Sat, 11 Oct 2008) $
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

namespace MyBatis.DataMapper.TypeHandlers.Nullables
{
    /// <summary>
    /// TypeHandler for Nullable Single type
    /// </summary>
    public sealed class NullableSingleTypeHandler : BaseTypeHandler
    {

        /// <summary>
        /// Sets a parameter on a IDbCommand
        /// </summary>
        /// <param name="dataParameter">the parameter</param>
        /// <param name="parameterValue">the parameter value</param>
        /// <param name="dbType">the dbType of the parameter</param>
        public override void SetParameter(IDataParameter dataParameter, object parameterValue, string dbType)
        {
            Single? nullableValue = (Single?)parameterValue;

            if (nullableValue.HasValue)
            {
                dataParameter.Value = nullableValue.Value;
            }
            else
            {
                dataParameter.Value = DBNull.Value;
            }
        }


        /// <summary>
        /// Gets a column value by the name
        /// </summary>
        /// <param name="mapping"></param>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public override object GetValueByName(ResultProperty mapping, IDataReader dataReader)
        {
            int index = dataReader.GetOrdinal(mapping.ColumnName);

            if (dataReader.IsDBNull(index))
            {
                return DBNull.Value;
            }
            return new Single?(dataReader.GetFloat(index));
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
            return new Single?(dataReader.GetFloat(mapping.ColumnIndex));
        }

        /// <summary>
        /// Retrieve ouput database value of an output parameter
        /// </summary>
        /// <param name="outputValue">ouput database value</param>
        /// <param name="parameterType">type used in EnumTypeHandler</param>
        /// <returns></returns>
        public override object GetDataBaseValue(object outputValue, Type parameterType)
        {
            return new Single?(Convert.ToSingle(outputValue));
        }

        /// <summary>
        /// Converts the String to the type that this handler deals with
        /// </summary>
        /// <param name="type">the tyepe of the property (used only for enum conversion)</param>
        /// <param name="s">the String value</param>
        /// <returns>the converted value</returns>
        public override object ValueOf(Type type, string s)
        {
            return new Single?(Convert.ToSingle(s));
        }


        /// <summary>
        /// Tell us if ot is a 'primitive' type
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        public override bool IsSimpleType
        {
            get { return true; }
        }


        /// <summary>
        /// The null value for this type
        /// </summary>
        /// <value></value>
        public override object NullValue
        {
            get { return new Single?(); }
        }
    }
}