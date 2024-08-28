using AutoMapper;
using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Stocks;
using HaoLife.Abp.Warehouse.Storehouses;
using System.Linq;
using Volo.Abp.AutoMapper;

namespace HaoLife.Abp.Warehouse;

public class WarehouseApplicationAutoMapperProfile : Profile
{
    public WarehouseApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CargoCategory, CargoCategoryDto>()
            .MapExtraProperties();

        CreateMap<CargoCategory, CargoCategoryNodeDto>()
            .Ignore(a => a.IsHaveChild)
            .Ignore(a => a.Childrens)
            .MapExtraProperties();

        //CreateMap<CreateCargoCategoryDto, CargoCategory>(MemberList.Source)
        //    .MapExtraProperties();

        CreateMap<Cargo, CargoDto>()
            .Ignore(a => a.Category)
            .Ignore(a => a.Supplier)
            .MapExtraProperties();

        CreateMap<Cargo, CargoListDto>();

        CreateMap<CargoTypeSpec, CargoTypeSpecDto>()
            .ForMember(a => a.Values, a => a.MapFrom(b => b.Values.OrderBy(a => a.Sort).Select(c => c.Value)))
            .MapExtraProperties();

        CreateMap<StoreTool, StoreToolDto>()
            .ForMember(a => a.Attrs, a => a.MapFrom(b => b.Attrs.OrderBy(a => a.Sort).Select(c => c.Name)))
            .MapExtraProperties();

        CreateMap<Storehouse, StorehouseDto>()
            .MapExtraProperties();

        CreateMap<Storehouse, StorehouseListDto>();

        CreateMap<Storearea, StoreareaDto>()
            .Ignore(a => a.Storehouse)
            .MapExtraProperties();

        CreateMap<Storearea, StoreareaListDto>();

        CreateMap<Storelocation, StorelocationDto>()
            .Ignore(a => a.Storehouse)
            .Ignore(a => a.Storearea)
            .MapExtraProperties();


        CreateMap<Stock, StockDto>()
            .Ignore(a => a.Cargo)
            .MapExtraProperties();


    }
}
