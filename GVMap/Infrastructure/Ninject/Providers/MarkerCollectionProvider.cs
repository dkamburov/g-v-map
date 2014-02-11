namespace GVMap.Infrastructure.Ninject.Providers
{
    using System.Configuration;
    using GVMap.Models;
    using global::Ninject.Activation;
    using MongoDB.Driver;

    public class MarkerCollectionProvider : Provider<MongoCollection<MarkerModel>>
    {
        protected override MongoCollection<MarkerModel> CreateInstance(IContext context)
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(ConfigurationManager.AppSettings["Datebase"]);

            MongoCollection<MarkerModel> collection = database.GetCollection<MarkerModel>(ConfigurationManager.AppSettings["MarkerTableName"]);

            return collection;
        }
    }
}