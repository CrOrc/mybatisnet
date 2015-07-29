using System;
using System.IO;
using System.Reflection;
using MyBatis.Common.Data;
using MyBatis.Common.Resources;
using MyBatis.Common.Utilities;
using MyBatis.DataMapper.Sqlite.Test.Helpers;

namespace MyBatis.DataMapper.Sqlite.Test.Fixtures
{
    public abstract class ScriptBase
    {
        protected readonly string scriptDirectory;
        protected readonly string basePath;
        private static bool changedAppBase;

        protected ScriptBase()
        {
            SetApplicationBase();
            basePath = Utility.GetAssemblyRelativeBasePath("Maps/Account.xml");
            scriptDirectory = new Uri(Path.Combine(Resources.ApplicationBase,Path.Combine(basePath, "Scripts"))).LocalPath;

        }

        private static void SetApplicationBase()
        {
            if (changedAppBase) return;
            var assembly = Assembly.GetExecutingAssembly();
            var applicationBase = assembly.CodeBase.Substring(0,
                assembly.CodeBase.Length -
                assembly.ManifestModule.Name.Length);
            AppDomain.CurrentDomain.SetData("APPBASE", applicationBase);
            changedAppBase = true;
        }


        /// <summary>
        /// Run a sql batch for the datasource.
        /// </summary>
        /// <param name="datasource">The datasource.</param>
        /// <param name="script">The sql batch</param>
        public void InitScript(IDataSource datasource, string script)
        {
            InitScript(datasource, script, true);
        }

        /// <summary>
        /// Run a sql batch for the datasource.
        /// </summary>
        /// <param name="datasource">The datasource.</param>
        /// <param name="script">The sql batch</param>
        /// <param name="doParse">parse out the statements in the sql script file.</param>
        private void InitScript(IDataSource datasource, string script, bool doParse)
        {
            if (Uri.IsWellFormedUriString(script,UriKind.Relative))
                script = Path.Combine(scriptDirectory, script);
            ScriptRunner runner = new ScriptRunner();

            runner.RunScript(datasource, script, doParse);
        }
    }
}
