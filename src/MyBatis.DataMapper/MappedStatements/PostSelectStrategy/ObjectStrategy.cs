#region Apache Notice
/*****************************************************************************
 * $Revision: 374175 $
 * $LastChangedDate: 2008-06-28 09:26:16 -0600 (Sat, 28 Jun 2008) $
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

using MyBatis.DataMapper.Scope;

namespace MyBatis.DataMapper.MappedStatements.PostSelectStrategy
{
	/// <summary>
	/// <see cref="IPostSelectStrategy"/> implementation to exceute a query for object.
	/// </summary>
    public sealed class ObjectStrategy : IPostSelectStrategy
	{
		#region IPostSelectStrategy Members

		/// <summary>
		/// Executes the specified <see cref="PostBindind"/>.
		/// </summary>
		/// <param name="postSelect">The <see cref="PostBindind"/>.</param>
		/// <param name="request">The <see cref="RequestScope"/></param>
		public void Execute(PostBindind postSelect, RequestScope request)
		{
			object value = postSelect.Statement.ExecuteQueryForObject(request.Session, postSelect.Keys, null);
			postSelect.ResultProperty.Set(postSelect.Target, value);
		}

		#endregion
	}
}
