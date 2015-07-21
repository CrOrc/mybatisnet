#region Apache Notice
/*****************************************************************************
 * $Revision: 374175 $
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
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace MyBatis.Common.Utilities.Objects
{
	/// <summary>
    /// A <see cref="IObjectFactory"/> implementation that can create objects via IL code
	/// </summary>
	public sealed class EmitObjectFactory : IObjectFactory
	{
        private readonly IDictionary _cachedfactories = new HybridDictionary();
        private readonly FactoryBuilder _factoryBuilder = null;
        private readonly object syncLock = new object();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EmitObjectFactory"/> class.
        /// </summary>
		public EmitObjectFactory()
		{
			_factoryBuilder = new FactoryBuilder();
		}

		#region IObjectFactory members

		/// <summary>
        /// Create a new <see cref="IFactory"/> instance for a given type
        /// </summary>
		/// <param name="typeToCreate">The type instance to build</param>
		/// <param name="types">The types of the constructor arguments</param>
        /// <returns>Returns a new <see cref="IFactory"/> instance.</returns>
		public IFactory CreateFactory(Type typeToCreate, Type[] types)
		{
			string key = GenerateKey(typeToCreate, types);

			IFactory factory = _cachedfactories[key] as IFactory;
			if (factory == null)
			{
				lock (syncLock)
				{
					factory = _cachedfactories[key] as IFactory;
					if (factory == null) // double-check
					{
						factory = _factoryBuilder.CreateFactory(typeToCreate, types);
						_cachedfactories[key] = factory;
					}
				}
			}
			return factory;
		}

		/// <summary>
		/// Generates the key for a cache entry.
		/// </summary>
		/// <param name="typeToCreate">The type instance to build.</param>
		/// <param name="arguments">The types of the constructor arguments</param>
		/// <returns>The key for a cache entry.</returns>
		private static string GenerateKey(Type typeToCreate, object[] arguments)
		{
			StringBuilder cacheKey = new StringBuilder();
			cacheKey.Append(typeToCreate.ToString());
			cacheKey.Append(".");
			if ((arguments != null) && (arguments.Length != 0)) 
			{
				for (int i=0; i<arguments.Length; i++) 
				{
					cacheKey.Append(".").Append(arguments[i]);
				}
			}
			return cacheKey.ToString();
		}
		#endregion
	}
}
