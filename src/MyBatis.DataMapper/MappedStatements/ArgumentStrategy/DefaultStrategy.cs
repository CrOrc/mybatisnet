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

using System;
using System.Data;
using MyBatis.DataMapper.Model.ResultMapping;
using MyBatis.DataMapper.Scope;
using MyBatis.DataMapper.TypeHandlers;

namespace MyBatis.DataMapper.MappedStatements.ArgumentStrategy
{
	/// <summary>
	/// <see cref="IArgumentStrategy"/> implementation when no 'select' or
	/// 'resultMapping' attribute exists on a <see cref="ResultProperty"/>.
	/// </summary>
	public sealed class DefaultStrategy : IArgumentStrategy
	{
		#region IArgumentStrategy Members

        /// <summary>
        /// Gets the value of an argument constructor.
        /// </summary>
        /// <param name="request">The current <see cref="RequestScope"/>.</param>
        /// <param name="mapping">The <see cref="ResultProperty"/> with the argument infos.</param>
        /// <param name="reader">The current <see cref="IDataReader"/>.</param>
        /// <param name="keys">The keys</param>
        /// <returns>The paremeter value.</returns>
		public object GetValue(RequestScope request, ResultProperty mapping, 
		                       ref IDataReader reader, object keys)
		{
			if (mapping.TypeHandler == null || 
				mapping.TypeHandler is UnknownTypeHandler) // Find the TypeHandler
			{
				lock(mapping) 
				{
					if (mapping.TypeHandler == null || mapping.TypeHandler is UnknownTypeHandler)
					{
						int columnIndex = 0;
						if (mapping.ColumnIndex == ResultProperty.UNKNOWN_COLUMN_INDEX) 
						{
							columnIndex = reader.GetOrdinal(mapping.ColumnName);
						} 
						else 
						{
							columnIndex = mapping.ColumnIndex;
						}
						Type systemType =reader.GetFieldType(columnIndex);

						mapping.TypeHandler = request.DataExchangeFactory.TypeHandlerFactory.GetTypeHandler(systemType);
					}
				}					
			}

			object dataBaseValue = mapping.GetDataBaseValue( reader );
			request.IsRowDataFound = request.IsRowDataFound || (dataBaseValue != null);

			return dataBaseValue;
		}

		#endregion
	}
}
