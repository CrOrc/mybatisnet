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
using System.Data;
using MyBatis.DataMapper.Model.ResultMapping;
using MyBatis.DataMapper.Scope;

namespace MyBatis.DataMapper.MappedStatements.PropertyStrategy
{
	/// <summary>
	/// <see cref="IPropertyStrategy"/> implementation when a 'select' attribute exists
	/// on a <see cref="IList"/> <see cref="ResultProperty"/>
	/// </summary>
    public sealed class SelectListStrategy : IPropertyStrategy
	{
		#region IPropertyStrategy members
		
		///<summary>
		/// Sets value of the specified <see cref="ResultProperty"/> on the target object
		/// when a 'select' attribute exists and fills an <see cref="IList"/> property
		/// on the <see cref="ResultProperty"/> are empties.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="resultMap">The result map.</param>
		/// <param name="mapping">The ResultProperty.</param>
		/// <param name="target">The target.</param>
		/// <param name="reader">The current <see cref="IDataReader"/></param>
		/// <param name="keys">The keys</param>
		public void Set(RequestScope request, IResultMap resultMap, 
			ResultProperty mapping, ref object target, IDataReader reader, object keys)
		{
			// Get the select statement
			IMappedStatement selectStatement = request.MappedStatement.ModelStore.GetMappedStatement(mapping.Select);

			PostBindind postSelect = new PostBindind();
			postSelect.Statement = selectStatement;
			postSelect.Keys = keys;
			postSelect.Target = target;
			postSelect.ResultProperty = mapping;		
		
			if (mapping.IsLazyLoad)
			{
                object values = mapping.LazyFactory.CreateProxy(
                    request.MappedStatement.ModelStore.DataMapper,
                    selectStatement, keys, target, mapping.SetAccessor);
				mapping.Set(target, values);
			}
			else
			{
				if (mapping.SetAccessor.MemberType == typeof(IList))
				{
					postSelect.Method = PostBindind.ExecuteMethod.ExecuteQueryForIList;
				}
				else
				{
					postSelect.Method = PostBindind.ExecuteMethod.ExecuteQueryForStrongTypedIList;
				}
				request.DelayedLoad.Enqueue(postSelect);
			}

		}

        /// <summary>
        /// Gets the value of the specified <see cref="ResultProperty"/> that must be set on the target object.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="resultMap">The result map.</param>
        /// <param name="mapping">The mapping.</param>
        /// <param name="reader">The reader.</param>
		/// <param name="target">The target object</param>
		public object Get(RequestScope request, IResultMap resultMap, ResultProperty mapping, ref object target, IDataReader reader)
        {
            throw new NotSupportedException("Get method on ResultMapStrategy is not supported");
        }
		#endregion
	}
}
