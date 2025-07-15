using Moq;
using StockAllocation.Application.Interfaces;
using StockAllocation.Application.Services;
using StockAllocation.Domain.Models;

public class OrderCancellationServiceTests
{
    private readonly Mock<ICustomerOrderRepository> _orderRepoMock;
    private readonly Mock<ISkuRepository> _skuRepoMock;
    private readonly OrderCancellationService _service;

    public OrderCancellationServiceTests()
    {
        _orderRepoMock = new Mock<ICustomerOrderRepository>();
        _skuRepoMock = new Mock<ISkuRepository>();
        

        _service = new OrderCancellationService(_orderRepoMock.Object, _skuRepoMock.Object);
    }

    [Fact]
    public async Task CancelOrderAsync_ShouldReleaseAllocatedStock()
    {
        // Arrange
        var order = new CustomerOrder
        {
            Id = Guid.NewGuid(),
            LineItems = new List<LineItem>
        {
            new LineItem
            {
                Id = Guid.NewGuid(),
                ProductNr = "P001",
                QuantityRequested = 5,
                QuantityAllocated = 5
            }
        }
        };

        var sku = new SKU
        {
            Id = Guid.NewGuid(),
            ProductNr = "P001",
            QuantityAvailable = 0
        };

        _orderRepoMock.Setup(r => r.GetByIdWithLineItemsAsync(order.Id))
                      .ReturnsAsync(order);

        _skuRepoMock.Setup(r => r.GetAvailableSkusForProductAsync("P001"))
                    .ReturnsAsync(new List<SKU> { sku });

        // Act
        await _service.CancelOrderAsync(order.Id);

        // Assert
        Assert.Equal(5, sku.QuantityAvailable);
        Assert.Equal(0, order.LineItems[0].QuantityAllocated);
        Assert.Equal("Cancelled", order.Status);
    }
}
