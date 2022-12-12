using Api.Models.Order;
using Api.Services;
using Api.Services.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[EnableCors("Police")]
[Route("api/[controller]")]
[ApiController, Authorize]
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
    
    [HttpPost("DeleteOrder")]
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
    public IActionResult AddProductToOrder(OrderProduct orderProducts)
    {
        return _orderService.AddProductToOrder(orderProducts);
    }
    
    [HttpPost("AddProductsToOrder")]
    public IActionResult AddProductsToOrder(List<OrderProduct> orderProducts)
    {
        return _orderService.AddProductsToOrder(orderProducts);
    }
    
    [HttpPost("DeleteProductFromOrder")]
    public IActionResult DeleteProductFromOrder(OrderProduct orderProducts)
    {
        return _orderService.DeleteProductFromOrder(orderProducts);
    }
    
    [HttpPost("SetOrderProducts")]
    public IActionResult SetOrderProducts(List<OrderProduct> orderProductsList)
    {
        return _orderService.SetOrderProducts(orderProductsList);
    }
    
    [HttpPost("ClearOrderProducts")]
    public IActionResult ClearOrderProducts(int id)
    {
        return _orderService.ClearOrderProducts(id);
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
    public IActionResult DeleteStatus(int orderStatusId)
    {
        return _orderService.DeleteStatus(orderStatusId);
    }
    
    [HttpPost("EditStatus")]
    public IActionResult EditStatus(OrderStatus orderStatus)
    {
        return _orderService.EditStatus(orderStatus);
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
    public IActionResult DeleteOrderStatus(int orderStatusesId)
    {
        return _orderService.DeleteOrderStatus(orderStatusesId);
    }
}
