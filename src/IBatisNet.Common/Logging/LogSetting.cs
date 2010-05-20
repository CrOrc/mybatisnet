
#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 471480 $
 * $Date: 2006-11-05 11:00:23 -0700 (Sun, 05 Nov 2006) $
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
using System.Collections.Specialized;

namespace IBatisNet.Common.Logging
{
	/// <summary>
	/// Container used to hold configuration information from config file.
	/// </summary>
	internal class LogSetting
	{

		#region Fields

		private Type _factoryAdapterType= null;
		private NameValueCollection _properties = null;

		#endregion 
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="factoryAdapterType">
		/// The <see cref="ILoggerFactoryAdapter" /> type 
		/// that will be used for creating <see cref="ILog" />
		/// </param>
		/// <param name="properties">
		/// Additional user supplied properties that are passed to the 
		/// <paramref name="factoryAdapterType" />'s constructor.
		/// </param>
		public LogSetting ( Type factoryAdapterType , NameValueCollection properties )
		{
			_factoryAdapterType = factoryAdapterType;
			_properties = properties;
		}
		
		/// <summary>
		/// The <see cref="ILoggerFactoryAdapter" /> type that will be used for creating <see cref="ILog" />
		/// instances.
		/// </summary>
		public Type FactoryAdapterType
		{
			get { return _factoryAdapterType; }
		}

		/// <summary>
		/// Additional user supplied properties that are passed to the <see cref="FactoryAdapterType" />'s constructor.
		/// </summary>
		public NameValueCollection Properties
		{
			get { return _properties; }
		}
	}
}
