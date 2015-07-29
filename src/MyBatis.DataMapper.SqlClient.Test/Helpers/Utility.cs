using System;
using System.IO;
using System.Reflection;

namespace MyBatis.DataMapper.SqlClient.Test.Helpers
{
    internal class Utility
    {
        public static string GetAssemblyRelativeBasePath(string relPathToSearch)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var applicationBase = assembly.CodeBase.Substring(0,
                assembly.CodeBase.Length -
                assembly.ManifestModule.Name.Length);
            var applicationBaseUri = new Uri(applicationBase);
            var
                basePath = applicationBaseUri.LocalPath;
            var di = new DirectoryInfo(basePath);
            while (di != null)
            {
                if (!di.Exists) return null;
                var path = Path.Combine(di.FullName, relPathToSearch);
                var fi = new FileInfo(path);
                if (fi.Exists) break;
                di = di.Parent;
            }
            var ret1 = new Uri(applicationBaseUri.Scheme + Uri.SchemeDelimiter + di.FullName + "/.");
            return applicationBaseUri.MakeRelative(ret1);
        }

    }
}
