
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

using System;

using IBatisNet.Common;
using IBatisNet.DataMapper.Scope;

namespace IBatisNet.DataMapper.Configuration.Sql
{
	/// <summary>
	/// Summary description for ISql.
	/// </summary>
	public interface ISql
	{

		#region Methods
		/// <summary>
		/// Get the RequestScope for this request.
		/// </summary>
		/// <param name="parameterObject">
		/// The parameter object (used by DynamicSql/SimpleDynamicSql).
		/// Use to complete the sql statement.
		/// </param>
		/// <param name="session"></param>
		/// <returns>The RequestScope.</returns>
		RequestScope GetRequestScope(object parameterObject, IDalSession session);
		#endregion

	}
}
