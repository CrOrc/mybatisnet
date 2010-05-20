
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

#region Imports
using System;
using System.Collections;
using System.Collections.Specialized;
#endregion

namespace IBatisNet.DataMapper.Configuration.Cache
{
	/// <summary>
	/// Summary description for ICacheController.
	/// </summary>
	public interface ICacheController
	{
		#region Properties
		/// <summary>
		/// Adds an item with the specified key and value into cached data.
		/// Gets a cached object with the specified key.
		/// </summary>
		/// <value>The cached object or <c>null</c></value>
		object this [object key] 
		{
			get;
			set;
		}
		#endregion

		#region Methods
		/// <summary>
		/// Clears all elements from the cache.
		/// </summary>
		void Flush ();

		/// <summary>
		/// Configures the CacheController
		/// </summary>
		/// <param name="properties"></param>
		void Configure(HybridDictionary properties);
		#endregion

	}
}
