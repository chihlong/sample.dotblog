﻿using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Contracts;

namespace UnitTestProject2
{
    public static class DbManager
    {

        public static IDbConnection CreateConnection(string connectionStringName)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(connectionStringName));

            var connectSetting = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (connectSetting == null)
            {
                throw new ArgumentNullException(connectionStringName + " not exist at app.config");
            }

            var factory = DbProviderFactories.GetFactory(connectSetting.ProviderName);
            var connection = factory.CreateConnection();

            //TODO:連線字串解密

            connection.ConnectionString = connectSetting.ConnectionString;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

      
    }
}