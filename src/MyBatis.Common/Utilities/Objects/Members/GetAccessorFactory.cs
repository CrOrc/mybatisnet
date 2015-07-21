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
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using MyBatis.Common.Exceptions;

namespace MyBatis.Common.Utilities.Objects.Members
{
    /// <summary>
    /// A factory to build <see cref="IGetAccessorFactory"/> for a type.
    /// </summary>
    public class GetAccessorFactory : IGetAccessorFactory
    {
        private delegate IGetAccessor CreatePropertyGetAccessor(Type targetType, string propertyName);
        private delegate IGetAccessor CreateFieldGetAccessor(Type targetType, string fieldName);

        private readonly CreatePropertyGetAccessor _createPropertyGetAccessor = null;
        private readonly CreateFieldGetAccessor _createFieldGetAccessor = null;

        private readonly IDictionary _cachedIGetAccessor = new HybridDictionary();
        private readonly AssemblyBuilder _assemblyBuilder = null;
        private readonly ModuleBuilder _moduleBuilder = null;
        private readonly object syncLock = new object();

         /// <summary>
        /// Initializes a new instance of the <see cref="GetAccessorFactory"/> class.
        /// </summary>
        /// <param name="allowCodeGeneration">if set to <c>true</c> [allow code generation].</param>
        public GetAccessorFactory(bool allowCodeGeneration)
		{
            if (allowCodeGeneration)
            {
                // Detect runtime environment and create the appropriate factory
                if (Environment.Version.Major >= 2)
                {
                    _createPropertyGetAccessor = new CreatePropertyGetAccessor(CreateDynamicPropertyGetAccessor);
                    _createFieldGetAccessor = new CreateFieldGetAccessor(CreateDynamicFieldGetAccessor);
                }
                else
                {
                    AssemblyName assemblyName = new AssemblyName();
                    assemblyName.Name = "MyBatis.FastGetAccessor" + HashCodeProvider.GetIdentityHashCode(this);

                    // Create a new assembly with one module
                    _assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                    _moduleBuilder = _assemblyBuilder.DefineDynamicModule(assemblyName.Name + ".dll");

                    _createPropertyGetAccessor = new CreatePropertyGetAccessor(CreatePropertyAccessor);
                    _createFieldGetAccessor = new CreateFieldGetAccessor(CreateFieldAccessor);
                }
            }
            else
            {
                _createPropertyGetAccessor = new CreatePropertyGetAccessor(CreateReflectionPropertyGetAccessor);
                _createFieldGetAccessor = new CreateFieldGetAccessor(CreateReflectionFieldGetAccessor);
            }
        }

        /// <summary>
        /// Create a Dynamic IGetAccessor instance for a property
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>null if the generation fail</returns>
        private static IGetAccessor CreateDynamicPropertyGetAccessor(Type targetType, string propertyName)
        {
            ReflectionInfo reflectionCache = ReflectionInfo.GetInstance(targetType);
            PropertyInfo propertyInfo = (PropertyInfo)reflectionCache.GetGetter(propertyName);

            if (propertyInfo.CanRead)
            {
                MethodInfo methodInfo = null;
                methodInfo = propertyInfo.GetGetMethod();

                if (methodInfo != null)// == visibilty public
                {
                    return new DelegatePropertyGetAccessor(targetType, propertyName);
                }
                return new ReflectionPropertyGetAccessor(targetType, propertyName);
            }
            throw new NotSupportedException(
                string.Format("Property \"{0}\" on type "
                              + "{1} cannot be get.", propertyInfo.Name, targetType));
        }

        /// <summary>
        /// Create a Dynamic IGetAccessor instance for a field
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="fieldName">Property name.</param>
        /// <returns>null if the generation fail</returns>
        private static IGetAccessor CreateDynamicFieldGetAccessor(Type targetType, string fieldName)
        {
            ReflectionInfo reflectionCache = ReflectionInfo.GetInstance(targetType);
            FieldInfo fieldInfo = (FieldInfo)reflectionCache.GetGetter(fieldName);

            if (fieldInfo.IsPublic)
            {
                return new DelegateFieldGetAccessor(targetType, fieldName);
            }
            return new ReflectionFieldGetAccessor(targetType, fieldName);
        }

