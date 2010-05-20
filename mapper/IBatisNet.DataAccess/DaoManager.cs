
#region Apache Notice
/*****************************************************************************
 * $Header: $
 * $Revision: $
 * $Date: $
 * 
 * iBATIS.NET Data Mapper
 * Copyright (C) 2004 - Gilles Bayon
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

#region Using

using System;
using System.Collections.Specialized;
using System.Data;
using System.Threading;
using System.Xml;
using IBatisNet.Common;
using IBatisNet.Common.Exceptions;
using IBatisNet.Common.Utilities;
using IBatisNet.DataAccess.Configuration;
using IBatisNet.DataAccess.Exceptions;
using IBatisNet.DataAccess.Interfaces;
using IBatisNet.DataAccess.SessionContainer;

#endregion

namespace IBatisNet.DataAccess
{

	/// <summary>
	/// DaoManager is a facade class that provides convenient access to the rest
	/// of the DAO framework.  It's primary responsibilities include:
	///    - Reading configuration information and initializing the framework 
	///    - Managing different contexts for different configurations
	///    - Providing access to Dao implementation
	///    - Providing access to the DaoSession pool for connections, transactions
	/// </summary>
	/// <example>
	/// <pre/>
	/// <b>Exemple 1:</b>
	/// DaoManager daoManager = DaoManager.GetInstance("PetStore"); 
	/// ICategoryDao categoryDao = (ICategoryDao) daoManager.GetDao("Category");
	/// DaoSession daoSession = daoManager.GetDaoSession();
	/// daoSession.OpenConnection();
	/// ArrayList categoryList = categoryGetCategoryList(5,daoSession);
	/// daoSession.CloseConnection(daoSession);
	/// <p/>
	/// <b>Exemple 2:</b>
	/// DaoManager daoManager = DaoManager.GetInstance("PetStore"); 
	/// ICategoryDao categoryDao = (ICategoryDao) daoManager.GetDao("Category");
	/// daoManager.OpenConnection();
	/// ArrayList categoryList = categoryGetCategoryList(5);
	/// daoManager.CloseConnection();
	/// 
	/// <b>Exemple 3:</b>
	/// Product p1 = new Product();
	/// Product p2 = new Product();
	/// Category c 1= new Category()
	/// c1.Add(p1);
	/// c2.Add(p2);
	/// <p/>
	/// DaoManager daoManager = DaoManager.GetInstance("PetStore"); 
	/// ICategoryDao categoryDao = (ICategoryDao) daoManager.GetDao("Category");
	/// IProductDao productDao = (IProductDao) daoManager.GetDao("Product");
	/// daoManager.BeginTransaction();
	/// try
	/// {
	///	productInsert(p1);
	///	productInsert(p2);
	///	categoryInsert(c1);
	///	daoManager.CommitTransaction();
	/// }
	///catch
	///{
	///	daoManager.RollBackTransaction();
	///}<pre/>
	///</example>
	public class DaoManager
	{
		#region Constants
		/// <summary>
		/// Key for default context name
		/// </summary>
		public static string DEFAULT_CONTEXT_NAME = "_DEFAULT_CONTEXT_NAME";
		#endregion

		#region Fields
		/// <summary>
		/// 
		/// </summary>
		///<remarks>
		///(contextName, daoManager)
		///</remarks>
		protected static HybridDictionary DaoContextMap = new HybridDictionary();

		private DataSource _dataSource = null;
		private Provider _provider = null;
		private string _name = string.Empty;
		private IDaoSessionHandler _daoSessionHandler = null;
		private bool _isDefault = false;
		//(daoName, IDao)
		private HybridDictionary _daoMap = new HybridDictionary();
		//(dao implementation, Dao)
		private static HybridDictionary _daoImplementationMap = new HybridDictionary();

		/// <summary>
		/// Container session unique for each thread. 
		/// </summary>
		private ISessionContainer _sessionContainer = null;
		#endregion

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		internal DataSource DataSource
		{
			get { return _dataSource; }
			set { _dataSource = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		internal Provider Provider
		{
			get { return _provider; }
			set { _provider = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>
		public DataSource LocalDataSource
		{
			get { return _dataSource; }
		}

		/// <summary>
		/// DaoManger name
		/// </summary>
		internal string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		internal IDaoSessionHandler DaoSessionHandler
		{
			get { return _daoSessionHandler; }
			set { _daoSessionHandler = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		internal bool IsDefault
		{
			get { return _isDefault; }
			set { _isDefault = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		internal HybridDictionary DaoMap
		{
			get { return _daoMap; }
			set { _daoMap = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		public IDalSession LocalDaoSession
		{
			get 
			{ 
				if (_sessionContainer.LocalSession == null) 
				{
					throw new DataAccessException("DaoManager could not invoke LocalDaoSession. No DaoSession was started. Call OpenConnection() or BeginTransaction first.");
				}
				return _sessionContainer.LocalSession;
			}
		}

		#endregion

		#region Constructor (s) / Destructor
		/// <summary>
		/// Make the default constructor private to prevent
		/// instances from being created.
		/// </summary>
		private DaoManager() 
		{ 
			_sessionContainer = SessionContainerFactory.GetSessionContainer(this.Name);
		}
		#endregion

		#region Methods

		#region Configure

		/// <summary>
		/// Configure an DaoManager from via the default file config.
		/// (accesd as relative ressource path from your Application root)
		/// </summary>
		[Obsolete("This method will be remove in future version, use DomSqlMapBuilder.Configure.", false)]
		public static void Configure()
		{
			Configure( DomDaoManagerBuilder.DEFAULT_FILE_CONFIG_NAME );
		}


		/// <summary>
		/// Configure an DaoManager from via a file.
		/// </summary>
		/// <param name="resource">
		/// A relative ressource path from your Application root.
		/// </param>
		[Obsolete("This method will be remove in future version, use DomSqlMapBuilder.Configure.", false)]
		public static void Configure(string resource)
		{
			XmlDocument document = Resources.GetResourceAsXmlDocument( resource );
			new DomDaoManagerBuilder().BuildDaoManagers( document, false );
		}


		/// <summary>
		/// Configure and monitor the configuration file for modifications and 
		/// automatically reconfigure  
		/// </summary>
		/// <param name="configureDelegate">
		/// Delegate called when a file is changed to rebuild the 
		/// </param>
		[Obsolete("This method will be remove in future version, use DomSqlMapBuilder.Configure.", false)]
		public static void ConfigureAndWatch(ConfigureHandler configureDelegate)
		{
			ConfigureAndWatch( DomDaoManagerBuilder.DEFAULT_FILE_CONFIG_NAME, configureDelegate );
		}


		/// <summary>
		/// Configure and monitor the configuration file for modifications and 
		/// automatically reconfigure  
		/// </summary>
		/// <param name="resource">
		/// A relative ressource path from your Application root.
		/// </param>
		///<param name="configureDelegate">
		/// Delegate called when the file has changed, to rebuild the dal.
		/// </param>
		[Obsolete("This method will be remove in future version, use DomSqlMapBuilder.Configure.", false)]
		public static void ConfigureAndWatch(string resource, ConfigureHandler configureDelegate)
		{
			ConfigWatcherHandler.ClearFilesMonitored();
			ConfigWatcherHandler.AddFileToWatch( Resources.GetFileInfo( resource ) );

			XmlDocument document = Resources.GetConfigAsXmlDocument( resource );
			new DomDaoManagerBuilder().BuildDaoManagers( document, true );

			TimerCallback callBakDelegate = new TimerCallback( DomDaoManagerBuilder.OnConfigFileChange );

			StateConfig state = new StateConfig();
			state.FileName = resource;
			state.ConfigureHandler = configureDelegate;

			new ConfigWatcherHandler( callBakDelegate, state );
		}

		#endregion

		#region Static

		/// <summary>
		/// Cleared all reference to 
		/// </summary>
		internal static void Reset()
		{
			//DaoManagerReverseLookup.Clear();
			DaoContextMap.Clear();
		}

		/// <summary>
		/// Create anew instance of a DaoManager
		/// </summary>
		/// <returns>A DaoManager.</returns>
		internal static DaoManager NewInstance() 
		{
			return new DaoManager();
		}

		/// <summary>
		/// Gets the default DaoManager.
		/// </summary>
		/// <returns>A DaoManager.</returns>
		public static DaoManager GetInstance() 
		{
			return (DaoManager)DaoContextMap[DEFAULT_CONTEXT_NAME];
		}

		/// <summary>
		/// Gets a DaoManager registered with the specified id.
		/// </summary>
		/// <param name="contextName">The name of the DaoManger.</param>
		/// <returns>A DaoManager.</returns>
		public static DaoManager GetInstance(string contextName) 
		{
			return (DaoManager) DaoContextMap[contextName];
		}

		/// <summary>
		/// Get the DaoManager associated with this a Dao instance
		/// </summary>
		/// <param name="dao">A Dao instance.</param>
		/// <returns>A DaoManager</returns>
		public static DaoManager GetInstance(IDao dao) 
		{
			Dao daoImplementation = _daoImplementationMap[dao] as Dao;
			return daoImplementation.DaoManager;
		}

		/// <summary>
		/// Register a DaoManager
		/// </summary>
		/// <param name="contextName"></param>
		/// <param name="daoManager"></param>
		internal static void RegisterDaoManager(string contextName, DaoManager daoManager) 
		{
			if ( DaoContextMap.Contains(contextName) ) 
			{
				throw new DataAccessException("There is already a DAO Context with the ID '" + contextName + "'.");
			}
			DaoContextMap.Add(contextName, daoManager);

			if (daoManager.IsDefault==true) 
			{
				if (DaoContextMap[DEFAULT_CONTEXT_NAME] == null) 
				{
					DaoContextMap.Add(DEFAULT_CONTEXT_NAME, daoManager);
				} 
				else 
				{
					throw new DataAccessException("Error while configuring DaoManager.  There can be only one default DAO context.");
				}
			}
		}

		#endregion

		#region Work with DaoSession

		/// <summary>
		/// Get a new DaoSession
		/// </summary>
		/// <returns></returns>
		public DaoSession GetDaoSession() 								   										   
		{
			if (_daoSessionHandler == null) 
			{
				throw new DataAccessException("DaoManager could not get DaoSession. DaoSessionHandler was null (possibly not configured).");
			}
			return _daoSessionHandler.GetDaoSession(this);
		}

		/// <summary>
		/// Check if a DaoSession is started.
		/// </summary>
		/// <returns>True or False</returns>
		public bool IsDaoSessionStarted()
		{
			return (_sessionContainer.LocalSession != null);
		}

		/// <summary>
		/// Open a connection.
		/// </summary>
		/// <returns>A IDalSession.</returns>
		public IDalSession OpenConnection() 
		{
			if (_daoSessionHandler== null) 
			{
				throw new DataAccessException("DaoManager could not get DaoSession.  DaoSessionPool was null (possibly not configured).");
			}
			if (_sessionContainer.LocalSession != null) 
			{
				throw new DataAccessException("DaoManager could not invoke OpenConnection(). A connection is already started. Call CloseConnection first.");
			}
			IDalSession session = _daoSessionHandler.GetDaoSession(this);
			_sessionContainer.Store(session);
			session.OpenConnection();
			return session;
		}

		/// <summary>
		/// Close a connection
		/// </summary>
		public void CloseConnection()
		{
			if (_sessionContainer.LocalSession == null) 
			{
				throw new DataAccessException("DaoManager could not invoke CloseConnection(). No connection was started. Call OpenConnection() first.");
			}
			try
			{
				IDalSession session = _sessionContainer.LocalSession;
				session.CloseConnection();	
			} 
			catch(Exception ex)
			{
				throw new DataAccessException("DaoManager could not CloseConnection(). Cause :"+ex.Message, ex);
			}
			finally 
			{
				_sessionContainer.Dispose();
			}
		}

		/// <summary>
		/// Begins a database transaction.
		/// </summary>
		/// <returns>A IDalSession</returns>
		public IDalSession BeginTransaction() 
		{
			if (_daoSessionHandler == null) 
			{
				throw new DataAccessException("DaoManager could not get DaoSession.  DaoSessionPool was null (possibly not configured).");
			}
			if (_sessionContainer.LocalSession != null) 
			{
				throw new DataAccessException("DaoManager could not invoke BeginTransaction(). A DaoSession is already started. Call CommitTransaction() or RollbackTransaction first.");
			}
			IDalSession session = _daoSessionHandler.GetDaoSession(this);
			_sessionContainer.Store(session);
			session.BeginTransaction();
			return session;
		}

		/// <summary>
		/// Begins a database transaction with the specified isolation level.
		/// </summary>
		/// <param name="isolationLevel">
		/// The isolation level under which the transaction should run.
		/// </param>
		/// <returns>A IDalSession.</returns>
		public IDalSession BeginTransaction(IsolationLevel isolationLevel) 
		{
			if (_daoSessionHandler == null) 
			{
				throw new DataAccessException("DaoManager could not get DaoSession.  DaoSessionPool was null (possibly not configured).");
			}
			if (_sessionContainer.LocalSession != null) 
			{
				throw new DataAccessException("DaoManager could not invoke BeginTransaction(). A DaoSession is already started. Call CommitTransaction() or RollbackTransaction first.");
			}

			IDalSession session = _daoSessionHandler.GetDaoSession(this);
			_sessionContainer.Store(session);
			session.BeginTransaction(isolationLevel);
			return session;
		}

		/// <summary>
		/// Commits the database transaction.
		/// </summary>
		/// <remarks>
		/// Close the connection.
		/// </remarks>
		public void CommitTransaction()
		{
			if (_sessionContainer.LocalSession == null) 
			{
				throw new DataAccessException("DaoManager could not invoke CommitTransaction(). No Transaction was started. Call BeginTransaction() first.");
			}
			try
			{
				IDalSession session = _sessionContainer.LocalSession;
				session.CommitTransaction();
			} 
			finally 
			{
				_sessionContainer.Dispose();
			}
		}

		/// <summary>
		/// Rolls back a transaction from a pending state.
		/// </summary>
		/// <remarks>
		/// Close the connection.
		/// </remarks>
		public void RollBackTransaction()
		{
			if (_sessionContainer.LocalSession == null) 
			{
				throw new DataAccessException("DaoManager could not invoke RollBackTransaction(). No Transaction was started. Call BeginTransaction() first.");
			}
			try
			{
				IDalSession session = _sessionContainer.LocalSession;
				session.RollBackTransaction();	
			} 
			finally 
			{
				_sessionContainer.Dispose();
			}
		}

		#endregion

		#region IDao access
		/// <summary>
		/// Gets a Dao instance for the requested interface type.
		/// </summary>
		public IDao this[Type daoInterface]
		{
			get
			{
				Dao dao = _daoMap[daoInterface] as Dao;
				if (dao == null) 
				{
					throw new DataException("There is no DAO implementation found for " + daoInterface.Name + " in this context.");
				}
				IDao idao = dao.Proxy;
				return idao;
			}
		}

		/// <summary>
		/// Gets a Dao instance for the requested interface type.
		/// </summary>
		/// <param name="daoInterface">The requested interface type.</param>
		/// <returns>A Dao instance</returns>
		public IDao GetDao(Type daoInterface)
		{
			return this[daoInterface];
		}

		/// <summary>
		/// Register a dao
		/// </summary>
		/// <param name="dao"></param>
		internal void RegisterDao(Dao dao) 
		{
			if ( DaoMap.Contains(dao.DaoInterface) ) 
			{
				throw new DataException("More than one implementation for '" + dao.Interface + "' was configured.  " +
					"Only one implementation per context is allowed.");
			}
			DaoMap.Add(dao.DaoInterface, dao);

			_daoImplementationMap.Add(dao.Proxy, dao);
			_daoImplementationMap.Add(dao.DaoInstance, dao);
		}
		#endregion

		#endregion


	}
}
