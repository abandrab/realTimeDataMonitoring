﻿using MongoDB.Driver;
using System.Configuration;

namespace MongoDb.Access
{
    public class MongoDataContext
    {
        public MongoDataContext()
            : this("MongoDb")
        {
        }

        public MongoDataContext(string connectionName)
        {
            var url = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

            var mongoUrl = new MongoUrl(url);
            IMongoClient client = new MongoClient(mongoUrl);
            MongoDatabase = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase MongoDatabase { get; }
    }
}
