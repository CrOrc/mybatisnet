#region Apache Notice
/*****************************************************************************
 * $Revision: 576082 $
 * $LastChangedDate: 2008-06-28 09:26:16 -0600 (Sat, 28 Jun 2008) $
 * $LastChangedBy: gbayon $
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
using System.Collections.Generic;
using System.Data;
using MyBatis.DataMapper.DataExchange;

#endregion

namespace MyBatis.DataMapper.Model.ResultMapping
{
    /// <summary>
    /// This is a grouping of ResultMapping objects used to map results back to objects
    /// </summary>
    public interface IResultMap : IResultMapEvents
    {
        /// <summary>
        /// The collection of constructor parameters.
        /// </summary>
        ArgumentPropertyCollection Parameters { get; }
        
        /// <summary>
        /// The collection of ResultProperty.
        /// </summary>
        ResultPropertyCollection Properties { get; }

        /// <summary>
        /// The GroupBy Properties.
        /// </summary>
        ResultPropertyCollection GroupByProperties { get; }

        /// <summary>
        /// Identifier used to identify the resultMap amongst the others.
        /// </summary>
        /// <example>GetProduct</example>
        string Id { get; }

        /// <summary>
        /// The GroupBy Properties name.
        /// </summary>
        List<string> GroupByPropertyNames { get; }

        /// <summary>
        /// The Key Properties name (used for resolved circular reference).
        /// </summary>
        List<string> KeyPropertyNames { get; }

        /// <summary>
        /// The Keys Properties.
        /// </summary>
        ResultPropertyCollection KeysProperties { get; }

        /// <summary>
        /// The output type class of the resultMap.
        /// </summary>
        Type Class { get; }

        /// <summary>
        /// Sets the IDataExchange
        /// </summary>
        IDataExchange DataExchange { set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initalized.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is initalized; otherwise, <c>false</c>.
        /// </value>
        bool IsInitalized { get; set; }


        /// <summary>
        /// Create an instance Of result.
        /// </summary>
        /// <param name="parameters">
        /// An array of values that matches the number, order and type 
        /// of the parameters for this constructor. 
        /// </param>
        /// <returns>An object.</returns>
        object CreateInstanceOfResult(object[] parameters);

        /// <summary>
        /// Set the value of an object property.
        /// </summary>
        /// <param name="target">The object to set the property.</param>
        /// <param name="property">The result property to use.</param>
        /// <param name="dataBaseValue">The database value to set.</param>
        void SetValueOfProperty(ref object target, ResultProperty property, object dataBaseValue);

        /// <summary>
        /// Resolves the sub map.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns></returns>
        IResultMap ResolveSubMap(IDataReader dataReader);

    }
}