using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Order;
using Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[EnableCors("Police1")]
[Route("[controller]")]
[ApiController]
public class OrderController : Controller, IOrderService
{
    private OrderService _orderService;
    
    public OrderController(IConfiguration configuration)
    {
        _orderService = new OrderService(configuration);
    }

    [HttpGet("GetOrder")]
    public IActionResult GetOrder(int id)
    {
        return _orderService.GetOrder(id);
    }
    
    [HttpGet("GetOrders")]
    public IActionResult GetOrders()
    {
        return _orderService.GetOrders();
    }
    
    [HttpPost("AddOrder")]
    public IActionResult AddOrder(Order order)
    {
        return _orderService.AddOrder(order);
    }
    
    [HttpPost("DeleteOrderByObject")]
    public IActionResult DeleteOrder(Order order)
    {
        return _orderService.DeleteOrder(order);
    }
    
    [HttpPost("DeleteOrderById")]
    public IActionResult DeleteOrder(int id)
    {
        return _orderService.DeleteOrder(id);
    }
    
    [HttpPost("EditOrder")]
    public IActionResult EditOrder(Order order)
    {
        return _orderService.EditOrder(order);
    }
    
    [HttpGet("GetOrderProducts")]
    public IActionResult GetOrderProducts(int id)
    {
        return _orderService.GetOrderProducts(id);
    }
    
    [HttpGet("AddProductToOrder")]
    public IActionResult AddProductToOrder(OrderProducts orderProducts)
    {
        return _orderService.AddProductToOrder(orderProducts);
    }
    
    [HttpPost("AddProductsToOrder")]
    public IActionResult AddProductsToOrder(List<OrderProducts> orderProducts)
    {
        return _orderService.AddProductsToOrder(orderProducts);
    }
    
    [HttpPost("DeleteProductFromOrder")]
    public IActionResult DeleteProductFromOrder(OrderProducts orderProducts)
    {
        return _orderService.DeleteProductFromOrder(orderProducts);
    }
    
    [HttpPost("SetOrderProducts")]
    public IActionResult SetOrderProducts(List<OrderProducts> orderProductsList)
    {
        return _orderService.SetOrderProducts(orderProductsList);
    }
    
    [HttpPost("ClearOrderProducts")]
    public IActionResult ClearOrderProducts(Order order)
    {
        return _orderService.ClearOrderProducts(order);
    }
    
    [HttpGet("GetStatuses")]
    public IActionResult GetStatuses()
    {
        return _orderService.GetStatuses();
    }

    [HttpPost("AddStatus")]
    public IActionResult AddStatus(OrderStatus orderStatus)
    {
        return _orderService.AddStatus(orderStatus);
    }
    
    [HttpPost("DeleteStatus")]
    public IActionResult DeleteStatus(OrderStatus orderStatus)
    {
        return _orderService.DeleteStatus(orderStatus);
    }
    
    [HttpPost("EditOrderStatus")]
    public IActionResult EditOrderStatus(OrderStatus orderStatus)
    {
        return _orderService.EditOrderStatus(orderStatus);
    }
    
    [HttpGet("GetOrderStatuses")]
    public IActionResult GetOrderStatuses(int id)
    {
        return _orderService.GetOrderStatuses(id);
    }

    [HttpPost("AddOrderStatus")]
    public IActionResult AddOrderStatus(OrderStatuses orderStatuses)
    {
        return _orderService.AddOrderStatus(orderStatuses);
    }
    
    [HttpPost("DeleteOrderStatus")]
    public IActionResult DeleteOrderStatus(OrderStatuses orderStatuses)
    {
        return _orderService.DeleteOrderStatus(orderStatuses);
    }
}
