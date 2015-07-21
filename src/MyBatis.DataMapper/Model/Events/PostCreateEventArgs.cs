
#region Apache Notice
/*****************************************************************************
 * $Revision: 408099 $
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
#endregion

using System;
using MyBatis.DataMapper.Model.ResultMapping;

namespace MyBatis.DataMapper.Model.Events
{
    /// <summary>
    /// Event launchs after creating an instance of the <see cref="IResultMap"/> object.
    /// </summary>
    public sealed class PostCreateEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the newly created instance.
        /// </summary>
        /// <value>The instance.</value>
        public object Instance { get; set; }
    }
}
