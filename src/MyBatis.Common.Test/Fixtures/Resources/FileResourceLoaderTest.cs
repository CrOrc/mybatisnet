using System;
using System.IO;
using MyBatis.Common.Exceptions;
using MyBatis.Common.Resources;
using MyBatis.Common.Test.Helpers;
using NUnit.Framework;

namespace MyBatis.Common.Test.Fixtures.Resources
{
    [TestFixture] public class FileResourceLoaderTest
    {
        private readonly IResourceLoader loader = new FileResourceLoader();

        #region SetUp & TearDown

        /// <summary>
        /// SetUp
        /// </summary>
        [SetUp] public void SetUp()
        {
        }

        /// <summary>
        /// TearDown
        /// </summary>
        [TearDown] public void Dispose()
        {
        }

        #endregion

        [Test] public void Test_good_and_bad_file_uri_format()
        {
            Assert.IsTrue(loader.Accept(new Uri("file://something")));
            Assert.IsFalse(loader.Accept(new Uri("http://ibatis.apache.org/")));
        }

        [Test] public void Test_resource_creation_with_absolute_path()
        {
            var file = "file://" +
                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Fixtures\Utilities\sample.txt");

            var builder = new CustomUriBuilder(file, AppDomain.CurrentDomain.BaseDirectory);

            using (var resource = loader.Create(builder.Uri))
            {
                Assert.IsNotNull(resource);

                using (TextReader reader = new StreamReader(resource.Stream))
                {
                    var line = reader.ReadLine();
                    Assert.That(line, Is.EqualTo("hello"));
                }
            }
        }

        [Test] public void Test_resource_creation_with_absolute_path_and_slash()
        {
            var file = "file:///" + Common.Resources.Resources.ApplicationBase + Path.DirectorySeparatorChar +
                       "SqlMap_MSSQL_SqlClient.config";

            var builder = new CustomUriBuilder(file, AppDomain.CurrentDomain.BaseDirectory);

            using (var resource = loader.Create(builder.Uri))
            {
                Assert.IsNotNull(resource);

                using (TextReader reader = new StreamReader(resource.Stream))
                {
                    var line = reader.ReadLine();
                    Assert.That(line, Is.EqualTo(@"<?xml version=""1.0"" encoding=""utf-8""?>"));
                }
            }
        }

        [Test(Description = "Create With Relative Path")] public void Test_resource_creation_with_relative_path()
        {
            var relPath = Utility.GetAssemblyRelativeBasePath("Fixtures/Utilities/sample.txt");
            var file = relPath + @"Fixtures/Utilities/sample.txt";

            var builder = new CustomUriBuilder(file, AppDomain.CurrentDomain.BaseDirectory);

            using (var resource = loader.Create(builder.Uri))
            {
                Assert.IsNotNull(resource);

                using (TextReader reader = new StreamReader(resource.Stream))
                {
                    var line = reader.ReadLine();
                    Assert.That(line, Is.EqualTo("hello"));
                }
            }
        }

        [Test] public void Test_resource_creation_in_same_base_directory_with_Relative()
        {
            var file = "SqlMap_MSSQL_SqlClient.config";

            var builder = new CustomUriBuilder(file, AppDomain.CurrentDomain.BaseDirectory);

            using (var resource = loader.Create(builder.Uri))
            {
                Assert.IsNotNull(resource);

                using (TextReader reader = new StreamReader(resource.Stream))
                {
                    var line = reader.ReadLine();
                    Assert.That(line, Is.EqualTo(@"<?xml version=""1.0"" encoding=""utf-8""?>"));
                }
            }
        }

        [Test, ExpectedException(typeof (ResourceException))] public void
            Non_existing_resource_should_raise_ResourceException()
        {
            var file = "/Something/file1.txt";

            var builder = new CustomUriBuilder(file, AppDomain.CurrentDomain.BaseDirectory);
        }

        [Test] public void Test_resource_creation_with_space_in_path()
        {
            var path = Path.GetFullPath("spaced dir");
            Directory.CreateDirectory(path);
            var filePath = Path.Combine(path, "spaced file.txt");
            using (var text = File.CreateText(filePath))
            {
                text.WriteLine("hello world");
            }

            var builder = new CustomUriBuilder("file://" + filePath, AppDomain.CurrentDomain.BaseDirectory);

            using (var resource = loader.Create(builder.Uri))
            {
                Assert.IsNotNull(resource);

                using (TextReader reader = new StreamReader(resource.Stream))
                {
                    Assert.That(reader.ReadLine(), Is.EqualTo("hello world"));
                }
            }
        }

        [Test] public void Test_resource_creation_with_special_uri_character()
        {
            var path = Path.GetFullPath("dir#");
            Directory.CreateDirectory(path);
            var filePath = Path.Combine(path, "file.txt");
            using (var text = File.CreateText(filePath))
            {
                text.WriteLine("hello world");
            }

            var builder = new CustomUriBuilder(filePath, AppDomain.CurrentDomain.BaseDirectory);

            using (var resource = loader.Create(builder.Uri))
            {
                Assert.IsNotNull(resource);

                using (TextReader reader = new StreamReader(resource.Stream))
                {
                    Assert.That(reader.ReadLine(), Is.EqualTo("hello world"));
                }
            }
        }
    }
}