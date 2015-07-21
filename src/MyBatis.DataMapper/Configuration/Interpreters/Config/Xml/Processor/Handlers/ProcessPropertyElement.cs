﻿#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 591621 $
 * $Date: 2009-06-13 20:02:32 -0600 (Sat, 13 Jun 2009) $
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

using System.Xml;
using MyBatis.Common.Configuration;
using MyBatis.Common.Resources;

namespace MyBatis.DataMapper.Configuration.Interpreters.Config.Xml.Processor
{
    public partial class XmlConfigProcessor
    {
        /// <summary>
        /// Processes the property element in the SqlMap.config file.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="configurationStore">The configuration store.</param>
        private void ProcessPropertyElement(Tag element, IConfigurationStore configurationStore)
        {
            if (element.Parent.Name != ConfigConstants.ELEMENT_CACHEMODEL)
            {
                if (element.Attributes.ContainsKey(ConfigConstants.ATTRIBUTE_URI))
                {
                    string uri = element.Attributes[ConfigConstants.ATTRIBUTE_URI];

                    using (XmlTextReader reader = Resources.GetUriAsXmlReader(uri))
                    {
                        this.Process(reader, configurationStore);
                    }
                }
                else
                {
                    IConfiguration config = new MutableConfiguration(
                        element.Name,
                        element.Attributes[ConfigConstants.ATTRIBUTE_KEY],
                        element.Attributes[ConfigConstants.ATTRIBUTE_VALUE]);
                    configurationStore.AddPropertyConfiguration(config);
                }
            }
        }
    }
}
