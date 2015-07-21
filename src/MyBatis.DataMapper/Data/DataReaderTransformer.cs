#region Apache Notice
/*****************************************************************************
 * $Revision: 374175 $
 * $LastChangedDate: 2008-10-19 05:25:12 -0600 (Sun, 19 Oct 2008) $
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

using System.Data;
using MyBatis.Common.Data;

namespace MyBatis.DataMapper.Data
{
	/// <summary>
	/// For <see cref="IDataReader"/> which don't support M.A.R.S, wraps the current <see cref="IDataReader"/>
	/// in an <see cref="InMemoryDataReader"/>.
	/// </summary>
	public sealed class DataReaderTransformer
	{

		/// <summary>
		///  Creates a DataReaderAdapter from a <see cref="IDataReader" />
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader" /> which holds the records from the Database.</param>
		/// <param name="dbProvider">The databse provider <see cref="IDbProvider"/></param>
		public static IDataReader Transform(IDataReader reader, IDbProvider dbProvider)
		{
		    if (!dbProvider.AllowMARS && !(reader is InMemoryDataReader))
			{
				// The underlying reader will be closed.
				return new InMemoryDataReader(reader);
			}
		    return reader;
		}
	}
}
