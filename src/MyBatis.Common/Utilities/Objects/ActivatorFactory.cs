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

namespace MyBatis.Common.Utilities.Objects
{
	/// <summary>
	/// Create objects via Activator.CreateInstance
	/// </summary>
    public sealed class ActivatorFactory : IFactory
	{
		private readonly Type typeToCreate = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="typeToCreate"></param>
		public ActivatorFactory(Type typeToCreate)
		{
			this.typeToCreate = typeToCreate;
		}

		#region IFactory members
		
		/// <summary>
		/// Create a new instance with the specified parameters
		/// </summary>
		/// <param name="parameters">
		/// An array of values that matches the number, order and type 
		/// of the parameters for this constructor. 
		/// </param>
		/// <remarks>
		/// If you call a constructor with no parameters, pass null. 
		/// Anyway, what you pass will be ignore.
		/// </remarks>
		/// <returns>A new instance</returns>
		public object CreateInstance(object[] parameters)
		{
			return Activator.CreateInstance( typeToCreate, parameters );
		}

		#endregion
	}
}
