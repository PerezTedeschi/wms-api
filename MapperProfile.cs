using AutoMapper;
using wms_api.DTO;
using wms_api.Entities;

namespace wms_api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Warehouse, GetWarehouseDTO>();
            CreateMap<CreateWarehouseDTO, Warehouse>()
                .ForMember(dest => dest.FileMimeType, opt => opt.MapFrom(source => source.File.ContentType))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(source => source.File.FileName))
                .ForMember(dest => dest.FileContent, act => act.Ignore());
        }
    }
}
