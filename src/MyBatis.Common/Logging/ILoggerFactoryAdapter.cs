
#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 474141 $
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
using System.Collections.Specialized;
using MyBatis.Common.Logging.Impl;

namespace MyBatis.Common.Logging
{
	/// <summary>
	/// Defines the members that logging implementations must implement.
	/// </summary>
	/// <remarks>
	/// Classes that implement this interface may optional implement a constructor that accepts 
	/// a <see cref="NameValueCollection" /> which will contain zero or more user supplied configuration
	/// properties.
	/// <para>
	/// The MyBatis.Common assembly ships with the following built-in <see cref="ILoggerFactoryAdapter" /> implementations:
	/// </para>
	///	<list type="table">
	///	<item><term><see cref="ConsoleOutLoggerFA" /></term><description>Writes output to Console.Out</description></item>
	///	<item><term><see cref="TraceLoggerFA" /></term><description>Writes output to the System.Diagnostics.Trace sub-system</description></item>
	///	<item><term><see cref="NoOpLoggerFA" /></term><description>Ignores all messages</description></item>
	///	</list>
	/// </remarks>
	public interface ILoggerFactoryAdapter 
	{
		/// <summary>
		/// Get a <see cref="ILog" /> instance by type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		ILog GetLogger( Type type );

		/// <summary>
		/// Get a <see cref="ILog" /> instance by name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		ILog GetLogger( string name );	

	}
}
