using AutoMapper;
using BusinessLayer.Models;

namespace Task.Common
{
    /// <summary>
    /// AutoMapper profile
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Profile
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<Hotels, HotelsDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();

        }
    }
}