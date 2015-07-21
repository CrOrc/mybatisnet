
#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 383115 $
 * $Date: 2008-09-21 13:25:16 -0600 (Sun, 21 Sep 2008) $
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

namespace MyBatis.DataMapper.TypeHandlers
{
	/// <summary>
	/// Allows parameters to be set on the underlying prepared IDbCommand.
	/// TypeHandlerCallback implementations use this interface to
	/// process values before they are set on the IDbCommand.
	/// </summary>
	/// <remarks>
	/// There is no need to implement this.  The implementation
	/// will be passed into the TypeHandlerCallback automatically.
	/// </remarks>
	public interface IParameterSetter
	{

		/// <summary>
		/// Returns the underlying IDataParameter
		/// </summary>
		IDataParameter DataParameter { get; }

		/// <summary>
		/// Get the parameter value
		/// </summary>
		object Value { set; }
	}
}
