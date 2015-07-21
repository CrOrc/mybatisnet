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

using MyBatis.DataMapper.Model.ParameterMapping;
using MyBatis.DataMapper.Model.ResultMapping;
using MyBatis.Common.Utilities.Objects;

namespace MyBatis.DataMapper.DataExchange
{
	/// <summary>
	/// IDataExchange implementation for IList objects
	/// </summary>
	public sealed class ListDataExchange : BaseDataExchange
	{

		/// <summary>
		/// Cosntructor
		/// </summary>
		/// <param name="dataExchangeFactory"></param>
		public ListDataExchange(DataExchangeFactory dataExchangeFactory):base(dataExchangeFactory)
		{
		}

		#region IDataExchange Members

		/// <summary>
		/// Gets the data to be set into a IDataParameter.
		/// </summary>
		/// <param name="mapping"></param>
		/// <param name="parameterObject"></param>
		public override object GetData(ParameterProperty mapping, object parameterObject)
		{
			return ObjectProbe.GetMemberValue(parameterObject, mapping.PropertyName,
				DataExchangeFactory.AccessorFactory);
		}

		/// <summary>
		/// Sets the value to the result property.
		/// </summary>
		/// <param name="mapping"></param>
		/// <param name="target"></param>
		/// <param name="dataBaseValue"></param>
		public override void SetData(ref object target, ResultProperty mapping, object dataBaseValue)
		{
			ObjectProbe.SetMemberValue(target, mapping.PropertyName, dataBaseValue, 
				DataExchangeFactory.ObjectFactory,
				DataExchangeFactory.AccessorFactory);
		}

		/// <summary>
		/// Sets the value to the parameter property.
		/// </summary>
		/// <remarks>Use to set value on output parameter</remarks>
		/// <param name="mapping"></param>
		/// <param name="target"></param>
		/// <param name="dataBaseValue"></param>
		public override void SetData(ref object target, ParameterProperty mapping, object dataBaseValue)
		{
			ObjectProbe.SetMemberValue(target, mapping.PropertyName, dataBaseValue, 
				DataExchangeFactory.ObjectFactory,
				DataExchangeFactory.AccessorFactory);
		}

		#endregion
	}
}