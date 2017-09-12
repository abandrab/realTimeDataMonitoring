using Domain.Model.Models.MongoDB;
using MongoDb.Access.Interfaces;
using MongoDB.Driver;

namespace MongoDb.Access.Repositories
{
    public class DataRepository : BaseMongoDbRepository<Data>, IDataRepository
    {
        private const string CollectionName = "Datas";
        private MongoDataContext _dataContext;

        public DataRepository()
        {
            this._dataContext = new MongoDataContext("MongoServerSettings");
        }
        protected override IMongoCollection<Data> Collection =>
            _dataContext.MongoDatabase.GetCollection<Data>(CollectionName);


    }
}
