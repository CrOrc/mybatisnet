namespace MyBatis.DataMapper.SqlClient.Test.Domain
{
	/// <summary>
	/// Description r�sum�e de Book.
	/// </summary>
	public class Book : Document
	{
		private int _pageNumber = -1;

		public int PageNumber
		{
			get { return _pageNumber; }
			set { _pageNumber = value; }
		}
	}
}
