using Api.Models.Order;
using Api.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IOrderService
{
    // order
    public IActionResult GetOrder(int id);
    public IActionResult GetOrders();
    public IActionResult AddOrder(Order order);
    public IActionResult DeleteOrder(Order order);
    public IActionResult DeleteOrder(int id);
    public IActionResult EditOrder(Order order);
    // order products
    public IActionResult GetOrderProducts(Order order);
    public IActionResult AddProductToOrder(Order order, Product product);
    public IActionResult AddProductsToOrder(Order order, List<Product> products);
    public IActionResult DeleteProductFromOrder(Order order, Product product);
    public IActionResult SetOrderProducts(Order order, List<OrderProducts> orderProducts);
    public IActionResult ClearOrderProducts(Order order);
    // order status
    public IActionResult AddStatus(OrderStatus orderStatus);
    public IActionResult DeleteStatus(OrderStatus orderStatus);
    public IActionResult EditOrderStatus(OrderStatus orderStatus);
    // order statuses
    public IActionResult AddOrderStatus(Order order, OrderStatus orderStatus);
    public IActionResult DeleteOrderStatus(Order order, OrderStatus orderStatus);
}