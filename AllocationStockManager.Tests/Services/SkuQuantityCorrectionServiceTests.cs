using Moq;
using StockAllocation.Application.Interfaces;
using StockAllocation.Application.Services;
using StockAllocation.Domain.Models;

public class SkuQuantityCorrectionServiceTests
{
    private readonly Mock<ISkuRepository> _skuRepoMock;
    private readonly Mock<ICustomerOrderRepository> _orderRepoMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly SkuQuantityCorrectionService _service;

    public SkuQuantityCorrectionServiceTests()
    {
        _skuRepoMock = new Mock<ISkuRepository>();
        _orderRepoMock = new Mock<ICustomerOrderRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new SkuQuantityCorrectionService(_skuRepoMock.Object, _orderRepoMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CorrectSkuQuantityAsync_ShouldUpdateQuantityAndDeallocate_WhenNotEnoughStock()
    {
        // Arrange
        var sku = new SKU
        {
            Id = Guid.NewGuid(),
            ProductNr = "P001",
            QuantityAvailable = 10
        };

        var lineItem = new LineItem
        {
            Id = Guid.NewGuid(),
            ProductNr = "P001",
            QuantityRequested = 10,
            QuantityAllocated = 10
        };

        var order = new CustomerOrder
        {
            Id = Guid.NewGuid(),
            CompleteDeliveryRequired = true,
            LineItems = new List<LineItem> { lineItem }
        };

        _skuRepoMock.Setup(r => r.GetByIdAsync(sku.Id))
                    .ReturnsAsync(sku);

        _orderRepoMock.Setup(r => r.GetOrdersNeedingAllocationAsync())
                      .ReturnsAsync(new List<CustomerOrder> { order });

        _skuRepoMock.Setup(r => r.GetAvailableSkusForProductAsync("P001"))
                    .ReturnsAsync(new List<SKU> { sku });

        // Act
        await _service.CorrectSkuQuantityAsync(sku.Id, 5);

        // Assert
        Assert.Equal(15, sku.QuantityAvailable);
        Assert.Equal(0, lineItem.QuantityAllocated);
        Assert.Equal("DeallocatedDueToCorrection", order.Status);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}
