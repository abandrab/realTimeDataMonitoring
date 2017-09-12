using Domain.Model.Models.MongoDB;

namespace MongoDb.Access.Interfaces
{
    public interface IDataRepository : IMongoDbRepository<Data>
    {
        
    }
}
