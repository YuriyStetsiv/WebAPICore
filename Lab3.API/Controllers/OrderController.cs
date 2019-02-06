using AutoMapper;
using Lab3.API.Models;
using Lab3.Domain.Models.Entities;
using Lab3.Domain.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab3.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController:Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [Authorize()]
        public async Task<IEnumerable<OrderViewModel>> Get()
        {
            var orders = await _orderService.GetAllOrders();
            var ordersView = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

            return ordersView;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public async Task<OrderViewModel> Get(int id)
        {
            var order = await _orderService.GetOrderById(id);
            var orderView = _mapper.Map<OrderViewModel>(order);

            return orderView;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Post([FromBody]OrderViewModel orderView)
        {
            if (ModelState.IsValid)
            {
                var order = _mapper.Map<OrderModel>(orderView);
                await _orderService.AddOrder(order);

                return Ok(order);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody]OrderViewModel orderView)
        {
            if (ModelState.IsValid)
            {
                var order = _mapper.Map<OrderModel>(orderView);
                await _orderService.EditOrder(order);

                return Ok(order);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize()]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrder(id);

            return Ok();
        }
    }
}
