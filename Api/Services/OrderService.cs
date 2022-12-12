using Api.Database;
using Api.Models.Order;
using Api.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public class OrderService : IOrderService
{
    private readonly ProductRepository _productRepository;
    private readonly IConfiguration _configuration;

    public OrderService(IConfiguration configuration)
    {
        _configuration = configuration;
        _productRepository = new ProductRepository(_configuration.GetConnectionString("ProductsAppCon"));
    }
    
    public IActionResult GetOrder(int id)
    {
        return _productRepository.GetOrder(id);
    }

    public IActionResult GetOrders()
    {
        return _productRepository.GetOrders();
    }

    public IActionResult AddOrder(Order order)
    {
        return _productRepository.AddOrder(order);
    }

    public IActionResult DeleteOrder(int id)
    {
        return _productRepository.DeleteOrder(id);
    }

    public IActionResult EditOrder(Order order)
    {
        return _productRepository.EditOrder(order);
    }

    public IActionResult GetOrderProducts(int id)
    {
        return _productRepository.GetOrderProducts(id);
    }

    public IActionResult AddProductToOrder(OrderProduct orderProducts)
    {
        return _productRepository.AddProductToOrder(orderProducts);
    }

    public IActionResult AddProductsToOrder(List<OrderProduct> orderProducts)
    {
        return _productRepository.AddProductsToOrder(orderProducts);
    }

    public IActionResult DeleteProductFromOrder(OrderProduct orderProducts)
    {
        return _productRepository.DeleteProductFromOrder(orderProducts);
    }

    public IActionResult SetOrderProducts(List<OrderProduct> orderProductsList)
    {
        return _productRepository.SetOrderProducts(orderProductsList);
    }

    public IActionResult ClearOrderProducts(int id)
    {
        return _productRepository.ClearOrderProducts(id);
    }

    public IActionResult GetStatuses()
    {
        return _productRepository.GetStatuses();
    }

    public IActionResult AddStatus(OrderStatus orderStatus)
    {
        return _productRepository.AddStatus(orderStatus);
    }

    public IActionResult DeleteStatus(int orderStatusId)
    {
        return _productRepository.DeleteStatus(orderStatusId);
    }

    public IActionResult EditStatus(OrderStatus orderStatus)
    {
        return _productRepository.EditStatus(orderStatus);
    }

    public IActionResult GetOrderStatuses(int id)
    {
        return _productRepository.GetOrderStatuses(id);
    }

    public IActionResult AddOrderStatus(OrderStatuses orderStatuses)
    {
        return _productRepository.AddOrderStatus(orderStatuses);
    }

    public IActionResult DeleteOrderStatus(int orderStatusId)
    {
        return _productRepository.DeleteOrderStatus(orderStatusId);
    }
}