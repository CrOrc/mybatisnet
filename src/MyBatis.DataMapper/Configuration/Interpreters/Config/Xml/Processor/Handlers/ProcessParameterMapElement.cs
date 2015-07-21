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

using MyBatis.Common.Configuration;

namespace MyBatis.DataMapper.Configuration.Interpreters.Config.Xml.Processor
{
    public partial class XmlMappingProcessor
    {
        /// <summary>
        /// Processes a parameter map element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="configurationStore">The configuration store.</param>
        private void ProcessParameterMapElement(Tag element, IConfigurationStore configurationStore)
        {
            MutableConfiguration config = null;

            if (element.Attributes.ContainsKey(ConfigConstants.ATTRIBUTE_CLASS))
            {
                config = new MutableConfiguration(
                        element.Name,
                        ApplyNamespace(element.Attributes[ConfigConstants.ATTRIBUTE_ID]),
                        element.Attributes[ConfigConstants.ATTRIBUTE_CLASS]);
            }
            else
            {
                config = new MutableConfiguration(
                        element.Name,
                        ApplyNamespace(element.Attributes[ConfigConstants.ATTRIBUTE_ID]));
            }
            config.CreateAttributes(element.Attributes);
            config.CreateAttribute(ConfigConstants.ATTRIBUTE_NAMESPACE, nameSpace);

            AddAttribute(config, ConfigConstants.ATTRIBUTE_EXTENDS, true);

            configurationStore.AddParameterMapConfiguration(config);
            element.Configuration = config;
        }

    }
}
