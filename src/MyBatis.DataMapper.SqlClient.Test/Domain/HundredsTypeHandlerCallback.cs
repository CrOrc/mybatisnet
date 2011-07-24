using System;
using MyBatis.DataMapper.Exceptions;
using MyBatis.DataMapper.TypeHandlers;

namespace MyBatis.DataMapper.SqlClient.Test.Domain
{
	/// <summary>
	/// Description r�sum�e de HundredsTypeHandlerCallback.
	/// </summary>
	public class HundredsTypeHandlerCallback : ITypeHandlerCallback
	{

		#region ITypeHandlerCallback

		public object ValueOf(string nullValue)
		{
			if ("100".Equals(nullValue)) 
			{
				return true;
			} 
			else if ("200".Equals(nullValue)) 
			{
				return false;
			} 
			else 
			{
				throw new DataMapperException("Unexpected value " + nullValue + " found where 100 or 200 was expected.");
			}
		}

		public object GetResult(IResultGetter getter)
		{
			if (getter.Value != null && getter.Value != System.DBNull.Value) {
				int i = Convert.ToInt32(getter.Value);
				if (i == 100) 
				{
					return true;
				} 
				else if (i == 200) 
				{
					return false;
				} 
				else 
				{
					throw new DataMapperException("Unexpected value " + i + " found where 100 or 200 was expected.");
				}
			}
			else 
			{
				throw new DataMapperException("Unexpected null value found where 100 or 200 was expected.");
			}
		}

		public void SetParameter(IParameterSetter setter, object parameter)
		{
			bool b = Convert.ToBoolean(parameter);
			if (b) 
			{
				setter.Value = 100;
			} 
			else 
			{
				setter.Value = 200;
			}		
		}

        public object NullValue
        {
            get { throw new InvalidCastException("HundredsTypeHandlerCallback could not cast a null value in the field."); }
        }
		#endregion
	}
}
