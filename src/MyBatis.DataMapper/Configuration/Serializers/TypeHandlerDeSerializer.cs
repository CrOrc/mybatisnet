#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 408164 $
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

#region Using

using System;
using System.Collections.Specialized;
using System.Xml;
using MyBatis.DataMapper.Model.Alias;
using MyBatis.DataMapper.Scope;
using MyBatis.DataMapper.TypeHandlers;
using MyBatis.Common.Configuration;

#endregion 

namespace MyBatis.DataMapper.Configuration.Serializers
{
	/// <summary>
	/// Summary description for TypeHandlerDeSerializer.
	/// </summary>
	public sealed class TypeHandlerDeSerializer
	{

        /// <summary>
        /// Deserializes the specified config.
        /// </summary>
        /// <param name="config">The config.</param>
        public static TypeHandler Deserialize(IConfiguration config)
        {
            string callBack = config.GetAttributeValue("callback");
            string type = config.GetAttributeValue("type");
            string dbType = config.GetAttributeValue("dbType");

            return new TypeHandler(type, dbType, callBack);
        }
    }
}
