
#region Apache Notice
/*****************************************************************************
 * $Revision: 408164 $
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

using MyBatis.DataMapper.Model.Sql.Dynamic.Elements;
using MyBatis.Common.Utilities.Objects.Members;

namespace MyBatis.DataMapper.Model.Sql.Dynamic.Handlers
{
	/// <summary>
	/// Summary description for IsNotNullTagHandler.
	/// </summary>
	public sealed class IsNotNullTagHandler : IsNullTagHandler
	{


        /// <summary>
        /// Initializes a new instance of the <see cref="IsNotNullTagHandler"/> class.
        /// </summary>
        /// <param name="accessorFactory">The accessor factory.</param>
        public IsNotNullTagHandler(AccessorFactory accessorFactory)
            : base(accessorFactory)
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ctx"></param>
		/// <param name="tag"></param>
		/// <param name="parameterObject"></param>
		/// <returns></returns>
		public override bool IsCondition(SqlTagContext ctx, SqlTag tag, object parameterObject)
		{
			return !base.IsCondition(ctx, tag, parameterObject);
		}
	}
}
