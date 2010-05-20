using System.Configuration;
using IBatisNet.DataAccess.Configuration;
using NUnit.Framework;

namespace IBatisNet.DataAccess.Test.NUnit.DaoTests
{
	/// <summary>
	/// Summary description for SqlMapDaoTesto.
	/// </summary>
	public class SqlMapDaoTest : BaseDaoTest
	{
		/// <summary>
		/// Initialisation
		/// </summary>
		[TestFixtureSetUp] 
		public void FixtureSetUp() 
		{
			DomDaoManagerBuilder builder = new DomDaoManagerBuilder();
			builder.Configure( "dao"+ "_" + ConfigurationSettings.AppSettings["database"] + "_"
				+ ConfigurationSettings.AppSettings["providerType"] + ".config" );
			daoManager = DaoManager.GetInstance("SqlMapDao");		
		}

		/// <summary>
		/// Initialisation
		/// </summary>
		[SetUp] 
		public void SetUp() 
		{			
			InitScript( daoManager.LocalDataSource, ScriptDirectory + "account-init.sql" );
		}

	}
}
