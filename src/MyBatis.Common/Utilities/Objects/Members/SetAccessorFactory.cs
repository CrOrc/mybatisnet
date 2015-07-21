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
    /// A factory to build <see cref="SetAccessorFactory"/> for a type.
    /// </summary>
    public class SetAccessorFactory : ISetAccessorFactory
    {
        private delegate ISetAccessor CreatePropertySetAccessor(Type targetType, string propertyName);
        private delegate ISetAccessor CreateFieldSetAccessor(Type targetType, string fieldName);

        private readonly CreatePropertySetAccessor _createPropertySetAccessor = null;
        private readonly CreateFieldSetAccessor _createFieldSetAccessor = null;

        private readonly IDictionary _cachedISetAccessor = new HybridDictionary();
        private readonly AssemblyBuilder _assemblyBuilder = null;
        private readonly ModuleBuilder _moduleBuilder = null;
        private readonly object syncLock = new object();

         /// <summary>
        /// Initializes a new instance of the <see cref="SetAccessorFactory"/> class.
        /// </summary>
        /// <param name="allowCodeGeneration">if set to <c>true</c> [allow code generation].</param>
        public SetAccessorFactory(bool allowCodeGeneration)
		{
            if (allowCodeGeneration)
            {
                // Detect runtime environment and create the appropriate factory
                if (Environment.Version.Major >= 2)
                {
                    _createPropertySetAccessor = new CreatePropertySetAccessor(CreateDynamicPropertySetAccessor);
                    _createFieldSetAccessor = new CreateFieldSetAccessor(CreateDynamicFieldSetAccessor);
                }
                else
                {
                    AssemblyName assemblyName = new AssemblyName();
                    assemblyName.Name = "MyBatis.FastSetAccessor" + HashCodeProvider.GetIdentityHashCode(this);

                    // Create a new assembly with one module
                    _assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
                    _moduleBuilder = _assemblyBuilder.DefineDynamicModule(assemblyName.Name + ".dll");

                    _createPropertySetAccessor = new CreatePropertySetAccessor(CreatePropertyAccessor);
                    _createFieldSetAccessor = new CreateFieldSetAccessor(CreateFieldAccessor);
                }
            }
            else
            {
                _createPropertySetAccessor = new CreatePropertySetAccessor(CreateReflectionPropertySetAccessor);
                _createFieldSetAccessor = new CreateFieldSetAccessor(CreateReflectionFieldSetAccessor);
            }
        }

        /// <summary>
        /// Create a Dynamic ISetAccessor instance for a property
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>null if the generation fail</returns>
        private static ISetAccessor CreateDynamicPropertySetAccessor(Type targetType, string propertyName)
        {
            ReflectionInfo reflectionCache = ReflectionInfo.GetInstance(targetType);
            PropertyInfo propertyInfo = (PropertyInfo)reflectionCache.GetSetter(propertyName);

            if (propertyInfo.CanWrite)
            {
                MethodInfo methodInfo = null;
                methodInfo = propertyInfo.GetSetMethod();

                if (methodInfo!=null)// == visibilty public
                {
                    return new DelegatePropertySetAccessor(targetType, propertyName);
                }
                return new ReflectionPropertySetAccessor(targetType, propertyName);
            }
            throw new NotSupportedException(
                string.Format("Property \"{0}\" on type "
                              + "{1} cannot be set.", propertyInfo.Name, targetType));
        }

        /// <summary>
        /// Create a Dynamic ISetAccessor instance for a field
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="fieldName">field name.</param>
        /// <returns>null if the generation fail</returns>
        private static ISetAccessor CreateDynamicFieldSetAccessor(Type targetType, string fieldName)
        {
            ReflectionInfo reflectionCache = ReflectionInfo.GetInstance(targetType);
            FieldInfo fieldInfo = (FieldInfo)reflectionCache.GetSetter(fieldName);

            if (fieldInfo.IsPublic)
            {
                return new DelegateFieldSetAccessor(targetType, fieldName);
            }
            return new ReflectionFieldSetAccessor(targetType, fieldName);
        }



        /// <summary>
        /// Create a ISetAccessor instance for a property
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>null if the generation fail</returns>
        private ISetAccessor CreatePropertyAccessor(Type targetType, string propertyName)
        {
            ReflectionInfo reflectionCache = ReflectionInfo.GetInstance(targetType);
            PropertyInfo propertyInfo = (PropertyInfo)reflectionCache.GetSetter(propertyName);

            if (propertyInfo.CanWrite)
            {
                MethodInfo methodInfo = null;
                methodInfo = propertyInfo.GetSetMethod();

                if (methodInfo != null)// == visibilty public
                {
                    return new EmitPropertySetAccessor(targetType, propertyName, _assemblyBuilder, _moduleBuilder);
                }
                return new ReflectionPropertySetAccessor(targetType, propertyName);
            }
            throw new NotSupportedException(
                string.Format("Property \"{0}\" on type "
                              + "{1} cannot be set.", propertyInfo.Name, targetType));
        }

        /// <summary>
        /// Create a ISetAccessor instance for a field
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="fieldName">Field name.</param>
        /// <returns>null if the generation fail</returns>
        private ISetAccessor CreateFieldAccessor(Type targetType, string fieldName)
        {
            ReflectionInfo reflectionCache = ReflectionInfo.GetInstance(targetType);
            FieldInfo fieldInfo = (FieldInfo)reflectionCache.GetSetter(fieldName);

            if (fieldInfo.IsPublic)
            {
                return new EmitFieldSetAccessor(targetType, fieldName, _assemblyBuilder, _moduleBuilder);
            }
            return new ReflectionFieldSetAccessor(targetType, fieldName);
        }

        /// <summary>
        /// Create a Reflection ISetAccessor instance for a property
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="propertyName">Property name.</param>
        /// <returns>null if the generation fail</returns>
        private static ISetAccessor CreateReflectionPropertySetAccessor(Type targetType, string propertyName)
        {
            return new ReflectionPropertySetAccessor(targetType, propertyName);
        }

        /// <summary>
        /// Create Reflection ISetAccessor instance for a field
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="fieldName">field name.</param>
        /// <returns>null if the generation fail</returns>
        private static ISetAccessor CreateReflectionFieldSetAccessor(Type targetType, string fieldName)
        {
            return new ReflectionFieldSetAccessor(targetType, fieldName);
        }

        #region ISetAccessorFactory Members

        /// <summary>
        /// Generate an <see cref="ISetAccessor"/> instance.
        /// </summary>
        /// <param name="targetType">Target object type.</param>
        /// <param name="name">Field or Property name.</param>
        /// <returns>null if the generation fail</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public ISetAccessor CreateSetAccessor(Type targetType, string name)
        {
            string key = new StringBuilder(targetType.FullName).Append(".").Append(name).ToString();

            if (_cachedISetAccessor.Contains(key))
            {
                return (ISetAccessor)_cachedISetAccessor[key];
            }
            ISetAccessor setAccessor = null;
            lock (syncLock)
            {
                if (!_cachedISetAccessor.Contains(key))
                {
                    // Property
                    ReflectionInfo reflectionCache = ReflectionInfo.GetInstance(targetType);
                    MemberInfo memberInfo = reflectionCache.GetSetter(name);

                    if (memberInfo != null)
                    {
                        if (memberInfo is PropertyInfo)
                        {
                            setAccessor = _createPropertySetAccessor(targetType, name);
                            _cachedISetAccessor[key] = setAccessor;
                        }
                        else
                        {
                            setAccessor = _createFieldSetAccessor(targetType, name);
                            _cachedISetAccessor[key] = setAccessor;
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
                    setAccessor = (ISetAccessor)_cachedISetAccessor[key];
                }
            }
            return setAccessor;
        }

        #endregion
    }
}
