
#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 383115 $
 * $Date: 2006-03-04 07:21:51 -0700 (Sat, 04 Mar 2006) $
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

#region Using
using System;
using System.Text;

#endregion

namespace IBatisNet.DataAccess.Scope
{
	/// <summary>
	/// An error context to help us create meaningful error messages.
	/// </summary>
	public class ErrorContext
	{
		#region Fields

		private string _resource;
		private string _activity;
		private string _objectId;
		private string _moreInfo;
		private Exception _cause;
		#endregion 

		#region Properties

		/// <summary>
		/// The resource causing the problem
		/// </summary>
		public string Resource
		{
			get { return _resource; }
			set { _resource = value; }
		}

		/// <summary>
		/// The activity that was happening when the error happened
		/// </summary>
		public string Activity
		{
			get { return _activity; }
			set { _activity = value; }
		}

		/// <summary>
		/// The object ID where the problem happened
		/// </summary>
		public string ObjectId
		{
			get { return _objectId; }
			set { _objectId = value; }
		}

		/// <summary>
		/// More information about the error
		/// </summary>
		public string MoreInfo
		{
			get { return _moreInfo; }
			set { _moreInfo = value; }
		}

		/// <summary>
		/// The cause of the error
		/// </summary>
		public Exception Cause
		{
			get { return _cause; }
			set { _cause = value; }
		}

		#endregion 

		#region Methods

		/// <summary>
		/// Clear the error context
		/// </summary>
		public void Reset() 
		{
			_resource = string.Empty;
			_activity = string.Empty;
			_objectId = string.Empty;
			_moreInfo = string.Empty;
			_cause = null;
		}


		/// <summary>
		/// ToString method for ErrorContext
		/// </summary>
		/// <returns></returns>
		public override string ToString() 
		{
			StringBuilder message = new StringBuilder();


			// activity
			if (_activity != null && _activity.Length > 0) 
			{
				message.Append("\n- The error occurred while ");
				message.Append(_activity);
				message.Append(".");
			}			

			// more info
			if (_moreInfo != null && _moreInfo.Length > 0) 
			{
				message.Append("\n- ");
				message.Append(_moreInfo);
			}
			
			// resource
			if (_resource != null && _resource.Length > 0) 
			{
				message.Append("\n- The error occurred in ");
				message.Append(_resource);
				message.Append(".");
			}

			// object
			if (_objectId != null && _objectId.Length > 0) 
			{
				message.Append("  \n- Check the ");
				message.Append(_objectId);
				message.Append(".");
			}

			// cause
			if (_cause != null) 
			{
				message.Append("\n- Cause: ");
				message.Append(_cause.ToString());
			}

			return message.ToString();
		}

		#endregion 

	}
}
