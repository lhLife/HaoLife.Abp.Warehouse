using HaoLife.Abp.Warehouse.Samples;
using Xunit;

namespace HaoLife.Abp.Warehouse.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<WarehouseMongoDbTestModule>
{

}
