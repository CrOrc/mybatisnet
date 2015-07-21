#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 378715 $
 * $Date: 2008-09-21 13:25:16 -0600 (Sun, 21 Sep 2008) $
 * 
 * MyBatis.NET Data Mapper
 * Copyright (C) 2008/2005 - Apache Fondation
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

using System.Web;
using MyBatis.Common.Exceptions;

namespace MyBatis.DataMapper.Session.Stores
{

	/// <summary>
	/// Provides an implementation of <see cref="ISessionStore"/>
	/// which relies on <c>HttpContext</c>. Suitable for web projects.
    /// This implementation will get the current session from the current 
    /// request.
	/// </summary>
	public class WebSessionStore : AbstractSessionStore
	{

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSessionStore"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public WebSessionStore(string id) : base(id)
		{}

		/// <summary>
		/// Get the local session
		/// </summary>
        public override ISession CurrentSession
		{
			get
			{
				HttpContext currentContext = ObtainSessionContext();
                return (ISession)currentContext.Items[sessionName];
			}
		}

		/// <summary>
		/// Store the specified session.
		/// </summary>
		/// <param name="session">The session to store</param>
        public override void Store(ISession session)
		{
			HttpContext currentContext = ObtainSessionContext();
			currentContext.Items[sessionName] = session;
		}

		/// <summary>
		/// Remove the local session.
		/// </summary>
		public override void Dispose()
		{
			HttpContext currentContext = ObtainSessionContext();
			currentContext.Items.Remove(sessionName);
		}

		
		private static HttpContext ObtainSessionContext()
		{
			HttpContext currentContext = HttpContext.Current;
	
			if (currentContext == null)
			{
				throw new MyBatisException("WebSessionStore: Could not obtain reference to HttpContext");
			}
			return currentContext;
		}
	}
}
