#region Apache Notice

/*****************************************************************************
 * $Revision: 575902 $
 * $LastChangedDate: 2008-10-19 05:25:12 -0600 (Sun, 19 Oct 2008) $
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

#endregion Apache Notice

#region Using

using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Xml.Serialization;
using MyBatis.Common.Contracts;
using MyBatis.Common.Contracts.Constraints;
using MyBatis.Common.Exceptions;
using MyBatis.Common.Utilities;
using MyBatis.Common.Utilities.Objects;
using MyBatis.Common.Utilities.Objects.Members;
using MyBatis.DataMapper.DataExchange;
using MyBatis.DataMapper.Model.Binding;
using MyBatis.DataMapper.Model.Sql.Dynamic;
using MyBatis.DataMapper.Model.Sql.Dynamic.Handlers;
using MyBatis.DataMapper.Model.Sql.Dynamic.Parsers;
using MyBatis.DataMapper.TypeHandlers;

#endregion Using

namespace MyBatis.DataMapper.Model.ParameterMapping
{
    /// <summary>
    /// Summary description for ParameterProperty.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("ParameterProperty: {propertyName}-{columnName}")]
    public class ParameterProperty
    {
        #region Fields

        [NonSerialized]
        private readonly DataExchangeFactory _dataExchangeFactory;

        [NonSerialized]
        private readonly Type _parameterClass;

        [NonSerialized]
        private readonly string _propertyPlaceholder = string.Empty;

        [NonSerialized]
        private readonly string callBackName = string.Empty;

        [NonSerialized]
        private readonly string clrType = string.Empty;

        [NonSerialized]
        private readonly string columnName = string.Empty;

        [NonSerialized]
        private readonly string dbType = null;

        [NonSerialized]
        private readonly string directionAttribute = string.Empty;

        [NonSerialized]
        private readonly IGetAccessor getAccessor = null;

        [NonSerialized]
        private readonly bool isComplexMemberName = false;

        [NonSerialized]
        private readonly string nullValue = null;

        [NonSerialized]
        private readonly byte precision = 0;

        [NonSerialized]
        private readonly byte scale = 0;

        [NonSerialized]
        private readonly int size = -1;

        [NonSerialized]
        private string _currentPropertyName = string.Empty;

        [NonSerialized]
        private ParameterDirection direction = ParameterDirection.Input;

        // used only for store procedure
        [NonSerialized]
        private ITypeHandler typeHandler = null;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Specify the custom type handlers to used.
        /// </summary>
        /// <remarks>Will be an alias to a class which implement ITypeHandlerCallback</remarks>
        public string CallBackName
        {
            get { return callBackName; }
        }

        /// <summary>
        /// Specify the CLR type of the parameter.
        /// </summary>
        /// <remarks>
        /// The type attribute is used to explicitly specify the property type to be read.
        /// Normally this can be derived from a property through reflection, but certain mappings such as
        /// HashTable cannot provide the type to the framework.
        /// </remarks>
        public string CLRType
        {
            get { return clrType; }
        }

        /// <summary>
        /// Column Name for output parameter
        /// in store proccedure.
        /// </summary>
        public string ColumnName
        {
            get { return columnName; }
        }

        /// <summary>
        /// Give an entry in the 'DbType' enumeration
        /// </summary>
        /// <example >
        /// For Sql Server, give an entry of SqlDbType : Bit, Decimal, Money...
        /// <br/>
        /// For Oracle, give an OracleType Enumeration : Byte, Int16, Number...
        /// </example>
        public string DbType
        {
            get { return dbType; }
        }

        /// <summary>
        /// Indicate the direction of the parameter.
        /// </summary>
        /// <example> Input, Output, InputOutput</example>
        public ParameterDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        /// <summary>
        /// The direction attribute of the XML parameter.
        /// </summary>
        /// <example> Input, Output, InputOutput</example>
        public string DirectionAttribute
        {
            get { return directionAttribute; }
        }

        /// <summary>
        /// Defines a field/property get accessor
        /// </summary>
        public IGetAccessor GetAccessor
        {
            get { return getAccessor; }
        }

        /// <summary>
        /// Tell if a nullValue is defined._nullValue!=null
        /// </summary>
        public bool HasNullValue
        {
            get { return (nullValue != null); }
        }

        /// <summary>
        /// Indicate if we have a complex member name as [avouriteLineItem.Id]
        /// </summary>
        public bool IsComplexMemberName
        {
            get { return isComplexMemberName; }
        }

        /// <summary>
        /// Null value replacement.
        /// </summary>
        /// <example>"no_email@provided.com"</example>
        public string NullValue
        {
            get { return nullValue; }
        }

        /// <summary>
        /// Column Precision.
        /// </summary>
        public byte Precision
        {
            get { return precision; }
        }

        /// <summary>
        /// Property name used to identify the property amongst the others.
        /// </summary>
        /// <example>EmailAddress</example>
        public string PropertyName
        {
            get { return _currentPropertyName; }
        }

        /// <summary>
        /// Column Scale.
        /// </summary>
        public byte Scale
        {
            get { return scale; }
        }

        /// <summary>
        /// Column size.
        /// </summary>
        [XmlAttribute("size")]
        public int Size
        {
            get { return size; }
        }

        /// <summary>
        /// The typeHandler used to work with the parameter.
        /// </summary>
        public ITypeHandler TypeHandler
        {
            get { return typeHandler; }
            set { typeHandler = value; }
        }

        public ParameterProperty Clone()
        {
            return new ParameterProperty(
                   PropertyName, columnName, callBackName, clrType, dbType,
                   directionAttribute, nullValue, precision, scale, size,
                   _parameterClass, _dataExchangeFactory);
        }

        #endregion Properties

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterProperty"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="callBackName">Name of the call back.</param>
        /// <param name="clrType">Type of the CLR.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <param name="directionAttribute">The direction attribute.</param>
        /// <param name="nullValue">The null value.</param>
        /// <param name="precision">The precision.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="size">The size.</param>
        /// <param name="parameterClass">The parameter class.</param>
        /// <param name="dataExchangeFactory">The data exchange factory.</param>
        public ParameterProperty(
            string propertyName,
            string columnName,
            string callBackName,
            string clrType,
            string dbType,
            string directionAttribute,
            string nullValue,
            Byte precision,
            Byte scale,
            int size,
            Type parameterClass,
            DataExchangeFactory dataExchangeFactory
            )
        {
            Contract.Require.That(propertyName, Is.Not.Null & Is.Not.Empty).When("retrieving argument propertyName in ParameterProperty constructor");

            this.columnName = columnName;
            this.callBackName = callBackName;
            this.clrType = clrType;
            this.dbType = dbType;
            this.nullValue = nullValue;
            this.precision = precision;
            this.scale = scale;
            this.size = size;
            this._parameterClass = parameterClass;
            this._dataExchangeFactory = dataExchangeFactory;

            #region Direction

            if (directionAttribute.Length > 0)
            {
                direction = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), directionAttribute, true);
                this.directionAttribute = direction.ToString();
            }

            #endregion Direction

            #region Property Name

            this._currentPropertyName = propertyName;
            this._propertyPlaceholder = propertyName;

            if (propertyName.IndexOf('.') < 0)
            {
                isComplexMemberName = false;
            }
            else // complex member name FavouriteLineItem.Id
            {
                isComplexMemberName = true;
            }

            #endregion Property Name

            #region GetAccessor

            if (!typeof(IDictionary).IsAssignableFrom(parameterClass) // Hashtable parameter map
                && parameterClass != null // value property
                && !dataExchangeFactory.TypeHandlerFactory.IsSimpleType(parameterClass)) // value property
            {
                if (!isComplexMemberName)
                {
                    IGetAccessorFactory getAccessorFactory = dataExchangeFactory.AccessorFactory.GetAccessorFactory;
                    getAccessor = getAccessorFactory.CreateGetAccessor(parameterClass, propertyName);
                }
                else // complex member name FavouriteLineItem.Id
                {
                    string memberName = propertyName.Substring(propertyName.LastIndexOf('.') + 1);
                    string parentName = propertyName.Substring(0, propertyName.LastIndexOf('.'));
                    Type parentType = ObjectProbe.GetMemberTypeForGetter(parameterClass, parentName);

                    IGetAccessorFactory getAccessorFactory = dataExchangeFactory.AccessorFactory.GetAccessorFactory;
                    getAccessor = getAccessorFactory.CreateGetAccessor(parentType, memberName);
                }
            }

            #endregion GetAccessor

            #region TypeHandler

            if (CallBackName.Length > 0)
            {
                try
                {
                    Type type = dataExchangeFactory.TypeHandlerFactory.GetType(CallBackName);
                    ITypeHandlerCallback typeHandlerCallback = (ITypeHandlerCallback)Activator.CreateInstance(type);
                    typeHandler = new CustomTypeHandler(typeHandlerCallback);
                }
                catch (Exception e)
                {
                    throw new ConfigurationException("Error occurred during custom type handler configuration.  Cause: " + e.Message, e);
                }
            }
            else
            {
                if (CLRType.Length == 0)  // Unknown
                {
                    if (getAccessor != null &&
                        dataExchangeFactory.TypeHandlerFactory.IsSimpleType(getAccessor.MemberType))
                    {
                        // Primitive
                        typeHandler = dataExchangeFactory.TypeHandlerFactory.GetTypeHandler(getAccessor.MemberType, dbType);
                    }
                    else
                    {
                        typeHandler = dataExchangeFactory.TypeHandlerFactory.GetUnkownTypeHandler();
                    }
                }
                else // If we specify a CLR type, use it
                {
                    Type type = TypeUtils.ResolveType(CLRType);

                    if (dataExchangeFactory.TypeHandlerFactory.IsSimpleType(type))
                    {
                        // Primitive
                        typeHandler = dataExchangeFactory.TypeHandlerFactory.GetTypeHandler(type, dbType);
                    }
                    else
                    {
                        // .NET object
                        type = ObjectProbe.GetMemberTypeForGetter(parameterClass, PropertyName);
                        typeHandler = dataExchangeFactory.TypeHandlerFactory.GetTypeHandler(type, dbType);
                    }
                }
            }

            #endregion TypeHandler
        }

        #region Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"></see> is equal to the current <see cref="System.Object"></see>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"></see> to compare with the current <see cref="System.Object"></see>.</param>
        /// <returns>
        /// true if the specified <see cref="System.Object"></see> is equal to the current <see cref="System.Object"></see>; otherwise, false.
        /// </returns>
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType()) return false;
            ParameterProperty p = (ParameterProperty)obj;
            return (PropertyName == p.PropertyName);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. <see cref="System.Object.GetHashCode"></see> is suitable for use in hashing algorithms and data structures like a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="System.Object"></see>.
        /// </returns>
        public override int GetHashCode()
        {
            return _propertyPlaceholder.GetHashCode();
        }

        internal void ApplyIteratePropertyReferenceHandling(SqlTagContext ctx, SqlText sqlText)
        {
            if (ctx == null)
                throw new ArgumentNullException("ctx");
            if (sqlText == null)
                throw new ArgumentNullException("sqlText");

            if (_propertyPlaceholder != null)
            {
                var parsedPropertyName = ReflectionMapper.GetReflectedFullName(ctx, sqlText, this._propertyPlaceholder);

                this._currentPropertyName = parsedPropertyName;
            }
        }

        internal void ReplaceBindingName(BindingReplacement replacement)
        {
            if (replacement == null)
                throw new ArgumentNullException("replacement");

            this._currentPropertyName = this._propertyPlaceholder;

            replacement.Replace(ref _currentPropertyName);
        }

        #endregion Methods
    }
}