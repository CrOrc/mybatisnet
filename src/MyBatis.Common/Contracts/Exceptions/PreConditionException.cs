﻿#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 383115 $
 * $Date: 2008-06-28 09:26:16 -0600 (Sat, 28 Jun 2008) $
 * 
 * MyBatis.NET Data Mapper
 * Copyright (C) 2008/2005 - Apache Fondation
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

namespace MyBatis.Common.Contracts.Exceptions
{
    /// <summary>
    /// Design By ContractException raised when a precondition fails.
    /// </summary>
    public class PreConditionException : DesignByContractException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreConditionException"/> class.
        /// </summary>
        public PreConditionException()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PreConditionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public PreConditionException(string message): base(message)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PreConditionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public PreConditionException(string message, Exception inner): base(message, inner)
        {}
    }
}
