using System.Collections;

namespace MyBatis.DataMapper.SqlClient.Test.Domain
{
	/// <summary>
	/// Description r�sum�e de Complex.
	/// </summary>
	public class Complex
	{
		private Hashtable _map = null;

		public Hashtable Map 
		{
			get
			{
				return _map;
			}
			set
			{
				_map = value;
			}
		}

	}
}
