using Moq;
using StockAllocation.Application.Interfaces;
using StockAllocation.Application.Services;
using StockAllocation.Domain.Enums;
using StockAllocation.Domain.Models;

public class StockAllocationServiceTests
{
    private readonly Mock<ICustomerOrderRepository> _orderRepoMock;
    private readonly Mock<ISkuRepository> _skuRepoMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly StockAllocatorService _service;

    public StockAllocationServiceTests()
    {
        _orderRepoMock = new Mock<ICustomerOrderRepository>();
        _skuRepoMock = new Mock<ISkuRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new StockAllocatorService(_orderRepoMock.Object, _skuRepoMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task AllocateStockForOrdersAsync_ShouldAllocate_WhenStockAvailable()
    {
        // Arrange
        var order = new CustomerOrder
        {
            Id = Guid.NewGuid(),
            Priority = PriorityLevel.High,
            CreatedDate = DateTime.UtcNow,
            CompleteDeliveryRequired = false,
            LineItems = new List<LineItem>
            {
                new LineItem
                {
                    Id = Guid.NewGuid(),
                    ProductNr = "P001",
                    QuantityRequested = 5,
                    QuantityAllocated = 0
                }
            }
        };

        var sku = new SKU
        {
            Id = Guid.NewGuid(),
            ProductNr = "P001",
            QuantityAvailable = 10
        };

        _orderRepoMock.Setup(r => r.GetOrdersNeedingAllocationAsync())
                      .ReturnsAsync(new List<CustomerOrder> { order });

        _skuRepoMock.Setup(r => r.GetAvailableSkusForProductAsync("P001"))
                    .ReturnsAsync(new List<SKU> { sku });

        // Act
        await _service.AllocateStockForOrdersAsync();

        // Assert
        Assert.Equal(5, order.LineItems[0].QuantityAllocated);
        Assert.Equal(5, sku.QuantityAvailable);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task AllocateStockForOrdersAsync_ShouldDeallocate_WhenCompleteDeliveryRequiredNotFulfilled()
    {
        // Arrange
        var order = new CustomerOrder
        {
            Id = Guid.NewGuid(),
            Priority = PriorityLevel.High,
            CreatedDate = DateTime.UtcNow,
            CompleteDeliveryRequired = true,
            LineItems = new List<LineItem>
            {
                new LineItem
                {
                    Id = Guid.NewGuid(),
                    ProductNr = "P002",
                    QuantityRequested = 5,
                    QuantityAllocated = 0
                }
            }
        };

        var sku = new SKU
        {
            Id = Guid.NewGuid(),
            ProductNr = "P002",
            QuantityAvailable = 2
        };

        _orderRepoMock.Setup(r => r.GetOrdersNeedingAllocationAsync())
                      .ReturnsAsync(new List<CustomerOrder> { order });

        _skuRepoMock.Setup(r => r.GetAvailableSkusForProductAsync("P002"))
                    .ReturnsAsync(new List<SKU> { sku });

        // Act
        await _service.AllocateStockForOrdersAsync();

        // Assert
        Assert.Equal(0, order.LineItems[0].QuantityAllocated);
        Assert.Equal(2, sku.QuantityAvailable);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}
