using AutoMapper;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles() 
    { 
        //SELECT
        CreateMap<Item, ItemDto>();
        CreateMap<Store, StoreDto>();
        CreateMap<Person, PersonDto>();
        CreateMap<Supplier, SupplierDto>();
        CreateMap<Procurement, ProcurementDto>();
        CreateMap<ProcurementDetail, ProcurementDetailDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDetail, OrderDetailDto>();
        CreateMap<SupplierItem, SupplierStoreItemDto>();

        //INSERT/UPDATE
        CreateMap<ChangeItemRequest, Item>();
        CreateMap<ChangeStoreRequest, Store>();
        CreateMap<ChangePersonRequest, Person>();
        CreateMap<ChangeSupplierRequest, Supplier>();
        CreateMap<SupplierStoreItemDto, SupplierItem>();
        //CreateMap<ChangeProcurementRequest, Procurement>();
        //CreateMap<ChangeProcurementDetailRequest, ProcurementDetail>();
        //CreateMap<ChangeOrderRequest, Order>();
        //CreateMap<ChangeOrderDetailRequest, OrderDetailDto>();
    }
}
