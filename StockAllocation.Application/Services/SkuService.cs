using AutoMapper;
using StockAllocation.Application.DTOs;
using StockAllocation.Application.Interfaces;
using StockAllocation.Application.Pagination;
using StockAllocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Services
{
    public class SkuService : ISkuService
    {
        private readonly ISkuRepository _skuRepository;
        private readonly IMapper _mapper;

        public SkuService(ISkuRepository skuRepository, IMapper mapper)
        {
            _skuRepository = skuRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<SkuOutputDto>> GetAllAsync(PaginationRequest paging)
        {
            var totalCount = await _skuRepository.GetCountAsync();
            var items = await _skuRepository.GetAllAsync((paging.PageNumber - 1) * paging.PageSize, paging.PageSize);
            var mapped = _mapper.Map<List<SkuOutputDto>>(items);
            return new PaginationResponse<SkuOutputDto>(mapped, totalCount, paging.PageNumber, paging.PageSize);
        }

        public async Task<SkuOutputDto> GetByIdAsync(Guid id)
        {
            var sku = await _skuRepository.GetByIdAsync(id);
            if (sku == null)
                throw new KeyNotFoundException("SKU not found");

            return _mapper.Map<SkuOutputDto>(sku);
        }

        public async Task<SkuOutputDto> CreateAsync(SkuInputDto dto)
        {
            var sku = _mapper.Map<SKU>(dto);
            sku.Id = Guid.NewGuid();
            await _skuRepository.AddAsync(sku);
            return _mapper.Map<SkuOutputDto>(sku);
        }

        public async Task UpdateAsync(Guid id, SkuInputDto dto)
        {
            var sku = await _skuRepository.GetByIdAsync(id);
            if (sku == null)
                throw new KeyNotFoundException("SKU not found");

            sku.ProductNr = dto.ProductNr;
            sku.QuantityAvailable = dto.QuantityAvailable;
            sku.LocationId = dto.LocationId;

            await _skuRepository.UpdateAsync(sku);
        }

        public async Task DeleteAsync(Guid id)
        {
            var sku = await _skuRepository.GetByIdAsync(id);
            if (sku == null)
                throw new KeyNotFoundException("SKU not found");

            await _skuRepository.DeleteAsync(sku);
        }
    }
}
