namespace GVMap.Infrastructure.Ninject.Providers
{
    using System.Configuration;
    using Core.Models;
    using global::Ninject.Activation;
    using MongoDB.Driver;

    public class MarkerCollectionProvider : Provider<MongoCollection<MarkerModel>>
    {
        protected override MongoCollection<NewsFeedModel> CreateInstance(IContext context)
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase(ConfigurationManager.AppSettings["Datebase"]);

            MongoCollection<NewsFeedModel> collection = database.GetCollection<NewsFeedModel>(ConfigurationManager.AppSettings["MarkerTableName"]);

            return collection;
        }
    }
}