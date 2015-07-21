
#region Apache Notice
/*****************************************************************************
 * $Revision: 408164 $
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

using System.Data;

namespace MyBatis.DataMapper.TypeHandlers
{
	/// <summary>
	/// A ParameterSetter implementation
	/// </summary>
    public sealed class ParameterSetterImpl : IParameterSetter	
	{
		private readonly IDataParameter _dataParameter = null;

		#region Constructor

		/// <summary>
		/// Default Constructor
		/// </summary>
		/// <param name="dataParameter"></param>
		public ParameterSetterImpl(IDataParameter dataParameter) 
		{
			_dataParameter = dataParameter;
		}
		#endregion 

		#region IParameterSetter members

		/// <summary>
		/// Returns the underlying DataParameter
		/// </summary>
		public IDataParameter DataParameter
		{
			get { return _dataParameter; }
		}

		/// <summary>
		/// Set the parameter value
		/// </summary>
		public object Value
		{
			set { _dataParameter.Value = value; }
		}

		#endregion
	}
}