        /// <summary>
        /// Create a IGetAccessor instance for a property
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>null if the generation fail</returns>
        private IGetAccessor CreatePropertyAccessor(Type targetType, string propertyName)
        {
            ReflectionInfo reflectionCache = ReflectionInfo.GetInstance(targetType);
            PropertyInfo propertyInfo = (PropertyInfo)reflectionCache.GetGetter(propertyName);

            if (propertyInfo.CanRead)
            {
                MethodInfo methodInfo = null;

                methodInfo = propertyInfo.GetGetMethod();

                if (methodInfo != null)// == visibilty public
                {
                    return new EmitPropertyGetAccessor(targetType, propertyName, _assemblyBuilder, _moduleBuilder);
                }
                return new ReflectionPropertyGetAccessor(targetType, propertyName);
            }
            throw new NotSupportedException(
                string.Format("Property \"{0}\" on type "
                              + "{1} cannot be get.", propertyInfo.Name, targetType));
        }

        /// <summary>
        /// Create a IGetAccessor instance for a field
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="fieldName">Field name.</param>
        /// <returns>null if the generation fail</returns>
        private IGetAccessor CreateFieldAccessor(Type targetType, string fieldName)
        {
            ReflectionInfo reflectionCache = ReflectionInfo.GetInstance(targetType);
            FieldInfo fieldInfo = (FieldInfo)reflectionCache.GetGetter(fieldName);

            if (fieldInfo.IsPublic)
            {
                return new EmitFieldGetAccessor(targetType, fieldName, _assemblyBuilder, _moduleBuilder);
            }
            return new ReflectionFieldGetAccessor(targetType, fieldName);
        }

        /// <summary>
        /// Create a Reflection IGetAccessor instance for a property
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>null if the generation fail</returns>
        private static IGetAccessor CreateReflectionPropertyGetAccessor(Type targetType, string propertyName)
        {
            return new ReflectionPropertyGetAccessor(targetType, propertyName);
        }

        /// <summary>
        /// Create Reflection IGetAccessor instance for a field
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="fieldName">field name.</param>
        /// <returns>null if the generation fail</returns>
        private static IGetAccessor CreateReflectionFieldGetAccessor(Type targetType, string fieldName)
        {
            return new ReflectionFieldGetAccessor(targetType, fieldName);
        }

        #region IGetAccessorFactory Members

        /// <summary>
        /// Generate an <see cref="IGetAccessor"/> instance.
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="name">Field or Property name.</param>
        /// <returns>null if the generation fail</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IGetAccessor CreateGetAccessor(Type targetType, string name)
        {
            string key = new StringBuilder(targetType.FullName).Append(".").Append(name).ToString();

            if (_cachedIGetAccessor.Contains(key))
            {
                return (IGetAccessor)_cachedIGetAccessor[key];
            }
            IGetAccessor getAccessor = null;
            lock (syncLock)
            {
                if (!_cachedIGetAccessor.Contains(key))
                {
                    // Property
                    ReflectionInfo reflectionCache = ReflectionInfo.GetInstance(targetType);
                    MemberInfo memberInfo = reflectionCache.GetGetter(name);

                    if (memberInfo != null)
                    {
                        if (memberInfo is PropertyInfo)
                        {
                            getAccessor = _createPropertyGetAccessor(targetType, name);
                            _cachedIGetAccessor[key] = getAccessor;
                        }
                        else
                        {
                            getAccessor = _createFieldGetAccessor(targetType, name);
                            _cachedIGetAccessor[key] = getAccessor;
                        }
                    }
                    else
                    {
                        throw new ProbeException(
                            string.Format("No property or field named \"{0}\" exists for type "
                                          + "{1}.", name, targetType));
                    }
                }
                else
                {
                    getAccessor = (IGetAccessor)_cachedIGetAccessor[key];
                }
            }
            return getAccessor;
        }

        #endregion
    }
}
