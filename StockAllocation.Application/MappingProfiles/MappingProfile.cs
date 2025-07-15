using AutoMapper;
using StockAllocation.Application.DTOs;
using StockAllocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CustomerOrder mappings
            CreateMap<CustomerOrder, CustomerOrderOutputDto>();
            CreateMap<CustomerOrderInputDto, CustomerOrder>();

            // LineItem mappings
            CreateMap<LineItem, LineItemOutputDto>();
            CreateMap<LineItemInputDto, LineItem>();

            // SKU mappings
            CreateMap<SKU, SkuOutputDto>();
            CreateMap<SkuInputDto, SKU>();

            // Location mappings
            CreateMap<Location, LocationOutputDto>();
            CreateMap<LocationInputDto, Location>();
        }
    }
}
