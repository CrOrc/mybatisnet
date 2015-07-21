
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
#endregion

using System;

namespace MyBatis.DataMapper.Model.Cache.Decorators
{
    /// <summary>
    /// Cache decorator which deals with null value
    /// </summary>
    public sealed class NullValueDecorator : BaseCache 
    {
        private readonly ICache delegateCache = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="NullValueDecorator"/> class.
        /// </summary>
        /// <param name="delegateCache">The delegate cache.</param>
        public NullValueDecorator(ICache delegateCache)
        {
            this.delegateCache = delegateCache;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public override string Id
        {
            get { return delegateCache.Id; }
            set { delegateCache.Id = value; }
        }


        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The size.</value>
        public override int Size
        {
            get { return delegateCache.Size; }
        }

        /// <summary>
        /// Adds an item with the specified key and value into cached data.
        /// Gets a cached object with the specified key.
        /// </summary>
        /// <value>The cached object or <c>null</c></value>
        public override object this[object key]
        {
            get { return delegateCache[key];  }
            set
            {
                if (null == value) { value = CacheModel.NULL_OBJECT; }
                delegateCache[key] = value; ;
            }
        }

        /// <summary>
        /// Remove an object from a cache model
        /// </summary>
        /// <param name="key">the key to the object</param>
        /// <returns>the removed object(?)</returns>
        public override object Remove(object key)
        {
            return delegateCache.Remove(key);
        }

        /// <summary>
        /// Clears all elements from the cache.
        /// </summary>
        public override void Clear()
        {
            delegateCache.Clear();
        }

        /// <summary>
        /// Determines whether the cache contains the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// 	<c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        public override bool ContainsKey(object key)
        {
            return delegateCache.ContainsKey(key);
        }
    }
}
