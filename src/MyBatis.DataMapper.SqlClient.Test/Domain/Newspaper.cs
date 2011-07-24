namespace MyBatis.DataMapper.SqlClient.Test.Domain
{
	/// <summary>
	/// Description r�sum�e de Newspaper.
	/// </summary>
	public class Newspaper : Document
	{
		private string _city = string.Empty;

		public string City
		{
			get { return _city; }
			set { _city = value; }
		}
	}
}
