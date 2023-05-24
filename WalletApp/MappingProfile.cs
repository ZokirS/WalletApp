using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace WalletApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RechargeOperation, RechargeOperationDto>();
        }
    }
}
