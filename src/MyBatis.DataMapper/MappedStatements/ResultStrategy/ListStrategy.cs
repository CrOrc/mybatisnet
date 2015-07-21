#region Apache Notice
/*****************************************************************************
 * $Revision: 374175 $
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

using System.Collections;
using System.Data;
using MyBatis.DataMapper.Model.ResultMapping;
using MyBatis.DataMapper.Scope;
using MyBatis.DataMapper.TypeHandlers;

namespace MyBatis.DataMapper.MappedStatements.ResultStrategy
{
	/// <summary>
	/// <see cref="IResultStrategy"/> implementation when 
	/// a 'resultClass' attribute is specified and
	/// the type of the result object is <see cref="IList"/>.
	/// </summary>
    public sealed class ListStrategy : IResultStrategy
    {
        #region IResultStrategy Members

        /// <summary>
        /// Processes the specified <see cref="IDataReader"/> 
        /// when a ResultClass is specified on the statement and
        /// the ResultClass is <see cref="IList"/>.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="resultObject">The result object.</param>
        public object Process(RequestScope request, ref IDataReader reader, object resultObject)
        {
			object outObject = resultObject;
            //AutoResultMap resultMap = request.CurrentResultMap as AutoResultMap;

			if (outObject == null) 
			{
                outObject = request.CurrentResultMap.CreateInstanceOfResult(null);
			}

			int count = reader.FieldCount;
			for (int i = 0; i < count; i++) 
			{
				const string propertyName = "value";
                int columnIndex = i;
                ITypeHandler typeHandler = request.DataExchangeFactory.TypeHandlerFactory.GetTypeHandler(reader.GetFieldType(i));
                ResultProperty property = new ResultProperty(
                    propertyName,
                    string.Empty,
                    columnIndex,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    false,
                    string.Empty,
                    null,
                    string.Empty,
                    null,
                    null,
                    typeHandler);

				((IList) outObject).Add(property.GetDataBaseValue(reader));
			}    
    
			return outObject;
		}

        #endregion
    }
}
