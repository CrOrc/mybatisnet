#region Apache Notice
/*****************************************************************************
 * $Revision: 374175 $
 * $LastChangedDate: 2008-10-11 10:07:44 -0600 (Sat, 11 Oct 2008) $
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
using MyBatis.Common.Exceptions;

namespace MyBatis.Common.Utilities.Objects
{
	/// <summary>
	/// A <see cref="IObjectFactory"/> implementation that for abstract type
	/// </summary>
    public class AbstractFactory : IFactory
    {
        private readonly Type _typeToCreate = null;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractFactory"/> class.
        /// </summary>
        /// <param name="typeToCreate">The type to create.</param>
        public AbstractFactory(Type typeToCreate)
        {
            _typeToCreate = typeToCreate;
        }
        
        #region IFactory Members

        /// <summary>
        /// Create a new instance with the specified parameters
        /// </summary>
        /// <param name="parameters">An array of values that matches the number, order and type
        /// of the parameters for this constructor.</param>
        /// <returns>A new instance</returns>
        /// <remarks>
        /// If you call a constructor with no parameters, pass null.
        /// Anyway, what you pass will be ignore.
        /// </remarks>
        public object CreateInstance(object[] parameters)
        {
            throw new ProbeException(
                string.Format("Unable to optimize create instance. Cause : Could not find public constructor on the abstract type \"{0}\".", _typeToCreate.Name));
        }

        #endregion
    }
}
