#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: 476843 $
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


using System.Collections;

using MyBatis.DataMapper.Exceptions;
using MyBatis.DataMapper.Model;
using MyBatis.DataMapper.Model.Statements;
using MyBatis.DataMapper.Session;

namespace MyBatis.DataMapper.MappedStatements
{
	/// <summary>
	/// Summary description for DeleteMappedStatement.
	/// </summary>
    public sealed class DeleteMappedStatement : MappedStatement
	{

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteMappedStatement"/> class.
        /// </summary>
        /// <param name="modelStore">The model store.</param>
        /// <param name="statement">The statement.</param>
        public DeleteMappedStatement(IModelStore modelStore, IStatement statement)
            : base(modelStore, statement)
		{ }

		#region ExecuteQueryForMap

		/// <summary>
		/// 
		/// </summary>
		/// <param name="session"></param>
		/// <param name="parameterObject"></param>
		/// <param name="keyProperty"></param>
		/// <param name="valueProperty"></param>
		/// <returns></returns>
		public override IDictionary ExecuteQueryForMap(ISession session, object parameterObject, string keyProperty, string valueProperty )
		{
			throw new DataMapperException("Delete statements cannot be executed as a query for map.");
		}

		#endregion

		#region ExecuteInsert

		/// <summary>
		/// 
		/// </summary>
		/// <param name="session"></param>
		/// <param name="parameterObject"></param>
		/// <returns></returns>
        public override object ExecuteInsert(ISession session, object parameterObject)
		{
			throw new DataMapperException("Delete statements cannot be executed as a query insert.");
		}

		#endregion

		#region ExecuteQueryForList

		/// <summary>
		/// 
		/// </summary>
		/// <param name="session"></param>
		/// <param name="parameterObject"></param>
		/// <param name="resultObject"></param>
        public override void ExecuteQueryForList(ISession session, object parameterObject, IList resultObject)
		{
			throw new DataMapperException("Delete statements cannot be executed as a query for list.");
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="session"></param>
		/// <param name="parameterObject"></param>
		/// <returns></returns>
        public override IList ExecuteQueryForList(ISession session, object parameterObject)
		{
			throw new DataMapperException("Delete statements cannot be executed as a query for list.");
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="session"></param>
		/// <param name="parameterObject"></param>
		/// <param name="rowDelegate"></param>
		/// <returns></returns>
        public override IList ExecuteQueryForRowDelegate(ISession session, object parameterObject, RowDelegate rowDelegate)
		{
			throw new DataMapperException("Delete statements cannot be executed as a query for row delegate.");
		}

		
		#region ExecuteForObject

		/// <summary>
		/// 
		/// </summary>
		/// <param name="session"></param>
		/// <param name="parameterObject"></param>
		/// <param name="resultObject"></param>
		/// <returns></returns>
        public override object ExecuteQueryForObject(ISession session, object parameterObject, object resultObject)
		{
			throw new DataMapperException("Delete statements cannot be executed as a query for object.");
		}

		#endregion
	}
}
