#region Apache Notice

/*****************************************************************************
 * $Header: $
 * $Revision: 474141 $
 * $Date: 2008-10-11 10:07:44 -0600 (Sat, 11 Oct 2008) $
 * 
 * MyBatis.NET Data Mapper
 * Copyright (C) 2008/2005 - The Apache Software Foundation
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using MyBatis.Common.Logging.Impl;

namespace MyBatis.Common.Logging
{
    /// <summary>
    /// Uses the specified <see cref="ILoggerFactoryAdapter" /> to create <see cref="ILog" /> instances
    /// that are used to log messages. Inspired by log4net.
    /// </summary>
    public static class LogManager
    {
        private static ILoggerFactoryAdapter _adapter = null;
        private static readonly object _loadLock = new object();
        private const string IBATIS_SECTION_LOGGING = "iBATIS/logging";
        private const string MYBATIS_SECTION_LOGGING = "MyBatis/logging";

        /// <summary>
        /// Initializes the <see cref="LogManager"/> class.
        /// </summary>
        static LogManager()
        {
            _adapter = null;
            _loadLock = new object();
        }

        /// <summary>
        /// Gets or sets the adapter.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The MyBatis.Common assembly ships with the following built-in <see cref="ILoggerFactoryAdapter" /> implementations:
        /// </para>
        ///	<list type="table">
        ///	<item><term><see cref="ConsoleOutLoggerFA" /></term><description>Writes output to Console.Out</description></item>
        ///	<item><term><see cref="TraceLoggerFA" /></term><description>Writes output to the System.Diagnostics.Trace sub-system</description></item>
        ///	<item><term><see cref="NoOpLoggerFA" /></term><description>Ignores all messages</description></item>
        ///	</list>
        /// </remarks>
        /// <value>The adapter.</value>
        public static ILoggerFactoryAdapter Adapter
        {
            get
            {
                if (_adapter == null)
                {
                    lock (_loadLock)
                    {
                        if (_adapter == null)
                        {
                            _adapter = BuildLoggerFactoryAdapter();
                        }
                    }
                }
                return _adapter;
            }
            set
            {
                lock (_loadLock)
                {
                    _adapter = value;
                }
            }
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static ILog GetLogger(Type type)
        {
            return Adapter.GetLogger(type);
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static ILog GetLogger(string name)
        {
            return Adapter.GetLogger(name);
        }

        /// <summary>
        /// Builds the logger factory adapter.
        /// </summary>
        /// <returns></returns>
        private static ILoggerFactoryAdapter BuildLoggerFactoryAdapter()
        {
            LogSetting setting = null;
            IList<Exception> errors = new List<Exception>();
            string[] probeSectionNames = new[] {MYBATIS_SECTION_LOGGING, IBATIS_SECTION_LOGGING};
            foreach (var probeSectionName in probeSectionNames)
            {
                try
                {
                    setting = (LogSetting) ConfigurationManager.GetSection(probeSectionName);
                    if (setting != null) break;
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            if (errors.Count > 0 || setting == null)
            {
                ILoggerFactoryAdapter defaultFactory = BuildDefaultLoggerFactoryAdapter();
                ILog log = defaultFactory.GetLogger(typeof (LogManager));
                foreach (var ex in errors)
                    log.Warn("Unable to read configuration.", ex);
                log.Warn(string.Format("Unable to read configuration from {0}. Using default logger.",
                    string.Join(", ", probeSectionNames)));

                return defaultFactory;
            }

            if (!typeof (ILoggerFactoryAdapter).IsAssignableFrom(setting.FactoryAdapterType))
            {
                ILoggerFactoryAdapter defaultFactory = BuildDefaultLoggerFactoryAdapter();
                ILog log = defaultFactory.GetLogger(typeof (LogManager));
                log.Warn("Type " + setting.FactoryAdapterType.FullName +
                         " does not implement ILoggerFactoryAdapter. Using default logger");
                return defaultFactory;
            }

            ILoggerFactoryAdapter instance = null;

            if (setting.Properties.Count > 0)
            {
                try
                {
                    object[] args = {setting.Properties};

                    instance = (ILoggerFactoryAdapter) Activator.CreateInstance(setting.FactoryAdapterType, args);
                }
                catch (Exception ex)
                {
                    ILoggerFactoryAdapter defaultFactory = BuildDefaultLoggerFactoryAdapter();
                    ILog log = defaultFactory.GetLogger(typeof (LogManager));
                    log.Warn(
                        "Unable to create instance of type " + setting.FactoryAdapterType.FullName +
                        ". Using default logger.", ex);
                    return defaultFactory;
                }
            }
            else
            {
                try
                {
                    instance = (ILoggerFactoryAdapter) Activator.CreateInstance(setting.FactoryAdapterType);
                }
                catch (Exception ex)
                {
                    ILoggerFactoryAdapter defaultFactory = BuildDefaultLoggerFactoryAdapter();
                    ILog log = defaultFactory.GetLogger(typeof (LogManager));
                    log.Warn(
                        "Unable to create instance of type " + setting.FactoryAdapterType.FullName +
                        ". Using default logger.", ex);
                    return defaultFactory;
                }
            }

            return instance;
        }

        /// <summary>
        /// Builds the default logger factory adapter.
        /// </summary>
        /// <returns></returns>
        private static ILoggerFactoryAdapter BuildDefaultLoggerFactoryAdapter()
        {
            return new NoOpLoggerFA();
        }
    }
}