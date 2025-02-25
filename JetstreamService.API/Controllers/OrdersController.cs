using Microsoft.AspNetCore.Mvc;
using JetstreamService.API.Models;
using JetstreamService.API.Services;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace JetstreamService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/orders
        [HttpGet]
        public ActionResult<List<ServiceOrder>> Get() =>
            _orderService.GetAllOrders();

        // GET: api/orders/{id}
        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public ActionResult<ServiceOrder> Get(string id)
        {
            var order = _orderService.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/orders
        [HttpPost]
        public ActionResult<ServiceOrder> Create(ServiceOrder order)
        {
            _orderService.CreateOrder(order);
            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }

        // PUT: api/orders/{id}
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, ServiceOrder orderIn)
        {
            var order = _orderService.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.UpdateOrder(id, orderIn);
            return NoContent();
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var order = _orderService.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}
