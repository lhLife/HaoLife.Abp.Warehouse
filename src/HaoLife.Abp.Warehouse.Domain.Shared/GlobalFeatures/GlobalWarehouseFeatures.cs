using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.GlobalFeatures;

namespace HaoLife.Abp.Warehouse.GlobalFeatures;

public class GlobalWarehouseFeatures : GlobalModuleFeatures
{
    public const string ModuleName = "Warehouse";

    public CargoCategoryFeature CargoCategory => GetFeature<CargoCategoryFeature>();
    public SupplierFeature Supplier => GetFeature<SupplierFeature>();
    public CargoTypeSpecFeature CargoTypeSpec => GetFeature<CargoTypeSpecFeature>();

    public GlobalWarehouseFeatures(GlobalFeatureManager featureManager)
        : base(featureManager)
    {
        AddFeature(new CargoCategoryFeature(this));
        AddFeature(new SupplierFeature(this));
        AddFeature(new CargoTypeSpecFeature(this));
        AddFeature(new StorehouseFeature(this));
        AddFeature(new StoreToolFeature(this));
    }
    //simple

    public virtual void Simple()
    {

    }
}
