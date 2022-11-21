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
    public IActionResult AddProductToOrder(OrderProducts orderProducts);
    public IActionResult AddProductsToOrder(List<OrderProducts> orderProducts);
    public IActionResult DeleteProductFromOrder(OrderProducts orderProducts);
    public IActionResult SetOrderProducts(List<OrderProducts> orderProductsList);
    public IActionResult ClearOrderProducts(Order order);
    // order status
    public IActionResult AddStatus(OrderStatus orderStatus);
    public IActionResult DeleteStatus(OrderStatus orderStatus);
    public IActionResult EditOrderStatus(OrderStatus orderStatus);
    // order statuses
    public IActionResult AddOrderStatus(OrderStatuses orderStatuses);
    public IActionResult DeleteOrderStatus(OrderStatuses orderStatuses);
}