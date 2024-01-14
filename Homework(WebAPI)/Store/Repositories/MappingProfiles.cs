using AutoMapper;
using NetStore.Models;
using NetStore.Models.DTO;

namespace NetStore.Repositories
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, DTOProduct>(MemberList.Destination).ReverseMap();
            CreateMap<Group, DTOGroup>(MemberList.Destination).ReverseMap();
            CreateMap<Warehouse, DTOWarehouse>(MemberList.Destination).ReverseMap();
        }
    }
}
