using HaoLife.Abp.Warehouse.Samples;
using Xunit;

namespace HaoLife.Abp.Warehouse.MongoDB.Samples;

[Collection(MongoTestCollection.Name)]
public class SampleRepository_Tests : SampleRepository_Tests<WarehouseMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
