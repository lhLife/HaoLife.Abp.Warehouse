using HaoLife.Abp.Warehouse.MongoDB;
using HaoLife.Abp.Warehouse.Samples;
using Xunit;

namespace HaoLife.Abp.Warehouse.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<WarehouseMongoDbTestModule>
{

}
