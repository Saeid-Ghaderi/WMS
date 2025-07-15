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
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CustomerOrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<CustomerOrderOutputDto>> GetPagedOrdersAsync(PaginationRequest paging)
        {
            var totalCount = await _orderRepository.GetTotalOrdersCountAsync();
            var orders = await _orderRepository.GetPagedOrdersAsync(paging.PageNumber, paging.PageSize);
            var items = _mapper.Map<List<CustomerOrderOutputDto>>(orders);

            return new PaginationResponse<CustomerOrderOutputDto>(items, totalCount, paging.PageNumber, paging.PageSize);
        }

        public async Task<CustomerOrderOutputDto> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new KeyNotFoundException("Order not found");

            return _mapper.Map<CustomerOrderOutputDto>(order);
        }

        public async Task<CustomerOrderOutputDto> CreateOrderAsync(CustomerOrderInputDto orderDto)
        {
            var order = _mapper.Map<CustomerOrder>(orderDto);
            var created = await _orderRepository.AddAsync(order);
            return _mapper.Map<CustomerOrderOutputDto>(created);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            await _orderRepository.DeleteAsync(id);
        }
    }
}
