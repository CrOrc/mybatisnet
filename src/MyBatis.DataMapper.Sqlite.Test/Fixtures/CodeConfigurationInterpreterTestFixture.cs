﻿using System;
using System.Collections;
using MyBatis.DataMapper;
using MyBatis.DataMapper.Configuration;
using MyBatis.DataMapper.Configuration.Interpreters.Config.Xml;
using MyBatis.Common.Configuration;
using MyBatis.Common.Data;
using MyBatis.DataMapper.Sqlite.Test.Domain;
using NUnit.Framework;

namespace MyBatis.DataMapper.Sqlite.Test.Fixtures
{
    [TestFixture]
    public class CodeConfigurationInterpreterTestFixture : BaseTest
    {
        [Test]
        public void CodeConfigurationMatchesSqlMapConfig()
        {
            #region SqlMapConfig.xml
            /*
            <sqlMapConfig xmlns="http://ibatis.apache.org/dataMapper" 
            xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" >
	            <settings>
		            <setting useStatementNamespaces="true"/>
		            <setting cacheModelsEnabled="false"/>
		            <setting validateSqlMap="false"/>
		            <setting useReflectionOptimizer="false"/>
		            <setting preserveWhitespace="false"/>
	            </settings>	
	            <providers uri="file://providers.config"/>
	            <database>
		            <provider name="SQLite3"/>
		            <dataSource name="ibatisnet.sqlmap" connectionString="Data Source=ibatisnet.sqlite;Version=3;"/>
	            </database>  
	            <alias>
		            <typeAlias alias="Account" type="MyBatis.DataMapper.Sqlite.Test.Domain.Account, MyBatis.DataMapper.Sqlite.Test"/>
	            </alias>  
              <sqlMaps>
		            <sqlMap uri="file://../../Maps/Account.xml"/>
              </sqlMaps>
            </sqlMapConfig>
            */
            #endregion

            // slightly awkward to creating ConfigurationSetting, then engine, then interpreter ???

            ConfigurationSetting settings = new ConfigurationSetting();
            settings.UseStatementNamespaces = true;
            settings.IsCacheModelsEnabled = false;
            settings.ValidateMapperConfigFile = false;
            settings.UseReflectionOptimizer = false;
            settings.PreserveWhitespace = false;

            var engine = new DefaultConfigurationEngine(settings);

            CodeConfigurationInterpreter codeConfig = new CodeConfigurationInterpreter(engine.ConfigurationStore);
            codeConfig.AddDatabase(new SqliteDbProvider(), "Data Source=ibatisnet.sqlite;Version=3;");
            codeConfig.AddAlias(typeof(Account), "Account");
            codeConfig.AddSqlMap("file://../../Maps/Account.xml", true);

            engine.RegisterInterpreter(codeConfig);
            IMapperFactory mapperFactory = engine.BuildMapperFactory();
            IDataMapper localDataMapper = ((IDataMapperAccessor)mapperFactory).DataMapper;

            IConfigurationStore store = engine.ConfigurationStore;
            IConfigurationStore baseStore = ConfigurationEngine.ConfigurationStore;

            assertConfiguration(baseStore.Properties, store.Properties);
            // assertConfiguration(baseStore.Settings, store.Settings);
            assertConfiguration(baseStore.Databases, store.Databases);
            assertConfiguration(baseStore.TypeHandlers, store.TypeHandlers);
            assertConfiguration(baseStore.Alias, store.Alias);
            assertConfiguration(baseStore.CacheModels, store.CacheModels);
            assertConfiguration(baseStore.ResultMaps, store.ResultMaps);
            assertConfiguration(baseStore.Statements, store.Statements);
            assertConfiguration(baseStore.ParameterMaps, store.ParameterMaps);

            InitScript(sessionFactory.DataSource, "../../Scripts/account-init.sql");

            ICollection items = localDataMapper.QueryForList("Account.GetAllAccounts1", null);
            Assert.IsTrue(items.Count > 1);

            items = localDataMapper.QueryForList("Account.GetAllAccounts2", null);
            Assert.IsTrue(items.Count > 1);
        }

        private static void assertConfiguration(IConfiguration[] x, IConfiguration[] y)
        {
            CollectionAssert.AreEqual(x, y, ConfigurationComparer.Instance);
        }

        class ConfigurationComparer : IComparer
        {
            public static readonly ConfigurationComparer Instance = new ConfigurationComparer();

            public int Compare(object x, object y)
            {
                IConfiguration xConfiguration = (IConfiguration)x;
                IConfiguration yConfiguration = (IConfiguration)y;

                bool same = 
                    equal(xConfiguration.Id, yConfiguration.Id) &&
                    equal(xConfiguration.Type, yConfiguration.Type) &&
                    equal(xConfiguration.Value, yConfiguration.Value);

                // mainly care about 0
                return same ? 0 : -1;
            }
        }
        
        static bool equal(IComparable x, IComparable y)
        {
            bool same = false;

            if (x == null && y == null)
            {
                same = true;
            }
            else
            {
                if (x != null && y != null)
                {
                    same = x.CompareTo(y) == 0;
                }
            }

            return same;
        }
    }
}
