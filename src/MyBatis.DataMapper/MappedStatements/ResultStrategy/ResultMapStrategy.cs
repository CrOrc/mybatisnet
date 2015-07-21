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

using System.Data;
using MyBatis.DataMapper.Model.ResultMapping;
using MyBatis.DataMapper.Scope;

namespace MyBatis.DataMapper.MappedStatements.ResultStrategy
{
    /// <summary>
    /// <see cref="IResultStrategy"/> implementation when 
    /// a 'resultMap' attribute is specified.
    /// </summary>
    public sealed class ResultMapStrategy : BaseStrategy, IResultStrategy
    {
        #region IResultStrategy Members

        /// <summary>
        /// Processes the specified <see cref="IDataReader"/> 
        /// when a ResultMap is specified on the statement.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="resultObject">The result object.</param>
        public object Process(RequestScope request, ref IDataReader reader, object resultObject)
        {
            object outObject = resultObject;

            IResultMap resultMap = request.CurrentResultMap.ResolveSubMap(reader);

            if (outObject == null)
            {
                object[] parameters = null;
                if (resultMap.Parameters.Count > 0)
                {
                    parameters = new object[resultMap.Parameters.Count];
                    // Fill parameters array
                    for (int index = 0; index < resultMap.Parameters.Count; index++)
                    {
                        ResultProperty resultProperty = resultMap.Parameters[index];
                        parameters[index] = resultProperty.GetValue(request, ref reader, null);
                    }
                }

                outObject = resultMap.CreateInstanceOfResult(parameters);
            }

            // For each Property in the ResultMap, set the property in the object 
            for (int index = 0; index < resultMap.Properties.Count; index++)
            {
                ResultProperty property = resultMap.Properties[index];
                property.PropertyStrategy.Set(request, resultMap, property, ref outObject, reader, null);
            }

            return outObject;
        }

        #endregion
    }
}
