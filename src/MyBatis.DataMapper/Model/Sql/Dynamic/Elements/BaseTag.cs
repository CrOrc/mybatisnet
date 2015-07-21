#region Apache Notice

/*****************************************************************************
 * $Header: $
 * $Revision: 383115 $
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

#endregion Apache Notice

#region Imports

using System;
using System.Xml.Serialization;

#endregion Imports

namespace MyBatis.DataMapper.Model.Sql.Dynamic.Elements
{
    /// <summary>
    /// Summary description for BaseTag.
    /// </summary>
    [Serializable]
    public abstract class BaseTag : SqlTag
    {
        #region Fields

        [NonSerialized]
        private string _bindName = string.Empty;

        [NonSerialized]
        private string _property = string.Empty;

        #endregion Fields

        /// <summary>
        /// Property attribute
        /// </summary>
        [XmlAttribute("bindName")]
        public string BindName
        {
            get
            {
                return _bindName;
            }
            set
            {
                _bindName = value;
            }
        }

        /// <summary>
        /// Property attribute
        /// </summary>
        [XmlAttribute("property")]
        public string Property
        {
            get
            {
                return _property;
            }
            set
            {
                _property = value;
            }
        }
    }
}