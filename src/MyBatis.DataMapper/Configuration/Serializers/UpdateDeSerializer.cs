#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 469233 $
 * $Date: 2009-06-28 10:11:37 -0600 (Sun, 28 Jun 2009) $
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

using System.Collections.Specialized;
using System.Xml;
using MyBatis.DataMapper.Model.Statements;
using MyBatis.DataMapper.Scope;
using MyBatis.DataMapper.Model;
using MyBatis.Common.Configuration;

#endregion 


namespace MyBatis.DataMapper.Configuration.Serializers
{
	/// <summary>
	/// Summary description for UpdateDeSerializer.
	/// </summary>
    public sealed class UpdateDeSerializer : BaseStatementDeSerializer
	{
        /// <summary>
        /// Deserializes the specified configuration in a <see cref="Update"/> object.
        /// </summary>
        /// <param name="modelStore">The model store.</param>
        /// <param name="config">The config.</param>
        /// <param name="configurationSetting"></param>
        /// <returns></returns>
        public override IStatement Deserialize(IModelStore modelStore, IConfiguration config, ConfigurationSetting configurationSetting)
        {
            BaseDeserialize(modelStore, config, configurationSetting);

            return new Update(
                id,
                parameterClass,
                parameterMap,
                remapResults,
                extendsName,
                sqlSource,
                preserveWhitespace);
        }
	}
}
