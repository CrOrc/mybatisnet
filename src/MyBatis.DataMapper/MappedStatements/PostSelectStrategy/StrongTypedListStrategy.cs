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

using System;
using System.Collections;
using MyBatis.DataMapper.Scope;
using MyBatis.Common.Utilities.Objects;

namespace MyBatis.DataMapper.MappedStatements.PostSelectStrategy
{
	/// <summary>
	/// <see cref="IPostSelectStrategy"/> implementation to exceute a query for 
	/// strong typed list.
	/// </summary>
    public sealed class StrongTypedListStrategy : IPostSelectStrategy
	{
		#region IPostSelectStrategy Members

		/// <summary>
		/// Executes the specified <see cref="PostBindind"/>.
		/// </summary>
		/// <param name="postSelect">The <see cref="PostBindind"/>.</param>
		/// <param name="request">The <see cref="RequestScope"/></param>
		public void Execute(PostBindind postSelect, RequestScope request)
		{
			IFactory factory =  request.DataExchangeFactory.ObjectFactory.CreateFactory(postSelect.ResultProperty.SetAccessor.MemberType, Type.EmptyTypes);
			object values = factory.CreateInstance(null);
			postSelect.Statement.ExecuteQueryForList(request.Session, postSelect.Keys, (IList)values);	
			postSelect.ResultProperty.Set(postSelect.Target, values);
		}

		#endregion
	}
}
