#region Apache Notice
/*****************************************************************************
 * $Revision: 374175 $
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

using System;

using MyBatis.DataMapper.MappedStatements;
using MyBatis.Common.Utilities.Objects.Members;

namespace MyBatis.DataMapper.Proxy
{
    /// <summary>
    /// Contract of an <see cref="ILazyFactory"/>
    /// </summary>
    public interface ILazyFactory
    {
        /// <summary>
        /// Create a new proxy instance.
        /// </summary>
        /// <param name="dataMapper">The data mapper.</param>
        /// <param name="mappedStatement">The mapped statement.</param>
        /// <param name="param">The param.</param>
        /// <param name="target">The target.</param>
        /// <param name="setAccessor">The set accessor.</param>
        /// <returns>Returns a new proxy.</returns>
        object CreateProxy(
            IDataMapper dataMapper,
            IMappedStatement mappedStatement, 
            object param,
            object target, ISetAccessor setAccessor);
    }
}
