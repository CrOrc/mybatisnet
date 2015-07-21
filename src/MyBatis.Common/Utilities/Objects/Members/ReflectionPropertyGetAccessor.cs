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
using System.Reflection;

namespace MyBatis.Common.Utilities.Objects.Members
{
    /// <summary>
    /// The <see cref="ReflectionPropertyGetAccessor"/> class provides an reflection get access   
    /// to a property of a specified target class.
    /// </summary>
    public sealed class ReflectionPropertyGetAccessor : IGetAccessor
    {
        private readonly PropertyInfo _propertyInfo = null;
		private readonly string _propertyName = string.Empty;
		private readonly Type _targetType = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionPropertyGetAccessor"/> class.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="propertyName">Name of the property.</param>
        public ReflectionPropertyGetAccessor(Type targetType, string propertyName)
		{
			ReflectionInfo reflectionCache = ReflectionInfo.GetInstance( targetType );
            _propertyInfo = (PropertyInfo)reflectionCache.GetGetter(propertyName);

            _targetType = targetType;
			_propertyName = propertyName;
		}


        #region IAccessor Members

        /// <summary>
        /// Gets the property name.
        /// </summary>
        public string Name
        {
            get { return _propertyInfo.Name; }
        }

        /// <summary>
        /// Gets the type of this property.
        /// </summary>
        public Type MemberType
        {
            get { return _propertyInfo.PropertyType; }
        }

        #endregion

        #region IGet Members

        /// <summary>
        /// Gets the value stored in the property for 
        /// the specified target.
        /// </summary>
        /// <param name="target">Object to retrieve the property from.</param>
        /// <returns>Property value.</returns>
        public object Get(object target)
        {
            if (_propertyInfo.CanRead)
            {
                return _propertyInfo.GetValue(target, null);
            }
            throw new NotSupportedException(
                string.Format("Property \"{0}\" on type "
                              + "{1} doesn't have a get method.", _propertyName, _targetType));
        }

        #endregion
    }
}
