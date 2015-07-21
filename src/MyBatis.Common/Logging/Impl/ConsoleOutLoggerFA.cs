
#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 471478 $
 * $Date: 2008-06-28 09:26:16 -0600 (Sat, 28 Jun 2008) $
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

namespace MyBatis.Common.Logging.Impl
{
	/// <summary>
	/// Factory for creating <see cref="ILog" /> instances that write data to <see cref="Console.Out" />.
	/// </summary>
	public sealed class ConsoleOutLoggerFA: ILoggerFactoryAdapter 
	{
		private readonly Hashtable _logs = Hashtable.Synchronized( new Hashtable() );
        private readonly LogLevel _Level = LogLevel.All;
        private readonly bool _showDateTime = true;
        private readonly bool _showLogName = true;
        private readonly string _dateTimeFormat = string.Empty;

		/// <summary>
		/// Looks for level, showDateTime, showLogName, dateTimeFormat items from 
		/// <paramref name="properties" /> for use when the GetLogger methods are called.
		/// </summary>
		/// <param name="properties">Contains user supplied configuration information.</param>
		public ConsoleOutLoggerFA(NameValueCollection properties)
		{
			try
			{
				_Level = (LogLevel)Enum.Parse( typeof(LogLevel), properties["level"], true );
			}
			catch ( Exception )
			{
				_Level = LogLevel.All;
			}
			try
			{
				_showDateTime = bool.Parse( properties["showDateTime"] );
			}
			catch ( Exception )
			{
				_showDateTime = true;
			}
			try 
			{
				_showLogName = bool.Parse( properties["showLogName"] );
			}
			catch ( Exception )
			{
				_showLogName = true;
			}
			_dateTimeFormat =  properties["dateTimeFormat"];
		}

		#region ILoggerFactoryAdapter Members

		/// <summary>
		/// Get a ILog instance by <see cref="Type" />.
		/// </summary>
		/// <param name="type">Usually the <see cref="Type" /> of the current class.</param>
		/// <returns>An ILog instance that will write data to <see cref="Console.Out" />.</returns>
		public ILog GetLogger(Type type)
		{
			return GetLogger( type.FullName );
		}

		/// <summary>
		/// Get a ILog instance by name.
		/// </summary>
		/// <param name="name">Usually a <see cref="Type" />'s Name or FullName property.</param>
		/// <returns>An ILog instance that will write data to <see cref="Console.Out" />.</returns>
		public ILog GetLogger(string name)
		{
			ILog log = _logs[name] as ILog;
			if ( log == null )
			{
				log = new ConsoleOutLogger( name, _Level, _showDateTime, _showLogName, _dateTimeFormat );
				_logs.Add( name, log );
			}
			return log;
		}

		#endregion
	}
}
